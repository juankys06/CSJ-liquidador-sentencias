using liquidador_web.Interfaces;
using System;
using System.Collections.Generic;

namespace liquidador_web.Extra
{
    public class HipotecarioUVR
    {
        private readonly IConsultas _consultas;
        private List<FechaValor> TablaAbonosYCapitales = new List<FechaValor>();
        public List<Liquidacion> TablaLiquidacion;
        private Liquidacion filaAnterior;
        private Liquidacion filaActual;

        private DateTime fechaInicial;
        private DateTime fechaInicioObligacion;
        private DateTime fechaFinalObligacion;
        private DateTime fechaLiquidacion;
        private double capital;
        private double interesMoraPactado;
        private DateTime fechaContrato;
        /// <summary>
        /// Flag que indica si es Vivienda de Interes social
        /// Con este flag se controla si se esta liquidando vivienda de interes social o no, para efectos de determinar 
        /// el interés maximo remuneratorio y moratorio de acuerdo a si es VIS o no
        /// </summary>
        private bool vis;
        /// <summary>
        /// Monto en pesos de los abonos efectivos
        /// Los abonos menos el valor tenido en cuenta como pago de seguros
        /// </summary>

        private double interesMoraAdeudadoAcum = 0;
        private double interesMoraAdeudadoAcumTotal = 0;
        private double interesPlazoAdeudadoAcum = 0;
        private double interesPlazoAdeudadoAcumTotal = 0;
        private double seguroAcum = 0 ;
        private double capitalesAcum = 0 ;
        private double abonosAcum = 0 ;
        private double dineroDevolver = 0 ;
        private bool flagTerminoLiquidacion = false;

        private double capitalInicial;

        private double interesCorriente;

        private static bool flagControl = false ; //Controla que no lea de la base de datos la variacion cada vez que se liquida un periodo
        private static double variacionAnualUVR = 0;
        private static bool ControlSiYaSePasoADeInteresDePlazoAInteresDeMora = false;
        /// <summary>
        /// Indicador de si la clase debe ser instanciada como Hipotecario en UVR o en Pesos.
        /// </summary>
        private readonly bool isUVR;

        public HipotecarioUVR(IConsultas consultas, DateTime fechaContrato, double capital, DateTime fechaInicioObligacion,
                              DateTime fechaFinalObligacion, DateTime fechaLiquidacion, bool vis, double interesCorriente,
                              double interesMora, DateTime?[] f_abono, double?[] pago_abono, double?[] seguro_abono,
                              DateTime?[] f_capitales, double?[] capitales)
        {
            _consultas = consultas;
            this.fechaContrato = fechaContrato;
            this.capital = capital;
            capitalInicial = capital;
            this.fechaInicioObligacion = fechaInicioObligacion;
            fechaInicial = fechaInicioObligacion;
            this.fechaFinalObligacion = fechaFinalObligacion;
            this.fechaLiquidacion = fechaLiquidacion;
            this.vis = vis;
            this.interesCorriente = interesCorriente;

            if (pago_abono != null && f_abono != null)
                for (int i = 0; i < f_abono.Length; i++)
                    if (TablaAbonosYCapitales.Exists(f => f.Fecha == f_abono[i]))
                    {
                        TablaAbonosYCapitales.Find(f => f.Fecha == f_abono[i]).valorAbono += pago_abono[i] ?? 0 - seguro_abono[i] ?? 0;
                        seguroAcum += seguro_abono[i] ?? 0;
                    }
                    else if (pago_abono[i] > 0)
                    {
                        TablaAbonosYCapitales.Add(new FechaValor(f_abono[i] ?? DateTime.MinValue, (pago_abono[i] ?? 0) - (seguro_abono[i] ?? 0)));
                        seguroAcum += seguro_abono[i] ?? 0;
                    }

            if (f_capitales != null && capitales != null)
                for (int i = 0; i < f_capitales.Length; i++)
                    if (TablaAbonosYCapitales.Exists(f => f.Fecha == f_capitales[i]))
                        TablaAbonosYCapitales.Find(f => f.Fecha == f_capitales[i]).valorCapital += capitales[i] ?? 0;
                    else if (capitales[i] > 0)
                        TablaAbonosYCapitales.Add(new FechaValor() { Fecha = f_capitales[i]?? DateTime.MinValue, valorCapital = capitales[i] ?? 0});

            TablaAbonosYCapitales.Sort((x, y) => DateTime.Compare(x.Fecha, y.Fecha));

            interesMoraPactado = interesMora;
            TablaLiquidacion = new List<Liquidacion>();
            isUVR = true ;
            ControlSiYaSePasoADeInteresDePlazoAInteresDeMora = false;
        }

