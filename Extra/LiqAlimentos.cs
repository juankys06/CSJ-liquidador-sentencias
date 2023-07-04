using System;
using System.Collections.Generic;
using liquidador_web.Interfaces;
using Newtonsoft.Json;

namespace liquidador_web.Extra
{
    public class LiqAlimentos
    {
        private readonly IConsultas _consultas;

        /*
         * Parámetros que vienen del formulario
         */
        private bool _base360;
        private bool _aplicaSancion;
        private string _nombreTasaReferenciaInteresMora;
        private DateTime _fechaInicial;
        private DateTime _fechaLiquidacion;
        /// <summary>
        /// Interes Mora Pactado
        /// </summary>
        [JsonProperty]
        private double _interesMoraPactado;
        /// <summary>
        /// SALDO INICIAL: En este se van haciendo los descuentos cuando hay abonos y descuenta interese de plazo
        /// </summary>
        private double _interesPlazoAdedudadoAcum;
        /// <summary>
        /// SALDO INICIAL: En este se van haciendo los descuentos cuando hay abonos y descuenta interese de mora.
        /// </summary>
        private double _interesMoraAdeudadoAcum;
        private double capital;
        private double _CapitalesAcum;
        private double _abonosAcum;
        private double _cuotasExtAcum;
        private double _multasAcum;
        private double _mandamiento;

        private Liquidacion _filaAnterior;
        private Liquidacion _filaActual;

        private List<ExtMultasAbonos> _TablaAbonosYCapitales = new List<ExtMultasAbonos>();
        public List<Liquidacion> _TablaLiquidacion;

        private double _capitalInicial;
        /// <summary>
        /// Controla cuando se termino la liquidacion por efecto de haber pagado la deuda completa es decir cuando el subTotal es 0
        /// </summary>
        private bool _flagTerminoLiquidacion;
        /// <summary>
        /// Cuando los abonos superan la deuda hay que devolver el sobrante
        /// </summary>
        private double _dineroADevolver;
        /// <summary>
        /// Este no se van efectuando descuentos por abonos y siempre sera el total de lo que pago por intereses de mora
        /// </summary>
        private double _interesMoraAdeudadoAcumTotal;

        ///NUEVAS VARIABLES PARA LLEVAR EL SALDO A FAVOR 
        private double _saldoFavor;
        private double _capitalConSaldoFavor;

