using System;
using System.Collections.Generic;
using liquidador_web.Interfaces;

namespace liquidador_web.Extra
{
    public class Liquidador
    {
        private readonly IConsultas _consultas;

        /*
         * Parámetros que vienen del formulario
         */
        private readonly bool _base360;
        private readonly bool _aplicaSancion;
        private readonly string _nombreTasaReferenciaInteresCorriente;
        private readonly string _nombreTasaReferenciaInteresMora;
        private DateTime _fechaInicial;
        private readonly DateTime _fechaExibilidad;
        private DateTime _fechaLiquidacion;
        /// <summary>
        /// Interes Mora Pactado
        /// </summary>
        private readonly double _interesMoraPactado;
        /// <summary>
        /// Interes Corriente Pactado
        /// </summary>
        private readonly double _interesCorriente;
        /// <summary>
        /// En caso de elegir "variable" y un subtipo
        /// </summary>
        private readonly double _puntosInteresCorriente;
        /// <summary>
        /// En caso de elegir "variable" y un subtipo
        /// </summary>
        private readonly double _puntosInteresMora;
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
        private Liquidacion _filaAnterior;
        private Liquidacion _filaActual;
        private List<FechaValor> _TablaAbonosYCapitales = new List<FechaValor>();
        public List<Liquidacion> _TablaLiquidacion;
        private readonly double _capitalInicial;
        private double subTotalAnterior;
        /// <summary>
        /// Controla cuando se termino la liquidacion por efecto de haber pagado la deuda completa es decir cuando el subTotal es 0
        /// </summary>
        private bool _flagTerminoLiquidacion;
        /// <summary>
        /// Cuando los abonos superan la deuda hay que devolver el sobrante
        /// </summary>
        private double _dineroADevolver;
        /// <summary>
        /// Este no se van efectuando descuentos por abonos y siempre sera el total de lo que paga por intereses de plazo
        /// </summary>
        private double _interesPlazoAdeudadoAcumTotal;
        /// <summary>
        /// Este no se van efectuando descuentos por abonos y siempre sera el total de lo que pago por intereses de mora
        /// </summary>
        private double _interesMoraAdeudadoAcumTotal;
        