        public HipotecarioUVR(IConsultas consultas, double capital, DateTime fechaContrato, DateTime fechaInicioObligacion,
                              DateTime fechaFinalObligacion, DateTime fechaLiquidacion, bool vis, double interesCorriente,
                              double interesMora, DateTime?[] f_abono, double?[] pago_abono, double?[] seguro_abono,
                              DateTime?[] f_capitales, double?[] capitales)
        {
            _consultas = consultas;
            this.fechaContrato = fechaContrato;
            this.capital = capital;
            capitalInicial = capital;
            this.fechaInicioObligacion = fechaInicioObligacion;
            fechaInicial = fechaInicioObligacion;
            this.fechaFinalObligacion = fechaFinalObligacion;
            this.fechaLiquidacion = fechaLiquidacion;
            this.vis = vis;
            this.interesCorriente = interesCorriente;

            if (pago_abono != null && f_abono != null)
                for (int i = 0; i < f_abono.Length; i++)
                    if (TablaAbonosYCapitales.Exists(f => f.Fecha == f_abono[i]))
                    {
                        TablaAbonosYCapitales.Find(f => f.Fecha == f_abono[i]).valorAbono += pago_abono[i] ?? 0 - seguro_abono[i] ?? 0;
                        seguroAcum += seguro_abono[i] ?? 0;
                    }
                    else if (pago_abono[i] > 0)
                    {
                        TablaAbonosYCapitales.Add(new FechaValor(f_abono[i] ?? DateTime.MinValue, (pago_abono[i] ?? 0) - (seguro_abono[i] ?? 0)));
                        seguroAcum += seguro_abono[i] ?? 0;
                    }

            if (f_capitales != null && capitales != null)
                for (int i = 0; i < f_capitales.Length; i++)
                    if (TablaAbonosYCapitales.Exists(f => f.Fecha == f_capitales[i]))
                        TablaAbonosYCapitales.Find(f => f.Fecha == f_capitales[i]).valorCapital += capitales[i] ?? 0;
                    else if (capitales[i] > 0)
                        TablaAbonosYCapitales.Add(new FechaValor() { Fecha = f_capitales[i] ?? DateTime.MinValue, valorCapital = capitales[i] ?? 0 });

            TablaAbonosYCapitales.Sort((x, y) => DateTime.Compare(x.Fecha, y.Fecha));

            interesMoraPactado = interesMora;
            TablaLiquidacion = new List<Liquidacion>();

            isUVR = false;

            flagControl = false;
            variacionAnualUVR = 0;
            ControlSiYaSePasoADeInteresDePlazoAInteresDeMora = false;
        }