        public LiqAlimentos(IConsultas consultas, double mandamiento, double capital, DateTime fechaExigibilidad, DateTime fechaLiquidacion,
                          string nombreTasaReferenciaInteresMora, bool? base360, bool? aplicaSancion, double? interesMoraPactado,
                          DateTime?[] f_cuotas_ext, double?[] cuotas_ext, double?[] aSeguros, DateTime?[] f_multas,
                          double?[] multas, DateTime?[] f_cuotas, double?[] cuotas, DateTime?[] f_abonos = null,
                          double?[] abonos = null, string[] int_cuot = null, string[] int_multa = null)
        {
            _consultas = consultas;
            this.capital = capital;
            _mandamiento = mandamiento;
            _fechaInicial = fechaExigibilidad;
            _fechaLiquidacion = fechaLiquidacion;
            _nombreTasaReferenciaInteresMora = nombreTasaReferenciaInteresMora;

            ///INCIALIZACION DE LAS VARIABLES NUEVAS
            _saldoFavor = 0;
            _capitalConSaldoFavor = 0;

            _base360 = base360 ?? false;
            _aplicaSancion = aplicaSancion ?? false;
            _interesMoraPactado = interesMoraPactado ?? 0;

            if (abonos != null && f_abonos != null)
            {
                for (int i = 0; i < abonos.Length; i++)
                    if (_TablaAbonosYCapitales.Exists(f => f.Fecha == f_abonos[i]))
                        _TablaAbonosYCapitales.Find(f => f.Fecha == f_abonos[i]).abono += abonos[i] ?? 0;
                    else if (f_abonos[i] != DateTime.MinValue)
                        _TablaAbonosYCapitales.Add(new ExtMultasAbonos() { Fecha = f_abonos[i] ?? DateTime.MinValue, abono = abonos[i] ?? 0 });
            }

            if (cuotas != null && f_cuotas != null)
            {
                for (int i = 0; i < f_cuotas.Length; i++)
                    if (f_cuotas[i] != null)
                    {
                        if (_TablaAbonosYCapitales.Exists(f => f.Fecha == f_cuotas[i]))
                            _TablaAbonosYCapitales.Find(f => f.Fecha == f_cuotas[i]).cuota += cuotas[i] ?? 0;
                        else if (f_cuotas[i] != DateTime.MinValue)
                            _TablaAbonosYCapitales.Add(new ExtMultasAbonos() { Fecha = f_cuotas[i] ?? DateTime.Now, cuota = cuotas[i] ?? 0 });
                    }
            }

            if (f_cuotas_ext != null && cuotas_ext != null)
            {
                for (int i = 0; i < f_cuotas_ext.Length; i++)
                    if (f_cuotas_ext[i] != null)
                    {
                        if (int_cuot[i] == "on" && _TablaAbonosYCapitales.Exists(f => f.Fecha == f_cuotas_ext[i]))
                        {
                            _TablaAbonosYCapitales.Find(f => f.Fecha == f_cuotas_ext[i]).cuota += cuotas_ext[i] ?? 0;
                            _cuotasExtAcum += cuotas_ext[i] ?? 0;
                        }
                        else if (int_cuot[i] == "on" && f_cuotas_ext[i] != DateTime.MinValue)
                        {
                            _TablaAbonosYCapitales.Add(new ExtMultasAbonos() { Fecha = f_cuotas_ext[i] ?? DateTime.Now, cuota = cuotas_ext[i] ?? 0, extraordinario = true });
                            _cuotasExtAcum += cuotas_ext[i] ?? 0;
                        }
                        else if (f_cuotas_ext[i] != DateTime.MinValue)
                            _cuotasExtAcum += cuotas_ext[i] ?? 0;
                    }
            }

            if (f_multas != null && multas != null)
                for (int i = 0; i < f_multas.Length; i++)
                    if (f_multas[i] != null)
                    {
                        if (int_multa[i] == "on" && _TablaAbonosYCapitales.Exists(f => f.Fecha == f_multas[i]))
                        {
                            _TablaAbonosYCapitales.Find(f => f.Fecha == f_multas[i]).cuota += multas[i] ?? 0;
                            _multasAcum += multas[i] ?? 0;
                        }
                        else if (int_multa[i] == "on" && f_multas[i] != DateTime.MinValue)
                        {
                            _TablaAbonosYCapitales.Add(new ExtMultasAbonos() { Fecha = f_multas[i] ?? DateTime.Now, cuota = multas[i] ?? 0 });
                            _multasAcum += multas[i] ?? 0;
                        }
                        else if (f_multas[i] != DateTime.MinValue)
                            _multasAcum += multas[i] ?? 0;
                    }

            _TablaAbonosYCapitales.Sort((x, y) => DateTime.Compare(x.Fecha, y.Fecha));

            _capitalInicial = capital;
            _filaAnterior = null;
            _filaActual = null;
            _TablaLiquidacion = new List<Liquidacion>();
            _interesMoraAdeudadoAcum = 0;
            _interesMoraAdeudadoAcumTotal = 0;
            _dineroADevolver = 0;
            _flagTerminoLiquidacion = false;

            _interesPlazoAdedudadoAcum = 0;
            _interesMoraAdeudadoAcum = 0;
            _interesMoraPactado = interesMoraPactado ?? 0;
        }
        /// <summary>
        /// Resumen para liquidaciones con cuotas de administración
        /// </summary>
        /// <returns>Lista con el contenido del resumen.</returns>
        public List<(string, double)> Resumen()
        {
            double total_capital = _capitalInicial + _CapitalesAcum;
            double total_pagar = _interesMoraAdeudadoAcumTotal + total_capital + _cuotasExtAcum + _multasAcum;
            double montoSancion = total_capital * 0.2;

            return new List<(string, double)> {
                ("Mandamiento de Pago", _mandamiento),
                ("Total Capital", total_capital),
                ("Total Cuotas Extraordinarias", _cuotasExtAcum),
                ("Total Multas", _multasAcum),
                ("Total Interés Mora", _interesMoraAdeudadoAcumTotal),
                ("Total a Pagar", total_pagar+_mandamiento),
                ("- Abonos", _abonosAcum + AbonosNoTenidosEnCuenta()),
                ("Neto a Pagar", total_pagar+_mandamiento - _abonosAcum > 0 ? total_pagar+_mandamiento - _abonosAcum : 0)
            };
        }

