using liquidador_web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace liquidador_web.Extra
{
    public class Reliquidacion
    {
        private readonly IConsultas _consultas;

        private List<tasas> TablaTasas = new List<tasas>();
        private List<abonos> TablaAbonos = new List<abonos>();

        private readonly DateTime fechaSaldo;
        private readonly double saldoInicial;
        private readonly double saldoBanco;
        private double interesPlazo;
        //private double correccionMonetaria;
        private reliquidacion filaActual, filaAnterior;
        private readonly bool saldoInicialEnUPAC;
        private readonly bool saldoBancoEnUPAC;
        private double saldoInicialEnPesos;
        private double saldoFinalEnPesos;
        private readonly int factor;

        public List<reliquidacion> TablaReLiquidacion = new List<reliquidacion>();

        public Reliquidacion(IConsultas _consultas, double saldoInicial, DateTime fechaSaldo, bool saldoInicialEnUPAC, bool saldoBancoEnUPAC, double saldoBanco, double interesPlazo, int factor, DateTime?[] f_movimiento, double?[] pago_movimiento, double?[] seguro_movimiento, double?[] mora_movimiento, double?[] otros_movimiento, bool[] ficticio, DateTime?[] f_tasas, double?[] tasas) {
            this._consultas = _consultas;

            this.saldoInicial = saldoInicial;
            this.fechaSaldo = fechaSaldo;
            this.saldoInicialEnUPAC = saldoInicialEnUPAC;
            this.saldoBancoEnUPAC = saldoBancoEnUPAC;
            this.saldoBanco = saldoBanco;
            this.interesPlazo = interesPlazo;
            this.factor = factor;

            for (int i = 0; i < f_tasas.Length; i++)
                if (tasas[i] != null && f_tasas[i] != null)
                    TablaTasas.Add(new tasas { fecha = f_tasas[i] ?? DateTime.Now, valor = tasas[i] ?? 0 });

            for (int i = 0; i < f_movimiento.Length; i++)
                if (f_movimiento[i] != null)
                    TablaAbonos.Add(new abonos { fecha = f_movimiento[i] ?? DateTime.Now, pago = pago_movimiento[i] ?? 0, seguro = seguro_movimiento[i] ?? 0, mora = mora_movimiento[i] ?? 0, otros = otros_movimiento[i] ?? 0, ficticio = ficticio[i] });

            TablaTasas.Sort((x, y) => DateTime.Compare(x.fecha, y.fecha));
            TablaAbonos.Sort((x, y) => DateTime.Compare(x.fecha, y.fecha));
        }

        public List<(string, double)> Resumen() {
            List<(string, double)> response = new List<(string, double)>();

            if (saldoInicialEnUPAC)
                response.Add(("Saldo Banco en UPAC", saldoInicial));

            response.Add(("Saldo Inicial", saldoInicialEnPesos));

            if (saldoBancoEnUPAC)
                response.Add(("Saldo Final en UPAC", saldoBanco));

            response.Add(("Saldo Final Banco", saldoFinalEnPesos));
            response.Add(("Saldo Reliquidación en UVR", filaActual.saldoUVR));
            response.Add(("Saldo Reliquidación", filaActual.saldoPesos));
            response.Add(("Diferencia", saldoFinalEnPesos - filaActual.saldoPesos));

            return response;
        }

        public void Reliquidar() {
            incluirMovimientosFictiosPorCambioTasas();

            if (saldoInicialEnUPAC)
                saldoInicialEnPesos = (_consultas.CalcTasa("UPAC", fechaSaldo).Result.ValorTasa ?? 0) * saldoInicial;
            else
                saldoInicialEnPesos = saldoInicial;

            if (saldoBancoEnUPAC)
                saldoFinalEnPesos = (_consultas.CalcTasa("UPAC", DateTime.Parse("31/12/1999")).Result.ValorTasa ?? 0) * saldoBanco;
            else
                saldoFinalEnPesos = saldoBanco;

            CompletarPeriodo(); //Si no hay movimientos el 31/12/1999 se agrega un registro con abono de cero pesos

            // Adicionar primer registro de la tabla resultante
             var tempCotizacion = _consultas.CalcTasa("UVR", fechaSaldo).Result.ValorTasa ?? 0;

            TablaReLiquidacion.Add(new reliquidacion
            {
                factor = factor,
                fechaPago = fechaSaldo,
                numeroDias = 0,
                tasaInteres = interesPlazo,
                correccionMonetaria = 0,
                pagosPesos = 0,
                segurosPesos = 0,
                moraPesos = 0,
                abonoEfectivoPesos = 0,
                cotizacionUVR = tempCotizacion,
                pagoUVR = 0,
                tasaInteresUVR = interesPlazo,
                interesPeriodoUVR = 0,
                amortizacionUVR = 0,
                saldoUVR = saldoInicialEnPesos / tempCotizacion,
                interesPeriodoPesos = 0,
                amortizacionPesos = 0,
                saldoPesos = saldoInicialEnPesos
            });

            filaAnterior = TablaReLiquidacion.First();

            foreach (abonos abono in TablaAbonos)
                ReliquidarLapso(abono);
        }
        /// <summary>
        /// Cuando hay cambio de tasas en fechas en las que no hay movimiento
        /// hay que incluir un movimiento con cero pesos para que se haga efectivo
        /// el cambio de tasa a partir de la fecha
        /// </summary>
        private void incluirMovimientosFictiosPorCambioTasas() {
            bool existeAbonoEnLaFechaDeLaTasa = false;

            foreach (tasas elemento in TablaTasas)
            {
                foreach (abonos abono in TablaAbonos)
                    if (elemento.fecha == abono.fecha)
                        existeAbonoEnLaFechaDeLaTasa = true;

                if (!existeAbonoEnLaFechaDeLaTasa)
                    TablaAbonos.Add(new abonos { fecha = elemento.fecha, pago = 0, seguro = 0, mora = 0, otros = 0, ficticio = false });
            }

            TablaAbonos.Sort((x, y) => DateTime.Compare(x.fecha, y.fecha));
        }

        /// <summary>
        /// Si los abonos tiene abonos hasta diciembre 31 no hay que hacer nada
        /// pero si los movimientos estan hasta antes de diciembre 31 entonces
        /// hay que agregar un registro a la tabla de abonos con fecha del 31 de diciembre
        /// para que reliquide hasta el 31 de diciembre de 1999
        /// </summary>
        private void CompletarPeriodo() {
            //-- Recuperar el ultimo abono
            if (TablaAbonos.Count > 0) {
                var abono = TablaAbonos.Last();
                if (abono.fecha < DateTime.Parse("31/12/1999")) //Si el ultimo abono no fue el 31 de diciembre 1999 entonces adicionar una row
                    TablaAbonos.Add(new abonos { fecha = DateTime.Parse("31/12/1999") });
            } else //-- Si no hay filas, agregar una
                TablaAbonos.Add(new abonos { fecha = DateTime.Parse("31/12/1999") });
        }

        private void ReliquidarLapso(abonos abono) {
            double interesAplicado = 0, amortizacion = 0;

            //-- Factor de dias 360 or 365
            filaActual.factor = factor;
            filaActual.fechaPago = abono.fecha;
            filaActual.cotizacionUVR = _consultas.CalcTasa("UVR", filaActual.fechaPago).Result.ValorTasa ?? 0;
            filaActual.numeroDias = (int)(filaActual.fechaPago - filaAnterior.fechaPago).TotalDays;
            filaActual.pagosPesos = abono.pago;
            filaActual.segurosPesos = abono.seguro;
            filaActual.moraPesos = abono.mora;
            filaActual.tasaInteres = obtenerInteresPlazoAplicarEnELPeriodo(filaActual.fechaPago);
            filaActual.correccionMonetaria = 0; //-- filaActual.correccionMonetaria = correccionMonetaria;
            filaActual.abonoEfectivoPesos = abono.pago - abono.seguro - abono.mora - abono.otros;

            if (filaActual.abonoEfectivoPesos < 0) filaActual.abonoEfectivoPesos = 0;
            filaActual.pagoUVR = filaActual.cotizacionUVR == 0 ? 0: filaActual.abonoEfectivoPesos / filaActual.cotizacionUVR;
            interesAplicado = calcularInteresDelPeriodo(filaActual.numeroDias);
            filaActual.tasaInteresUVR = interesAplicado;
            filaActual.interesPeriodoUVR = interesAplicado * filaAnterior.saldoUVR ;
            amortizacion = filaActual.pagoUVR - filaActual.interesPeriodoUVR;
            //if( amortizacion < 0 ) amortizacion = 0
            filaActual.amortizacionUVR = amortizacion;
            filaActual.saldoUVR = filaAnterior.saldoUVR - amortizacion;

            //-- Pesos
            filaActual.interesPeriodoPesos = filaActual.interesPeriodoUVR * filaActual.cotizacionUVR;
            filaActual.amortizacionPesos = filaActual.amortizacionUVR * filaActual.cotizacionUVR;
            filaActual.saldoPesos = filaActual.saldoUVR * filaActual.cotizacionUVR;

            filaAnterior = filaActual;
            TablaReLiquidacion.Add(filaActual);
        }

        private double obtenerInteresPlazoAplicarEnELPeriodo(DateTime fecha)
        {

            if (TablaTasas.Count == 1)
                return TablaTasas.First().valor;
            else if (TablaTasas.Count > 0)
                foreach (tasas elemento in TablaTasas)
                    if (elemento.fecha < fecha)
                        return elemento.valor;

            return interesPlazo;
        }

        private double calcularInteresDelPeriodo(int dias) {
            //Si el saldo inicial esta en UPAC el credito es en UPACS
            //y el interes sobre el UVR es la formula
            //interes = 1+(interesplazo/100)^(NroDias / Me._factor)-1

            //double calcularInteresDelPeriodo = 0;
            //if (saldoInicialEnUPAC)
            //{
            //    calcularInteresDelPeriodo = obtenerInteresPlazoAplicarEnELPeriodo(filaActual.fechaPago);
            //    calcularInteresDelPeriodo /= 100;
            //    calcularInteresDelPeriodo += 1;
            //    calcularInteresDelPeriodo = Math.Pow(calcularInteresDelPeriodo, (double)dias / (double)factor);
            //    return calcularInteresDelPeriodo -= 1;
            //}
            //else {
            //    var plazo = obtenerInteresPlazoAplicarEnELPeriodo(filaActual.fechaPago);
            //    calcularInteresDelPeriodo = plazo + ((1 + plazo) / (1 + calcularCMI()) - 1);
            //    calcularInteresDelPeriodo = 1 + (calcularInteresDelPeriodo / 100);
            //    calcularInteresDelPeriodo = Math.Pow(calcularInteresDelPeriodo, (double)dias / (double)factor) - 1;
            //    return calcularInteresDelPeriodo;
            //}


            if (saldoInicialEnUPAC)
                return Math.Pow(1 + obtenerInteresPlazoAplicarEnELPeriodo(filaActual.fechaPago) / 100, (double)dias / (double)factor) - 1;
            else
                //Si el saldo inicial es en pesos el credito es en pesos
                //Entonces la tasa de interes se calcula con la formula prevista en el
                //Decreto 2702 de 1999
                //Fi= (1+TI) / (1+CMI) - 1
                return Math.Pow(1 + (obtenerInteresPlazoAplicarEnELPeriodo(filaActual.fechaPago) + ((1 + interesPlazo) / (1 + calcularCMI())) - 1 ) / 100, (double)dias / (double)factor) - 1;
        }

        private double calcularCMI() {
            //-- Calcular la variación porcentual de la UPAC durante el periódo de causación
            //-- Decreto 2701 de 1999
            double valorUPAC1 = _consultas.CalcTasa("UPAC", filaAnterior.fechaPago).Result.ValorTasa ?? 0;
            double valorUPAC2 = _consultas.CalcTasa("UPAC", filaActual.fechaPago).Result.ValorTasa ?? 0;
            //Calcular variacion
            //VP=[(a-b).100]/a
            return (valorUPAC2 - valorUPAC1) * 100 / valorUPAC1 - 1;
        }

        private struct tasas {
            public DateTime fecha;
            public double valor;
        }

        private struct abonos
        {
            public DateTime fecha;
            public double pago;
            public double seguro;
            public double mora;
            public double otros;
            public bool ficticio;
        }
    }

    public struct reliquidacion
    {
        public int factor;
        public DateTime fechaPago;
        public int numeroDias;
        public double tasaInteres;
        public double correccionMonetaria;
        public double pagosPesos;
        public double segurosPesos;
        public double moraPesos;
        public double abonoEfectivoPesos;
        public double cotizacionUVR;
        public double pagoUVR;
        public double tasaInteresUVR;
        public double interesPeriodoUVR;
        public double amortizacionUVR;
        public double saldoUVR;
        public double interesPeriodoPesos;
        public double amortizacionPesos;
        public double saldoPesos;
    }
}