        public List<(string, double, double)> ResumenUVR()
        {
            double totalCapital = capitalesAcum + capitalInicial;

            List<(string, double, double)> resp = new List<(string, double, double)>()
            {
                ("Saldo Capital", filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR, (filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR) * filaActual.valorUVR),
                ("Saldo Interés Plazo", 0, filaActual.saldoInteresPlazoAcum),
                ("Saldo Interés Mora", 0, filaActual.saldoInteresMoraPesos),
                ("Total a Pagar", 0, (filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR) * filaActual.valorUVR + filaActual.saldoInteresMoraPesos + filaActual.saldoInteresPlazoAcum),
            };

            if (dineroDevolver > 0)
                resp.Add(("Saldo devolver al deudor", 0, dineroDevolver));

            if (seguroAcum > 0)
                resp.Add(("- Pago seguros", 0, seguroAcum));
            
            return resp;

            /*
            if (seguroAcum > 0)
                return new List<(string, double, double)> {
                    ("Saldo Capital", filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR, (filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR) * filaActual.valorUVR),
                    ("Saldo Interés Plazo", 0, filaActual.saldoInteresPlazoAcum),
                    ("Saldo Interés Mora", 0, filaActual.saldoInteresMoraPesos),
                    ("Total a Pagar", 0, (filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR) * filaActual.valorUVR + filaActual.saldoInteresMoraPesos + filaActual.saldoInteresPlazoAcum),
                    ("- Pago seguros", 0, seguroAcum)
                };
            else
                return new List<(string, double, double)> {
                    ("Saldo Capital", filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR, (filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR) * filaActual.valorUVR),
                    ("Saldo Interés Plazo", 0, filaActual.saldoInteresPlazoAcum),
                    ("Saldo Interés Mora", 0, filaActual.saldoInteresMoraPesos),
                    ("Total a Pagar", 0, (filaActual.CapitalALiquidar - filaActual.abonoCapitalUVR) * filaActual.valorUVR + filaActual.saldoInteresMoraPesos + filaActual.saldoInteresPlazoAcum),
                };
            */
        }

        public List<(string, double)> Resumen()
        {
            double totalCapital = capitalesAcum + capitalInicial;
            double totalAPagar = interesMoraAdeudadoAcumTotal + interesPlazoAdeudadoAcumTotal + totalCapital;

            List<(string, double)> resp = new List<(string, double)>() {
                ("Capital", capitalInicial),
                ("Capitales Adicionados", capitalesAcum),
                ("Total Capital", totalCapital),
                ("Total Interés de Plazo", interesPlazoAdeudadoAcumTotal),
                ("Total Interés Mora", interesMoraAdeudadoAcumTotal),
                ("Total a Pagar", totalAPagar),
                ("- Abonos", abonosAcum),
                ("Neto a Pagar", (totalAPagar - abonosAcum > 0) ? totalAPagar - abonosAcum : 0)
            };

            if (dineroDevolver > 0)
                resp.Add(("Saldo devolver al deudor", dineroDevolver));

            if (seguroAcum > 0)
                resp.Add(("- Pago seguros", seguroAcum));

            return resp;
        }

        public void Liquidar()
        {
            DateTime fechaTmp = fechaInicial;
            int noDiasLiquidar, noDiasMes, noMeses;

            if (fechaInicial.Day > 1) {
                if (fechaInicial.Month == fechaLiquidacion.Month && fechaInicial.Year == fechaLiquidacion.Year)
                {
                    //-- Se esta liquidando dentro del mismo mes y anho
                    noDiasLiquidar = (int)(fechaLiquidacion - fechaInicial).TotalDays + 1;
                    MirarSihayAbonosYCapitalYLiquidar(fechaInicial, noDiasLiquidar);
                    return;
                }
                else
                {
                    //-- Se esta liquidando en meses diferentes
                    noDiasMes = DateTime.DaysInMonth(fechaInicial.Year, fechaInicial.Month);
                    //-- +1 porque cuenta el dia inicial pq es la fecha en que empieza la liquidacion
                    noDiasLiquidar = noDiasMes - fechaInicial.Day + 1;
                    MirarSihayAbonosYCapitalYLiquidar(fechaInicial, noDiasLiquidar);
                    fechaTmp = fechaInicial.AddDays(noDiasLiquidar);
                }
            }

            noMeses = (fechaLiquidacion.Year - fechaTmp.Year) * 12 + fechaLiquidacion.Month - fechaTmp.Month + (fechaLiquidacion.Day >= fechaTmp.Day ? 0 : -1);
            for (int i = 0; i < noMeses; i++)
            {
                noDiasLiquidar = DateTime.DaysInMonth(fechaTmp.Year, fechaTmp.Month);
                MirarSihayAbonosYCapitalYLiquidar(fechaTmp, noDiasLiquidar);
                fechaTmp = fechaTmp.AddDays(noDiasLiquidar);
            }

            if (fechaLiquidacion.Day >= 1)
            {
                MirarSihayAbonosYCapitalYLiquidar(fechaTmp, fechaLiquidacion.Day);
                fechaTmp = fechaTmp.AddDays(fechaLiquidacion.Day);
            }
        }