        public void Liquidar()
        {
            int noDiasLiquidar, noDiasMes, noMeses;
            //DE AQUI EN ADELANTE SE CREAN LAS DIVISIONES PARA
            //LIQUIDAR POR CALENDARIO PRIMERO LOS DIAS INICIALES LUEGO MES A MES HASTA TERMINAR
            //MESES COMPLETOS Y LUEGO LOS DIAS FINALES
            DateTime fechatmp = _fechaInicial;
            //-- Liquidar dias iniciales
            if (_fechaInicial.Day > 1)
                if (_fechaInicial.Month == _fechaLiquidacion.Month && _fechaInicial.Year == _fechaLiquidacion.Year)
                {
                    //-- Se esta liquidando dentro del mismo mes y anho
                    noDiasLiquidar = (int)(_fechaLiquidacion - _fechaInicial).TotalDays + 1;
                    MirarSihayAbonosYCapitalYLiquidar(_fechaInicial, noDiasLiquidar);
                    return;
                }
                else
                {
                    //-- Se esta liquidando en meses diferentes
                    noDiasMes = DateTime.DaysInMonth(_fechaInicial.Year, _fechaInicial.Month);
                    //-- +1 porque cuenta el dia inicial pq es la fecha en que empieza la liquidacion
                    noDiasLiquidar = noDiasMes - _fechaInicial.Day + 1;
                    MirarSihayAbonosYCapitalYLiquidar(_fechaInicial, noDiasLiquidar);
                    fechatmp = _fechaInicial.AddDays(noDiasLiquidar);
                }
            //-- Liquidar mes a mes hasta terminar
            noMeses = (_fechaLiquidacion.Year - fechatmp.Year) * 12 + _fechaLiquidacion.Month - fechatmp.Month + (_fechaLiquidacion.Day >= fechatmp.Day ? 0 : -1);
            for (int i = 0; i < noMeses; i++)
            {
                noDiasLiquidar = DateTime.DaysInMonth(fechatmp.Year, fechatmp.Month);
                MirarSihayAbonosYCapitalYLiquidar(fechatmp, noDiasLiquidar);
                fechatmp = fechatmp.AddDays(noDiasLiquidar);
            }
            //-- Liquidar dias finales
            if (_fechaLiquidacion.Day >= 1)
            {
                MirarSihayAbonosYCapitalYLiquidar(fechatmp, _fechaLiquidacion.Day);
                fechatmp = fechatmp.AddDays(_fechaLiquidacion.Day);
            }
            //-- Finalizada la liquidacion
            //-- Generar la tabla con los datos globales
            //GenerarTablaAcumulados();
        }

        private void MirarSihayAbonosYCapitalYLiquidar(DateTime fechaInic, int NoDias)
        {
            //-- Si el flag fué activado ya se acabó la liquidación
            if (_flagTerminoLiquidacion)
                return;
            //-- Si hay abonos hay que liquidar un dia con el abono lo que requiere partir
            //-- El periodo en dos o hasta tres periodos indpendientes.
            //-- El abono puede ser el primer dia en un dia intermedio o el ultimo dia del periodo
            bool flagAbonos = false; //-- Control si hubo liquidación con abonos

            //-- Mirar si hay abono en el período
            //-- Partir el período y líquidar las particiones
            if (_TablaAbonosYCapitales.Count > 0)
                foreach (ExtMultasAbonos x in _TablaAbonosYCapitales)
                { //-- El Abono pertenece a este período
                    if (x.Fecha >= fechaInic && x.Fecha <= fechaInic.AddDays(NoDias - 1) && !x.acTenidoEncuenta)
                    {
                        if (x.Fecha == fechaInic)
                        {
                            //-- abono el primer dia del lapso a liquidar entonces liquidar el dia
                            _TablaLiquidacion.Add(LiquidarLapso(fechaInic, 1, x.abono, x.cuota));
                            Abonar(x.abono);
                            //-- Liquidar el resto de dias del periodo
                            DateTime fec = fechaInic.AddDays(1);
                            MirarSihayAbonosYCapitalYLiquidar(fec, NoDias - 1);
                            x.acTenidoEncuenta = true;
                            return;
                            //-- Salir sale porque cumplio el objetivo de liquidar un periodo
                            //-- Y llamo a mirar si hay abonos y liquidar para el siguiente periodo
                        }
                        else if (x.Fecha == fechaInic.AddDays(NoDias - 1))
                        {
                            //-- Abono el ultimo dia del periodo a liquidar
                            _TablaLiquidacion.Add(LiquidarLapso(fechaInic, NoDias - 1, 0, 0));
                            DateTime fec = fechaInic.AddDays(NoDias - 1);
                            _TablaLiquidacion.Add(LiquidarLapso(fec, 1, x.abono, x.cuota));
                            Abonar(x.abono);
                            x.acTenidoEncuenta = true;
                        }
                        else
                        {
                            //-- Abono en el intermedio del periodo a liquidar
                            int tmpNoDias;
                            tmpNoDias = x.Fecha.Day - fechaInic.Day;
                            _TablaLiquidacion.Add(LiquidarLapso(fechaInic, tmpNoDias, 0, 0));
                            NoDias -= tmpNoDias; //-- Descontar los dias ya liquidados

                            _TablaLiquidacion.Add(LiquidarLapso(x.Fecha, 1, x.abono, x.cuota));
                            NoDias -= 1; //-- Descontar los dias ya liquidados
                            Abonar(x.abono);

                            MirarSihayAbonosYCapitalYLiquidar(x.Fecha.AddDays(1), NoDias);
                            x.acTenidoEncuenta = true;
                            return;
                            //-- Salir. sale porque cumplio el objetivo de liquidar un periodo
                            //-- Y llamo a mirar si hay abonos y liquidar para el siguiente periodo
                        }
                        flagAbonos = true;
                        //-- Efectua el descuento en intereses y capital si alcanza
                    }//-- endif
                }

            //-- Si no hubo abonos entonces liquidar con fecha y dias 
            //-- que se encuentran en parametros
            if (!flagAbonos)
                _TablaLiquidacion.Add(LiquidarLapso(fechaInic, NoDias));
        }