        /// <summary>
        /// Constructor para el liquidador singular.
        /// </summary>
        /// <param name="consultas">Interfaz para la BD</param>
        /// <param name="capital">Capital a Liquidar</param>
        /// <param name="base360">Año bisiesto</param>
        /// <param name="aplicaSancion">Sanción del 20% según Art. 731</param>
        /// <param name="_interesPlazoAdedudadoAcum"></param>
        /// <param name="_interesMoraAdeudadoAcum"></param>
        /// <param name="nombreTasaReferenciaInteresCorriente">Tasa de referencia dentro del plazo</param>
        /// <param name="nombreTasaReferenciaInteresMora">Tasa de referencia como moroso</param>
        /// <param name="abonos">Abonos efectuados</param>
        /// <param name="f_abonos">Fechas de los Abonos efectuados</param>
        /// <param name="capitales"></param>
        /// <param name="f_capitales"></param>
        /// <param name="fechaInicial">Fecha de Inicio Obligación</param>
        /// <param name="fechaExigibilidad">Fecha en que debe estar pagado</param>
        /// <param name="fechaLiquidacion">Fecha en que se liquidó</param>
        /// <param name="interesMoraPactado">Interés pactado en caso de que no cumpla con la fecha</param>
        /// <param name="interesCorriente">Interés pactado dentro del período</param>
        /// <param name="_puntosInteresCorriente"></param>
        /// <param name="_puntosInteresMora"></param>
        public Liquidador(IConsultas consultas, double capital,
                          bool base360, string aplicaSancion, double _interesPlazoAdedudadoAcum, double _interesMoraAdeudadoAcum,
                          string nombreTasaReferenciaInteresCorriente, string nombreTasaReferenciaInteresMora, double?[] abonos,
                          DateTime?[] f_abonos, double?[] capitales, DateTime?[] f_capitales,
                          DateTime fechaInicial, DateTime fechaExigibilidad, DateTime fechaLiquidacion, 
                          double? interesMoraPactado, double? interesCorriente, double? _puntosInteresCorriente, double? _puntosInteresMora)
        {
            _consultas = consultas;
            this.capital = capital;
            _base360 = base360;
            _aplicaSancion = aplicaSancion != null ? true: false ;
            _interesPlazoAdeudadoAcumTotal = _interesPlazoAdedudadoAcum;
            this._interesPlazoAdedudadoAcum = _interesPlazoAdedudadoAcum;
            _interesMoraAdeudadoAcumTotal = _interesMoraAdeudadoAcum;
            this._interesMoraAdeudadoAcum = _interesMoraAdeudadoAcum;
            _nombreTasaReferenciaInteresCorriente = nombreTasaReferenciaInteresCorriente;
            _nombreTasaReferenciaInteresMora = nombreTasaReferenciaInteresMora;
            _fechaInicial = fechaInicial;
            _fechaExibilidad = fechaExigibilidad;
            _fechaLiquidacion = fechaLiquidacion;
            _interesMoraPactado = interesMoraPactado ?? 0;
            _interesCorriente = interesCorriente ?? 0;
            this._puntosInteresCorriente = _puntosInteresCorriente ?? 0;
            this._puntosInteresMora = _puntosInteresMora ?? 0;

            if (abonos != null && f_abonos != null)
            {
                for (int i = 0; i < abonos.Length; i++)
                    if (abonos[i] != null)
                    {
                        if (_TablaAbonosYCapitales.Exists(f => f.Fecha == f_abonos[i]))
                            _TablaAbonosYCapitales.Find(f => f.Fecha == f_abonos[i]).valorAbono += abonos[i] ?? 0;
                        else
                            _TablaAbonosYCapitales.Add(new FechaValor(f_abonos[i] ?? DateTime.Now, abonos[i] ?? 0));
                    }
            }

            if (capitales != null && f_capitales != null)
            {
                for (int i = 0; i < f_capitales.Length; i++)
                    if (f_capitales[i] != null)
                    {
                        if (_TablaAbonosYCapitales.Exists(f => f.Fecha == f_capitales[i]))
                            _TablaAbonosYCapitales.Find(f => f.Fecha == f_capitales[i]).valorCapital += capitales[i] ?? 0;
                        else
                            _TablaAbonosYCapitales.Add(new FechaValor() { Fecha = f_capitales[i] ?? DateTime.Now, valorCapital = capitales[i] ?? 0 });
                    }
            }
            _TablaAbonosYCapitales.Sort((x, y) => DateTime.Compare(x.Fecha, y.Fecha));

            //-- Propios de la clase
            _capitalInicial = capital;
            _filaAnterior = null ;
            _filaActual = null ;
            _TablaLiquidacion = new List<Liquidacion>();
            _interesMoraAdeudadoAcum = 0 ;
            _interesPlazoAdedudadoAcum = 0 ;
            _dineroADevolver = 0;
            _flagTerminoLiquidacion = false;
            subTotalAnterior = 0;
        }
        /// <summary>
        /// Constructor Liquidaciones Múltiples
        /// </summary>
        /// <param name="consultas">Interfaz para la BD</param>
        /// <param name="capital">Capital a Liquidar</param>
        /// <param name="fechaInicial">Fecha de Inicio de Obligación</param>
        /// <param name="fechaExigibilidad">Fecha en que debe estar pagado</param>
        /// <param name="fechaLiquidacion">Fecha en que se liquidó</param>
        /// <param name="base360">Año bisiesto</param>
        /// <param name="aplicaSancion">Sanción del 20% según Art. 731</param>
        /// <param name="interesMoraPactado">Interés pactado en caso de que no cumpla con la fecha</param>
        /// <param name="interesCorriente">Interés pactado dentro del período</param>
        /// <param name="_puntosInteresCorriente"></param>
        /// <param name="_puntosInteresMora"></param>
        /// <param name="nombreTasaReferenciaInteresCorriente">Tasa de referencia dentro del plazo</param>
        /// <param name="nombreTasaReferenciaInteresMora">Tasa de referencia como moroso</param>
        /// <param name="abonos">Abonos efectuados</param>
        /// <param name="f_abonos">Fechas de los Abonos efectuados</param>
        public Liquidador(IConsultas consultas, double capital, DateTime fechaInicial, DateTime fechaExigibilidad, DateTime fechaLiquidacion,
                          bool? base360, bool? aplicaSancion, double? interesMoraPactado, double? interesCorriente, double? _puntosInteresCorriente, double? _puntosInteresMora,
                          string nombreTasaReferenciaInteresCorriente = null, string nombreTasaReferenciaInteresMora = null, 
                          double[] abonos = null, DateTime[] f_abonos = null)
        {
            _consultas = consultas;
            this.capital = capital;
            _fechaInicial = fechaInicial;
            _fechaExibilidad = fechaExigibilidad;
            _fechaLiquidacion = fechaLiquidacion;
            _base360 = base360 ?? false;
            _aplicaSancion = aplicaSancion ?? false;
            _nombreTasaReferenciaInteresCorriente = nombreTasaReferenciaInteresCorriente.ToUpper() ?? "IBC";
            _nombreTasaReferenciaInteresMora = nombreTasaReferenciaInteresMora.ToUpper() ?? "IBC";
            _interesMoraPactado = interesMoraPactado ?? 0;
            _interesCorriente = interesCorriente ?? 0;
            this._puntosInteresCorriente = _puntosInteresCorriente ?? 0;
            this._puntosInteresMora = _puntosInteresMora ?? 0;
            
            if(abonos != null && f_abonos != null)
                for (int i = 0; i < abonos.Length; i++)
                    if (_TablaAbonosYCapitales.Exists(f => f.Fecha == f_abonos[i]))
                        _TablaAbonosYCapitales.Find(f => f.Fecha == f_abonos[i]).valorAbono += abonos[i];
                    else if(abonos[i] > 0)
                        _TablaAbonosYCapitales.Add(new FechaValor(f_abonos[i], abonos[i]));

            _TablaAbonosYCapitales.Sort((x, y) => DateTime.Compare(x.Fecha, y.Fecha));

            //-- Propios de la clase
            _capitalInicial = capital;
            _filaAnterior = null;
            _filaActual = null;
            _TablaLiquidacion = new List<Liquidacion>();
            _interesMoraAdeudadoAcum = 0;
            _interesMoraAdeudadoAcumTotal = 0;
            _dineroADevolver = 0;
            _flagTerminoLiquidacion = false;
            subTotalAnterior = 0;
        }
        /// <summary>
        /// Resumen para liquidaciones singulares y múltiples
        /// </summary>
        /// <returns>Lista con el contenido del resumen.</returns>
        public List<(string, double)> Resumen()
        {
            double total_capital = _capitalInicial + _CapitalesAcum;
            double total_pagar = _interesMoraAdeudadoAcumTotal + total_capital + _interesPlazoAdeudadoAcumTotal;
            double montoSancion = total_capital * 0.2;

            if(!_aplicaSancion){

                if((int)AbonosNoTenidosEnCuenta()!=0){
	                return new List<(string, double)> {
                    ("Capital", _capitalInicial),
                    ("Capitales Adicionados", _CapitalesAcum),
                    ("Total Capital", total_capital),
                    ("Total Interés de Plazo", _interesPlazoAdeudadoAcumTotal),
                    ("Total Interés Mora", _interesMoraAdeudadoAcumTotal),
                    ("Total a Pagar", total_pagar),
                    ("- Abonos", _abonosAcum + AbonosNoTenidosEnCuenta()),
                    ("Neto a Pagar", total_pagar - _abonosAcum > 0 ? total_pagar - _abonosAcum : 0),
                    ("Saldo devolver al deudor", _dineroADevolver + AbonosNoTenidosEnCuenta())
                	};
	            }
	            else{
	            	return new List<(string, double)> {
                    ("Capital", _capitalInicial),
                    ("Capitales Adicionados", _CapitalesAcum),
                    ("Total Capital", total_capital),
                    ("Total Interés de Plazo", _interesPlazoAdeudadoAcumTotal),
                    ("Total Interés Mora", _interesMoraAdeudadoAcumTotal),
                    ("Total a Pagar", total_pagar),
                    ("- Abonos", _abonosAcum + AbonosNoTenidosEnCuenta()),
                    ("Neto a Pagar", total_pagar - _abonosAcum > 0 ? total_pagar - _abonosAcum : 0)
                	};
	            }
            }
            else{
                
                if((int)AbonosNoTenidosEnCuenta()!=0){
	                return new List<(string, double)> {
                    ("Capital", _capitalInicial),
                    ("Capitales Adicionados", _CapitalesAcum),
                    ("Total Capital", total_capital),
                    ("Total Interés de Plazo", _interesPlazoAdeudadoAcumTotal),
                    ("Total Interés Mora", _interesMoraAdeudadoAcumTotal),
                    ("Sanción Artículo 731 CC", montoSancion),
                    ("Total a Pagar", total_pagar + montoSancion),
                    ("- Abonos", _abonosAcum + AbonosNoTenidosEnCuenta()),
                    ("Neto a Pagar", total_pagar + montoSancion - _abonosAcum > 0 ? total_pagar + montoSancion - _abonosAcum : 0),
                    ("Saldo devolver al deudor", _dineroADevolver + AbonosNoTenidosEnCuenta())
                	};
	            }
	            else{
	            	return new List<(string, double)> {
                    ("Capital", _capitalInicial),
                    ("Capitales Adicionados", _CapitalesAcum),
                    ("Total Capital", total_capital),
                    ("Total Interés de Plazo", _interesPlazoAdeudadoAcumTotal),
                    ("Total Interés Mora", _interesMoraAdeudadoAcumTotal),
                    ("Sanción Artículo 731 CC", montoSancion),
                    ("Total a Pagar", total_pagar + montoSancion),
                    ("- Abonos", _abonosAcum + AbonosNoTenidosEnCuenta()),
                    ("Neto a Pagar", total_pagar + montoSancion - _abonosAcum > 0 ? total_pagar + montoSancion - _abonosAcum : 0)
                };
	            }
            }
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
                if(_fechaInicial.Month == _fechaLiquidacion.Month && _fechaInicial.Year == _fechaLiquidacion.Year)
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
            for(int i = 0; i < noMeses; i++)
            {
                noDiasLiquidar = DateTime.DaysInMonth(fechatmp.Year, fechatmp.Month);
                MirarSihayAbonosYCapitalYLiquidar(fechatmp, noDiasLiquidar);
                fechatmp = fechatmp.AddDays(noDiasLiquidar);
            }
            //-- Liquidar dias finales
            if( _fechaLiquidacion.Day >= 1)
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

            bool ControlSiYaSePasoADeInteresDePlazoAInteresDeMora = false;
            
            if (_fechaExibilidad > fechaInic && _fechaExibilidad < fechaInic.AddDays(NoDias) && !ControlSiYaSePasoADeInteresDePlazoAInteresDeMora)
            {
                int NoDiasALiq = (_fechaExibilidad - fechaInic).Days;
                ControlSiYaSePasoADeInteresDePlazoAInteresDeMora = true;
                MirarSihayAbonosYCapitalYLiquidar(fechaInic, NoDiasALiq);
                MirarSihayAbonosYCapitalYLiquidar(fechaInic.AddDays(NoDiasALiq), NoDias - NoDiasALiq);
                return;
            }

            //-- Mirar si hay abono en el período
            //-- Partir el período y líquidar las particiones
            if (_TablaAbonosYCapitales.Count > 0)
                foreach (FechaValor x in _TablaAbonosYCapitales)
                { //-- El Abono pertenece a este período
                    if (x.Fecha >= fechaInic && x.Fecha <= fechaInic.AddDays(NoDias - 1) && !x.acTenidoEncuenta)
                    {
                        if (x.Fecha == fechaInic)
                        {
                            //-- abono el primer dia del lapso a liquidar entonces liquidar el dia
                            _TablaLiquidacion.Add(LiquidarLapso(fechaInic, 1, x.valorAbono, x.valorCapital));
                            Abonar(x.valorAbono);
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
                            _TablaLiquidacion.Add(LiquidarLapso(fec, 1, x.valorAbono, x.valorCapital));
                            Abonar(x.valorAbono);
                            x.acTenidoEncuenta = true;
                        }
                        else
                        {
                            //-- Abono en el intermedio del periodo a liquidar
                            int tmpNoDias;
                            tmpNoDias = x.Fecha.Day - fechaInic.Day;
                            _TablaLiquidacion.Add(LiquidarLapso(fechaInic, tmpNoDias, 0, 0));
                            NoDias -= tmpNoDias; //-- Descontar los dias ya liquidados

                            _TablaLiquidacion.Add(LiquidarLapso(x.Fecha, 1, x.valorAbono, x.valorCapital));
                            NoDias -= 1; //-- Descontar los dias ya liquidados
                            Abonar(x.valorAbono);

                            MirarSihayAbonosYCapitalYLiquidar(x.Fecha.AddDays(1), NoDias);
                            x.acTenidoEncuenta = true;
                            return;
                            //-- Salir. sale porque cumplio el objetivo de liquidar un periodo
                            //-- Y llamo a mirar si hay abonos y liquidar para el siguiente periodo
                        }
                        flagAbonos = true ;
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

            //-- Suma los capitales adicionados
            if (valorKapital > 0)
                capital += valorKapital;

            _CapitalesAcum += valorKapital;
            _abonosAcum += valorAbono;

            //-- Liquidar lapso correspondiente adicionar una fila con los resultados
            rw.Desde = fechaInic; rw.Hasta = fechaInic.AddDays(NoDias - 1);

            //-- Número de días a liquidar
            if (_base360)
            {
                //-- Si la base es de 360 meses de 30
                NoDiasDelMes = DateTime.DaysInMonth(fechaInic.Year, fechaInic.Month);

                //-- Si se está liquidando el mes completo
                if (NoDiasDelMes == NoDias)
                {
                    rw.NoDias = NoDiasDelMes;
                    NoDias = 30; //-- En NoDias siempre será 30 para meses completos
                    rw.NoDias = 30;
                }else
                    rw.NoDias = NoDias;
            }else //-- Si se liquidan todos los días
                rw.NoDias = NoDias;

            //-- Colocar capital solo si hay capital nuevo o es el primero
            if (_filaAnterior == null)
                rw.capital = capital;
            else if (valorKapital != 0)
                rw.capital = valorKapital;
            rw.CapitalALiquidar = capital;
            if (valorAbono != 0)
                rw.abonos = valorAbono;

            if (rw.Hasta < _fechaExibilidad)
            {
                EstablecerTasasDeInteres("PLAZO", rw);
                rw.intPlazoPeriodo = capital * rw.InteresNominal * NoDias;
                _interesPlazoAdedudadoAcum += rw.intPlazoPeriodo;
                _interesPlazoAdeudadoAcumTotal += rw.intPlazoPeriodo;
                rw.saldoInteresPlazoAcum = _interesPlazoAdedudadoAcum; //-- Muestre el acumulado de interes de plazo
                rw.subTotal = capital + _interesPlazoAdedudadoAcum;
                subTotalAnterior = rw.subTotal;
            }
            else { //-- Intereses mora
                EstablecerTasasDeInteres("MORA", rw);
                rw.InteresMoraPeriodo = capital * rw.InteresNominal * NoDias;

                _interesMoraAdeudadoAcum += rw.InteresMoraPeriodo;
                _interesMoraAdeudadoAcumTotal += rw.InteresMoraPeriodo;
                rw.interesAdeudadoMoraAcum += _interesMoraAdeudadoAcum;
                rw.subTotal = capital + _interesMoraAdeudadoAcum + _interesPlazoAdedudadoAcum;

                //-- Muestre el total de intereses de plazo adeudado
                rw.saldoInteresPlazoAcum = _interesPlazoAdedudadoAcum;
            }

            _filaAnterior = rw;
            _filaActual = rw;

            return rw;
        }

        /// <summary>
        /// Establece las columnas relativas a las tasas de interes tanto para interes de plazo como mora
        /// </summary>
        /// <param name="opcion">Mora o Plazo</param>
        /// <param name="rw">Fila en la cual se deben colocar los valores de las tasas de interes calculadas</param>
        private void EstablecerTasasDeInteres(string opcion, Liquidacion rw)
        {
            double tasaMaximaIBC;
            double valorTasa;

            if ( (opcion == "PLAZO" && _nombreTasaReferenciaInteresCorriente == "MIC") || (opcion == "MORA" && _nombreTasaReferenciaInteresMora == "MIC") )
            {
                //-- Tasa Máxima para MICROCRÉDITO
                tasaMaximaIBC = (double)_consultas.CalcTasa("MIC", rw.Desde).Result.ValorTasa;
                rw.TasaMaxima = tasaMaximaIBC * 1.5;
            } else {
                tasaMaximaIBC = (double)_consultas.CalcTasa("IBC", rw.Desde).Result.ValorTasa;
                rw.TasaMaxima = tasaMaximaIBC * 1.5;
            }

            if (opcion == "PLAZO")
                switch (_nombreTasaReferenciaInteresCorriente) {
                    case "IBC":
                    case "MIC":
                        rw.TasaAnual = tasaMaximaIBC;
                        rw.intAplicado = tasaMaximaIBC;
                        rw.InteresNominal = CalcularInteresDiario(rw.TasaAnual);
                        break;
                    case "PACTADO":
                        //-- El interes corriente no puede ser mayor a 1.5 veces el interes legal
                        rw.TasaAnual = _interesCorriente;
                        if (_interesCorriente >= 0 && _interesCorriente <= rw.TasaMaxima)
                        {
                            rw.intAplicado = _interesCorriente;
                            rw.InteresNominal = CalcularInteresDiario(_interesCorriente);
                            //-- IMPORTANTE: si viene cero queda cero
                        }
                        else {
                            rw.intAplicado = rw.TasaMaxima;
                            rw.InteresNominal = CalcularInteresDiario(rw.TasaMaxima);
                        }
                        break;
                    case "IPC":
                        /* El valor de la tasa se calcula como la variacion anual de la ipc
                        entre el valor del ipc de la fecha que se esta liquidando 
                        y el mismo mes del año Inmediatamente anterior */
                        double valorTasaAnoAnterior = (double)_consultas.CalcTasa(_nombreTasaReferenciaInteresCorriente,
                                                                           rw.Desde.AddYears(-1).AddMonths(-1)).Result.ValorTasa;
                        valorTasa = (double)_consultas.CalcTasa(_nombreTasaReferenciaInteresCorriente,
                                                                rw.Desde.AddMonths(-1)).Result.ValorTasa;
                        valorTasa = (valorTasa / valorTasaAnoAnterior - 1) * 100;
                        valorTasa = valorTasa + _puntosInteresCorriente;
                        rw.TasaAnual = valorTasa;
                        if (valorTasa > 0 && valorTasa <= rw.TasaMaxima) {
                            rw.intAplicado = valorTasa;
                            rw.InteresNominal = CalcularInteresDiario(valorTasa);
                        }
                        else {
                            rw.intAplicado = rw.TasaMaxima;
                            rw.InteresNominal = CalcularInteresDiario(tasaMaximaIBC);
                        }
                        break;
                    case "DTF":
                        valorTasa = (double)_consultas.CalcTasa(_nombreTasaReferenciaInteresCorriente,
                                                                rw.Desde.AddMonths(-1)).Result.ValorTasa;
                        valorTasa += _puntosInteresCorriente;
                        rw.TasaAnual = valorTasa;
                        if (valorTasa > 0 && valorTasa <= rw.TasaMaxima)
                        {
                            rw.intAplicado = valorTasa;
                            rw.InteresNominal = CalcularInteresDiario(valorTasa);
                        }
                        else {
                            rw.intAplicado = rw.TasaMaxima;
                            rw.InteresNominal = CalcularInteresDiario(tasaMaximaIBC);
                        }
                        break;
                }
            else if (opcion == "MORA")
                switch (_nombreTasaReferenciaInteresMora) {
                    case "IBC":
                    case "MIC":
                        rw.TasaAnual = tasaMaximaIBC * 1.5;
                        rw.intAplicado = rw.TasaAnual;
                        rw.InteresNominal = CalcularInteresDiario(rw.TasaAnual);
                        break;
                    case "PACTADO":
                        //-- Si hay interes pactado y este es menor que el interes maximo legal
                        //-- entonces tener en cuenta la tasa pactada
                        rw.TasaAnual = _interesMoraPactado;
                        if (_interesMoraPactado > 0 && _interesMoraPactado < rw.TasaMaxima)
                        {
                            rw.IntPactadoMayorAInteresLegal = false;
                            rw.intAplicado = _interesMoraPactado;
                            rw.InteresNominal = CalcularInteresDiario(rw.intAplicado);
                        }else if (_interesMoraPactado > 0 && _interesMoraPactado >= rw.TasaMaxima) {
                            //-- Si la tasa pactada es mayor que el interes maximo legal
                            rw.intAplicado = rw.TasaMaxima;
                            if (_interesMoraPactado > 0)
                                rw.IntPactadoMayorAInteresLegal = true;
                            rw.InteresNominal = CalcularInteresDiario(rw.intAplicado);
                        }
                        break;
                    case "IPC":
                        /*
                         * El valor de la tasa se calcula como la variacion anual de la IPC
                         * entre el valor del IPC de la fecha que se esta liquidando
                         * y el mismo mes del año inmediatamente anterior.
                         * **/
                        double valorTasaAnoAnterior = 0;
                        valorTasaAnoAnterior = (double)_consultas.CalcTasa(_nombreTasaReferenciaInteresMora,
                                                                            rw.Hasta.AddYears(-1)).Result.ValorTasa;
                        valorTasa = (double)_consultas.CalcTasa(_nombreTasaReferenciaInteresMora, rw.Hasta).Result.ValorTasa;
                        valorTasa = (valorTasa / valorTasaAnoAnterior - 1) * 100;
                        valorTasa += _puntosInteresMora;
                        rw.TasaAnual = valorTasa;
                        if (valorTasa > 0 && valorTasa <= rw.TasaMaxima)
                        {
                            rw.intAplicado = valorTasa;
                            rw.InteresNominal = CalcularInteresDiario(valorTasa);
                        }
                        else {
                            rw.intAplicado = rw.TasaMaxima;
                            rw.InteresNominal = CalcularInteresDiario(rw.TasaMaxima);
                        }
                        break;
                    case "DTF":
                        valorTasa = (double)_consultas.CalcTasa(_nombreTasaReferenciaInteresMora,
                                                                rw.Hasta).Result.ValorTasa;
                        valorTasa += _puntosInteresMora;
                        rw.TasaAnual = valorTasa;
                        if (valorTasa > 0 && valorTasa <= rw.TasaMaxima) {
                            rw.intAplicado = valorTasa;
                            rw.InteresNominal = CalcularInteresDiario(valorTasa);
                        }
                        else {
                            rw.intAplicado = rw.TasaMaxima;
                            rw.InteresNominal = CalcularInteresDiario(rw.TasaMaxima);
                        }
                        break;
                    case "AUMENTADO":
                        /**
                         * Cuando el interes moratorio es de 1.5 o 2 veces el
                         * interes corriente o algun otro factor mayor a 1
                         ***/
                        EstablecerTasasDeInteres("PLAZO", rw);
                        rw.TasaAnual = rw.intAplicado * _puntosInteresMora;
                        rw.intAplicado *= _puntosInteresMora;
                        if (rw.intAplicado > rw.TasaMaxima)
                            rw.intAplicado = rw.TasaMaxima;
                        //-- Validar aquí que no exceda del máximo
                        rw.InteresNominal = CalcularInteresDiario(rw.intAplicado);
                        break;
                }//-- end switch
        }

        private double CalcularInteresNominalMensual(double tasa)
        {
            return Math.Pow(1+tasa/100,(1/12)-1) ;
        }

        /// <summary>
        /// Dada una tasa EA se calcula el interes diario
        /// </summary>
        /// <param name="tasa"></param>
        /// <returns></returns>
        private double CalcularInteresDiario(double tasa) {
            return _base360 ? Math.Pow((double)1 + tasa / (double)100, (double)1 / (double)360) - (double)1 : Math.Pow((double)1 + tasa / (double)100, (double)1 / (double)365) - (double)1;
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
                if (A >= _interesMoraAdeudadoAcum) {
                    A -= _interesMoraAdeudadoAcum;
                    _interesMoraAdeudadoAcum = 0; //-- Alcanzo para pagar todos los intereses
                } else {
                    _interesMoraAdeudadoAcum -= A;
                    A = 0;
                }
                //-- Abono a intereses de plazo
                if (A >= _interesPlazoAdedudadoAcum) {
                    A -= _interesPlazoAdedudadoAcum;
                    _interesPlazoAdedudadoAcum = 0; //-- Alcanzo para pagar todos los intereses
                } else if (A > 0) {
                    _interesPlazoAdedudadoAcum -= A;
                    A = 0;
                }
                //-- Abono a K
                if (A >= capital) {
                    double k = capital;
                    capital = 0;
                    A = A - k;
                    _flagTerminoLiquidacion = true; //-- Termino la liquidacion porque los abonos alcanzaron para pagar intereses de mora, intereses de plazo y el capital
                    _filaActual.subTotal = 0; //-- El saldo es cero.
                } else {
                    double k = capital;
                    capital -= A;
                    A = 0;
                }
                /**
                 * If A > 0 then
                 * DineroDevolver = A
                 * endif 
                 */
                if (A > 0) {
                    //-- Dinero a devolver
                    _dineroADevolver = A;
                    //-- Afectar el subtotal con el abono efectuado
                    //-- Sub total es igual al valor del abono menos lo que efectivamente se tuvo como descuento pq pago mas de lo que debia
                    _filaActual.subTotal -= pValorAbono - A ;
                } else if (A <= 0)
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
            foreach (FechaValor x in _TablaAbonosYCapitales)
                if (!x.acTenidoEncuenta)
                    resp += x.valorAbono;
            return resp;
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

    public class Liquidacion
    {
        public DateTime Desde;
        public DateTime Hasta;
        public int NoDias;
        public double TasaAnual;
        public double TasaMaxima;
        public double InteresNominal;
        public double capital;
        public double interesAdeudadoMoraAcum;
        public double CapitalALiquidar;
        public double InteresMoraPeriodo;
        public double abonos;
        public double subTotal;
        public bool IntPactadoMayorAInteresLegal;
        public double intPlazoPeriodo;
        public double intAplicado;
        public double saldoInteresPlazoAcum;

        //-- Campos para Hipotecarios en UVR
        public double valorUVR;
        public double capitalPesos;
        public double interesMoraPeriodoPesos;
        public double saldoInteresMoraPesos;
        public double totalPesos;
        public double abonoEnPesos;
        public double abonoIntCtePesos;
        public double abonoCapitalUVR;

        //-- Campos para Hipotecarios en Pesos
        public double abonoInteresMoraPesos;

        //-- Campo de saldo a favor
        public double saldoFavor;
    }
}