        private void MirarSihayAbonosYCapitalYLiquidar(DateTime fechaInicial, int noDias)
        {
            if (flagTerminoLiquidacion)
                return;

            bool flagAbonos = false;
            double valorAbonoUVR;

            ControlSiYaSePasoADeInteresDePlazoAInteresDeMora = false;
            if (fechaFinalObligacion > fechaInicial && fechaFinalObligacion < fechaInicial.AddDays(noDias) && !ControlSiYaSePasoADeInteresDePlazoAInteresDeMora)
            {
                int NoDiasALiq = (fechaFinalObligacion - fechaInicial).Days;
                ControlSiYaSePasoADeInteresDePlazoAInteresDeMora = true;
                MirarSihayAbonosYCapitalYLiquidar(fechaInicial, NoDiasALiq);
                MirarSihayAbonosYCapitalYLiquidar(fechaInicial.AddDays(NoDiasALiq), noDias - NoDiasALiq);
                return;
            }

            DateTime fechaFinal = fechaInicial.AddDays(noDias);
            //Controlar si hay que crear partir el periodo debido a que cambio el monto del interes maximo remuneratorio 
            foreach (Models.Datasainte filaInteres in _consultas.getTasas(vis ? "UVIS" : "UNVI").Result)
                //Si la fecha del registro que tiene la informacion del cambio de la
                //tasa maxima pertenece al lapso a liquidar entonces partir el periodo y liquidar
                if (fechaInicial < filaInteres.VigenteDesde && fechaFinal >= filaInteres.VigenteDesde.AddDays(1)) {
                    int noDiasALiq = (filaInteres.VigenteDesde - fechaInicial).Days;
                    MirarSihayAbonosYCapitalYLiquidar(fechaInicial, noDiasALiq);
                    MirarSihayAbonosYCapitalYLiquidar(fechaInicial.AddDays(noDiasALiq), noDias - noDiasALiq);
                    return;
                }

            if (TablaAbonosYCapitales.Count > 0)
                foreach (FechaValor fila in TablaAbonosYCapitales) {
                    if (fila.Fecha >= fechaInicial && fila.Fecha <= fechaInicial.AddDays(noDias - 1) && !fila.acTenidoEncuenta) {
                        valorAbonoUVR = fila.valorAbono / _consultas.CalcTasa("UVR", fila.Fecha).Result.ValorTasa ?? 0;

                        if (fila.Fecha == fechaInicial)
                        {
                            TablaLiquidacion.Add(LiquidarLapso(fechaInicial, 1, isUVR ? valorAbonoUVR : fila.valorAbono, fila.valorCapital));
                            Abonar(fila.valorAbono, isUVR);
                            DateTime fec = fechaInicial.AddDays(1);
                            MirarSihayAbonosYCapitalYLiquidar(fec, noDias - 1);
                            fila.acTenidoEncuenta = true;
                            return;
                        }
                        else if (fila.Fecha == fechaInicial.AddDays(noDias - 1))
                        {
                            //-- Abono el ultimo dia del periodo a liquidar
                            TablaLiquidacion.Add(LiquidarLapso(fechaInicial, noDias - 1, 0, 0));
                            DateTime fec = fechaInicial.AddDays(noDias - 1);
                            TablaLiquidacion.Add(LiquidarLapso(fec, 1, isUVR ? valorAbonoUVR : fila.valorAbono, fila.valorCapital));
                            Abonar(fila.valorAbono, isUVR);
                        }
                        else {
                            int tmpNoDias;
                            tmpNoDias = fila.Fecha.Day - fechaInicial.Day;
                            TablaLiquidacion.Add(LiquidarLapso(fechaInicial, tmpNoDias, 0, 0));
                            noDias -= tmpNoDias; //-- Descontar los dias ya liquidados

                            TablaLiquidacion.Add(LiquidarLapso(fila.Fecha, 1, isUVR ? valorAbonoUVR : fila.valorAbono, fila.valorCapital));
                            noDias -= 1; //-- Descontar los dias ya liquidados
                            Abonar(fila.valorAbono, isUVR);

                            MirarSihayAbonosYCapitalYLiquidar(fila.Fecha.AddDays(1), noDias);
                            fila.acTenidoEncuenta = true;
                            return;
                        }

                        flagAbonos = true;
                    }
                }

            //-- Si no hubo abonos entonces liquidar con fecha y dias 
            //-- que se encuentran en parametros
            if (!flagAbonos)
                TablaLiquidacion.Add(LiquidarLapso(fechaInicial, noDias));
        }