        private Liquidacion LiquidarLapso(DateTime fechaInic, int NoDias, double valorAbono = 0, double valorKapital = 0)
        {
            Liquidacion rw = new Liquidacion();
            int NoDiasDelMes;
            double tasaVigenteTMP;

            if (_saldoFavor > 0 && valorKapital > 0)
            {
                _capitalConSaldoFavor = valorKapital;
                _saldoFavor -= valorKapital;
                valorKapital = 0;
                if(_saldoFavor < 0)
                {
                    valorKapital = _saldoFavor * -1;
                    _saldoFavor = 0;
                }
            }

            //-- Suma los capitales adicionados
            if (valorKapital > 0)
                capital += valorKapital;

            _CapitalesAcum += valorKapital;
            _abonosAcum += valorAbono;


            //-- Número de días a liquidar
            NoDiasDelMes = DateTime.DaysInMonth(fechaInic.Year, fechaInic.Month);
            rw.NoDias = NoDias;
            //-- Liquidar lapso correspondiente adicionar una fila con los resultados
            rw.Desde = fechaInic; rw.Hasta = fechaInic.AddDays(rw.NoDias - 1);

            //-- Colocar capital solo si hay capital nuevo o es el primero
            if (_filaAnterior == null)
            {                
                rw.capital = capital;
            }//-- AJUSTES PARA LLEVAR SALFO A FAVOR
            else if(valorKapital != 0 && _capitalConSaldoFavor == 0)
            {
                rw.capital = capital;
            }
            else if (_capitalConSaldoFavor > 0)
            {
                rw.capital = _capitalConSaldoFavor;
            }

            _capitalConSaldoFavor = 0;


            rw.CapitalALiquidar = capital;

            if (valorAbono != 0)
                rw.abonos = valorAbono;

            tasaVigenteTMP = (double)_consultas.CalcTasa("IBC", fechaInic).Result.ValorTasa;
            rw.TasaMaxima = tasaVigenteTMP * 1.5;
            if (_interesMoraPactado > 0)
                rw.TasaAnual = _interesMoraPactado;
            else if (_interesMoraPactado == 0)
                rw.TasaAnual = tasaVigenteTMP;

            //-- Si la fecha final es menor que la fecha final de la obligacion
            //-- Entonces se estan calculando interes corrientes o de plazo
            //-- Intereses de mora
            //-- Si hay interes pactado y este es menor que el interes maximo legal
            //-- entonces tener en cuenta la tasa pactada
            if (_interesMoraPactado > 0 && _interesMoraPactado <= rw.TasaMaxima)
            {
                rw.IntPactadoMayorAInteresLegal = false;
                rw.intAplicado = _interesMoraPactado;
            }
            else
            {
                //-- Si la tasa pactada es mayor que el interes maximo legal o menor o igual Cero
                rw.intAplicado = rw.TasaMaxima;
                if (_interesMoraPactado > 0)
                    rw.IntPactadoMayorAInteresLegal = true;
            }

            rw.InteresNominal = CalcularInteresDiario(rw.intAplicado);
            rw.InteresMoraPeriodo = rw.CapitalALiquidar * rw.InteresNominal * NoDias;
            _interesMoraAdeudadoAcum = _interesMoraAdeudadoAcum + rw.InteresMoraPeriodo;
            _interesMoraAdeudadoAcumTotal += rw.InteresMoraPeriodo;
            rw.interesAdeudadoMoraAcum = _interesMoraAdeudadoAcum;
            rw.subTotal = rw.CapitalALiquidar + _interesMoraAdeudadoAcum + _interesPlazoAdedudadoAcum;

            _filaAnterior = rw;
            _filaActual = rw;

            return rw;
        }