        private Liquidacion LiquidarLapso(DateTime fechaInicial, int noDias, double valorAbono = 0, double valorCapital = 0)
        {
            Liquidacion rw = new Liquidacion();

            if (valorCapital > 0)
                capital += valorCapital;

            capitalesAcum += valorCapital;
            if(!isUVR)
                abonosAcum += valorAbono;

            rw.NoDias = noDias;

            rw.Desde = fechaInicial; rw.Hasta = fechaInicial.AddDays(noDias - 1);

            if (filaAnterior == null)
                rw.capital = capital;
            else if (valorCapital != 0)
                rw.capital = valorCapital;

            rw.CapitalALiquidar = capital;

            if (!isUVR && valorAbono != 0) rw.abonos = valorAbono;

            rw.TasaAnual = interesCorriente;
            rw.TasaMaxima = interesMoraPactado;

            if(isUVR){
                rw.valorUVR = _consultas.CalcTasa("UVR", rw.Hasta).Result.ValorTasa ?? 0;
                rw.capitalPesos = rw.CapitalALiquidar * rw.valorUVR;
            }

            if (rw.Hasta < fechaFinalObligacion) {
                establecerInteresPlazoAplicado(rw, fechaInicial);
                rw.InteresNominal = CalcularInteresNominalDiario(rw.intAplicado);
                rw.intPlazoPeriodo = (isUVR?rw.capitalPesos:rw.CapitalALiquidar) * CalcularInteresNominalDiario(rw.intAplicado) * noDias;

                interesPlazoAdeudadoAcum += rw.intPlazoPeriodo;
                interesPlazoAdeudadoAcumTotal += rw.intPlazoPeriodo;
            } else { //-- Intereses mora
                establecerInteresMoraAplicado(rw, fechaInicial);

                rw.InteresNominal = CalcularInteresNominalDiario(rw.intAplicado);
                rw.InteresMoraPeriodo = capital * CalcularInteresNominalDiario(rw.intAplicado) * noDias;

                rw.interesMoraPeriodoPesos = rw.InteresMoraPeriodo * rw.valorUVR;
                interesMoraAdeudadoAcumTotal += isUVR ? rw.interesMoraPeriodoPesos : rw.InteresMoraPeriodo;
                interesMoraAdeudadoAcum += isUVR ? rw.interesMoraPeriodoPesos : rw.InteresMoraPeriodo;
                rw.saldoInteresMoraPesos = interesMoraAdeudadoAcum;
                rw.interesAdeudadoMoraAcum = interesMoraAdeudadoAcum;
            }

            rw.saldoInteresPlazoAcum += interesPlazoAdeudadoAcum;
            rw.totalPesos = rw.capitalPesos + interesMoraAdeudadoAcum + interesPlazoAdeudadoAcum;
            rw.subTotal = capital + interesMoraAdeudadoAcum + interesPlazoAdeudadoAcum;

            filaAnterior = rw;
            filaActual = rw;

            return rw;
        }

        private void establecerInteresPlazoAplicado(Liquidacion rw, DateTime fechaInicial)
        {
            double tasaVigenteIBC, tasaVigenteRem = 0;

            tasaVigenteIBC = _consultas.CalcTasa("IBC", fechaInicial).Result.ValorTasa ?? 0;
            foreach (Models.Datasainte fila in _consultas.getTasas(vis ? "UVIS" : "UNVI").Result)
                if (fechaInicial >= fila.VigenteDesde && fechaInicial < fila.VigenteHasta.AddDays(1))
                    tasaVigenteRem = fila.ValorTasa ?? 0;

            //Inicialmente se presume que esta bien el interes plazo dentro del maximo permitido
            rw.intAplicado = interesCorriente;
            if (isUVR)
            {
                //1. El interes de plazo pactado debe ser menor o igual a el de plazo establecido como limite
                //En las resoluciones del banco de la republica
                if (rw.intAplicado >= tasaVigenteRem && tasaVigenteRem > 0)
                    rw.intAplicado = tasaVigenteRem;
            }
            else {
                if (!flagControl) {
                    variacionAnualUVR = _consultas.CalcTasa("VUVR", fechaContrato).Result.ValorTasa ?? 0;
                    flagControl = true;
                }

                if (rw.intAplicado > (tasaVigenteRem + variacionAnualUVR) && tasaVigenteRem > 0)
                    rw.intAplicado = tasaVigenteRem + variacionAnualUVR;
            }
            //2. El interes plazo no puede ser mayor a la tasa de usura
            if (rw.intAplicado > tasaVigenteIBC * 1.5)
                //Se establece la tasa maxima como el interes remuneratorio
                rw.intAplicado = tasaVigenteIBC * 1.5;
        }

        private void establecerInteresMoraAplicado(Liquidacion rw, DateTime fechaInicial)
        {
            double tasaVigenteIBC = 0, tasaVigenteREM = 0;

            tasaVigenteIBC = _consultas.CalcTasa("IBC", fechaInicial).Result.ValorTasa ?? 0;
            //Interes reumentarorio maximo para Creditos en UVR
            foreach (Models.Datasainte fila in _consultas.getTasas(vis ? "UVIS" : "UNVI").Result)
                if (fechaInicial >= fila.VigenteDesde && fechaInicial < fila.VigenteHasta.AddDays(1))
                    tasaVigenteREM = fila.ValorTasa ?? 0;

            //Inicialmente se presume que esta bien el interes mora dentro del maximo permitido
            rw.intAplicado = interesMoraPactado;

            //1. El interes mora pactado no puede superar 1.5 veces el remuneratorio
            if (rw.intAplicado > interesCorriente * 1.5)
                rw.intAplicado = interesCorriente * 1.5;

            if (isUVR)
            {
                //2. El interes de mora pactado debe ser menor a 1.5 veces el maximo remuneratorio (ojo no el pactado sino el de ley)
                if (rw.intAplicado > tasaVigenteREM * 1.5 && tasaVigenteREM > 0)
                    //Se establece la tasa maxima como el interes remuneratorio
                    rw.intAplicado = tasaVigenteREM * 1.5;
            }
            else {
                if (!flagControl) {
                    variacionAnualUVR = _consultas.CalcTasa("VUVR", fechaContrato).Result.ValorTasa ?? 0;
                    flagControl = true;
                }

                if (rw.intAplicado > (tasaVigenteREM + variacionAnualUVR) * 1.5)
                    rw.intAplicado = (tasaVigenteREM + variacionAnualUVR) * 1.5;
            }
            //3. El interes mora no puede ser mayor a la tasa de usura
            if (rw.intAplicado > tasaVigenteIBC * 1.5)
                //Se establece la tasa maxima como el interes remuneratorio
                rw.intAplicado = tasaVigenteIBC * 1.5;
        }