        private double CalcularInteresNominalMensual(double tasa)
        {
            return Math.Pow(1 + tasa / 100, (1 / 12) - 1);
        }

        /// <summary>
        /// Dada una tasa EA se calcula el interes diario
        /// </summary>
        /// <param name="tasa"></param>
        /// <returns></returns>
        private double CalcularInteresDiario(double tasa)
        {
            return Math.Pow((double)1 + tasa / (double)100, (double)1 / (double)365) - (double)1;
        }

        private void Abonar(double pValorAbono)
        {
            double A = pValorAbono;
            //Orden de Abonos:
            //Intereses acumulados de mora
            //Intereses Acumulados Corrientes
            //Capital Acumulado
            //Intereses Cuota Mes
            //Capital Cuota Mes

            //Interes Mora
            if (A > 0)
            {
                //-- Determinar si alcanza a abonar Intereses de Mora
                if (A >= _interesMoraAdeudadoAcum)
                {
                    A -= _interesMoraAdeudadoAcum;
                    _interesMoraAdeudadoAcum = 0; //-- Alcanzo para pagar todos los intereses
                }
                else
                {
                    _interesMoraAdeudadoAcum -= A;
                    A = 0;
                }
                //-- Abono a intereses de plazo
                if (A >= _interesPlazoAdedudadoAcum)
                {
                    A -= _interesPlazoAdedudadoAcum;
                    _interesPlazoAdedudadoAcum = 0; //-- Alcanzo para pagar todos los intereses
                }
                else if (A > 0)
                {
                    _interesPlazoAdedudadoAcum -= A;
                    A = 0;
                }
                //-- Abono a K
                if (A >= capital)
                {
                    double k = capital;
                    capital = 0;
                    A = A - k;
                    /// SE ELIMINA LA BANDERA DE PARAR LA LIQUIDACION AL QUEDAR EN CERO PARA EMPEZAR A LLEVAR EL SALDO A FAVOR
                    _saldoFavor += A;
                    ///_flagTerminoLiquidacion = true; //-- Termino la liquidacion porque los abonos alcanzaron para pagar intereses de mora, intereses de plazo y el capital
                    _filaActual.subTotal = 0; //-- El saldo es cero.
                }
                else
                {
                    double k = capital;
                    capital -= A;
                    A = 0;
                }
                /**
                 * If A > 0 then
                 * DineroDevolver = A
                 * endif 
                 */
                if (A > 0)
                {
                    //-- Dinero a devolver
                    _dineroADevolver = A;
                    //-- Afectar el subtotal con el abono efectuado
                    //-- Sub total es igual al valor del abono menos lo que efectivamente se tuvo como descuento pq pago mas de lo que debia
                    _filaActual.subTotal -= pValorAbono - A;
                }
                else if (A <= 0)
                    //-- SubTotal es igual al subtotal menor el valor del abono completo pago menos de lo que debia
                    _filaActual.subTotal -= pValorAbono;

                //-- Si quedo menor a cero fue pq pago mas de lo que debia y le sobro plata 
                if (_filaActual.subTotal < 0)
                    _filaActual.subTotal = 0;
            }
        }

        /// <summary>
        /// Suma los abonos no tenidos en cuenta para adicionarlos al saldo a devolver
        /// Ocurre cuando los abonos en un momento dado cubren toda la deuda y 
        /// hay Abonos posteriores 
        /// </summary>
        /// <returns>Total abonos sin tomar en cuenta.</returns>
        private double AbonosNoTenidosEnCuenta()
        {
            double resp = 0;
            foreach (ExtMultasAbonos x in _TablaAbonosYCapitales)
                if (!x.acTenidoEncuenta)
                    resp += x.abono;
            return resp;
        }

        private class ExtMultasAbonos
        {
            public DateTime Fecha;
            public double abono = 0;
            public double cuota = 0;
            public double multa = 0;
            public bool acTenidoEncuenta = false;
            public bool extraordinario = false;

            public ExtMultasAbonos() { }
        }
    }
}