        private double CalcularInteresNominalDiario(double tasa) {
            return Math.Pow((double)1 + tasa / (double)100, (double)1 / (double)365) - (double)1;
        }

        private void Abonar(double pvalorAbono, bool isUVR) {
            //Orden de Abonos:
            //Intereses acumulados de mora
            //Intereses Acumulados Corrientes
            //Capital Acumulado
            double A = pvalorAbono, valorUVR = 0, k;
            //En la columna abonos se coloca el valor del abono menos los seguros en otras columnas aparecera discriminado si se abono a capital o a intereses 
            filaActual.abonos = pvalorAbono;

            if (isUVR)
            {
                //Determinar si alcanza a abonar Intereses de Mora
                if (A >= interesMoraAdeudadoAcum)
                {
                    filaActual.abonoEnPesos = interesMoraAdeudadoAcum;
                    filaActual.saldoInteresMoraPesos = 0;
                    A = A - interesMoraAdeudadoAcum;
                    interesMoraAdeudadoAcum = 0; //alcanzo para pagar todos intereses
                }
                else
                {
                    //Si el abono es menor que el monto adeudado por mora entonces solo alcanza a abonar a Interes moratorios
                    interesMoraAdeudadoAcum = interesMoraAdeudadoAcum - A;
                    filaActual.abonoEnPesos = A;
                    filaActual.saldoInteresMoraPesos -= A;
					A = 0;
                }

                //Abono a intereses de plazo
                if (A >= interesPlazoAdeudadoAcum)
                {
                    //Alcanza para pagar todos los intereses de plazo
                    filaActual.abonoIntCtePesos = interesPlazoAdeudadoAcum;
                    filaActual.saldoInteresPlazoAcum = 0;
                    A = A - interesPlazoAdeudadoAcum;
                    interesPlazoAdeudadoAcum = 0; //alcanzo para pagar todos intereses
                }
                else if (A > 0)
                {
                    //No alcanzo para pagar todos los intereses de plazo
                    interesPlazoAdeudadoAcum = interesPlazoAdeudadoAcum - A;
                    filaActual.abonoIntCtePesos = A;
                    filaActual.saldoInteresPlazoAcum -= A;
                    A = 0;
                }

                //Abono a kapital en UVRs
                if (A > 0)
                {
                    valorUVR = _consultas.CalcTasa("UVR", filaActual.Desde).Result.ValorTasa ?? 0;
                    A = A / valorUVR;
                }

                //Si alcanza a pagar todo el capital
                if (A >= capital)
                {
                    k = capital;
                    capital = 0;
                    A = A - k;
                    flagTerminoLiquidacion = true; //Termino liquidacion pq los abonos alcanzaron para pagar intereses de mora, intereses de plazo y el capital
                    filaActual.abonoCapitalUVR = k; //Abono todo el capital que debia
                }
                else
                { //Si solo alcanza a pagar una parte del capital
                    k = capital;
                    capital = capital - A;
                    filaActual.abonoCapitalUVR = A;
                    A = 0;
                }

                //If a es mayor que cero entonces sobro Dinero para Devolver
                if (A > 0)
                    //Dinero a devolver en pesos
                    dineroDevolver = A * valorUVR;

                //Hubo abono recalcular los totales afectando la fila actual osea la ultima
                filaActual.capitalPesos -= filaActual.abonoCapitalUVR * filaActual.valorUVR;
                filaActual.totalPesos = filaActual.capitalPesos + filaActual.saldoInteresMoraPesos;
            }
            else {
                //Determinar si alcanza a abonar Intereses de Mora
                if (A >= interesMoraAdeudadoAcum)
                {
                    // Paga todos los intereses de mora
                    A = A - interesMoraAdeudadoAcum;
                    filaActual.abonoInteresMoraPesos = interesMoraAdeudadoAcum ;
                    interesMoraAdeudadoAcum = 0; //alcanzo para pagar todos intereses
                }
                else
                {
                    //Paga parte de los interes mora
                    interesMoraAdeudadoAcum = interesMoraAdeudadoAcum - A;
                    filaActual.abonoInteresMoraPesos = A;
                    A = 0;
                }

                //Abono a intereses de plazo
                if (A >= interesPlazoAdeudadoAcum)
                {
                    //Alcanza para pagar todos los intereses de plazo
                    A = A - interesPlazoAdeudadoAcum ;
                    filaActual.abonoIntCtePesos = interesPlazoAdeudadoAcum;
                    interesPlazoAdeudadoAcum = 0; //alcanzo para pagar todos intereses
                }
                else if (A > 0)
                {
                    //Paga parte de los intereses de plazo
                    interesPlazoAdeudadoAcum = interesPlazoAdeudadoAcum - A;
                    filaActual.abonoIntCtePesos = A;
                    A = 0;
                }

                //Si alcanza a pagar todo el capital
                if (A >= capital)
                {
                    k = capital;
                    capital = 0;
                    A = A - k;
                    flagTerminoLiquidacion = true; //Termino liquidacion pq los abonos alcanzaron para pagar intereses de mora, intereses de plazo y el capital
                    filaActual.subTotal = 0; //-- El saldo es cero
                    filaActual.abonoCapitalUVR = k; //Abono todo el capital que debia
                }
                else
                { //Si solo alcanza a pagar una parte del capital
                    k = capital;
                    capital = capital - A;
                    filaActual.abonoCapitalUVR = A;
                    A = 0;
                }

                //If a es mayor que cero entonces sobro Dinero para Devolver
                if (A > 0)
                {
                    //Dinero a devolver en pesos
                    dineroDevolver = A;
                    //Afectar el subtotal con el abono efectuado
                    //Sub total es igual al valor del abono menos lo que efectivamente se tuvo como descuento porque pagó más de lo que debía
                    filaActual.subTotal = filaActual.subTotal - (pvalorAbono - A);
                }
                else if (A <= 0)
                    //Subtotal es igual al subtotal menor el valor del abono completo pago menos de lo que debía
                    filaActual.subTotal = filaActual.subTotal - pvalorAbono;

                // Si quedó menor a cero fué porque pagó más de lo que debía y sobró plata
                if (filaActual.subTotal < 0)
                    filaActual.subTotal = 0;

                //Hubo abono recalcular los totales afectando la fila actual osea la ultima
                filaActual.saldoInteresPlazoAcum = interesPlazoAdeudadoAcum;
                filaActual.interesAdeudadoMoraAcum = interesMoraAdeudadoAcum;
                filaActual.capitalPesos -= filaActual.abonoCapitalUVR;
                filaActual.totalPesos = filaActual.capitalPesos + filaActual.saldoInteresMoraPesos;
            }
        }

        private class FechaValor
        {
            public DateTime Fecha;
            public double valorAbono;
            public double valorCapital;
            public bool acTenidoEncuenta;

            public FechaValor() { acTenidoEncuenta = false; }

            public FechaValor(DateTime fecha, double valor)
            {
                Fecha = fecha;
                valorAbono = valor;
                valorCapital = 0;
                acTenidoEncuenta = false;
            }
        }
    }
}
