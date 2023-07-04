using System;

namespace liquidador_web.Extra {
    /// <summary>
    /// Estructura que define la tabla que devuelve las Indemnizaciones.
    /// </summary>
    public struct Indemnizaciones
    {
        public DateTime fechaInicial;
        public DateTime fechaFinal;
        public int dias;
        public decimal capital;
        public decimal vrDia;
        public decimal totalMora;
        public decimal moraAcumulado;
    }
    /// <summary>
    /// Estructura que define la tabla que devuelve las Indexaciones.
    /// </summary>
    public struct Indexaciones
    {
        public DateTime fechaInicial;
        public DateTime fechaFinal;
        public double ipcInicial;
        public double ipcFinal;
        public decimal indexado;
        public decimal correccion;
    }
    /// <summary>
    /// Estructura que define la tabla que devuelve las Cotizaciones.
    /// </summary>
    public struct Cotizacion
    {
        public DateTime fechaInicial;
        public DateTime fechaFinal;
        public double salario;
        public int dias;
        public decimal valorDia;
        public decimal basePeriodo;
    }
    /// <summary>
    /// Estructura que indica las acciones mostradas en las Auditorias
    /// </summary>
    public static class Eventos
    {
        public const string Añadir = "Agregar";
        public const string Actualizar = "Actualizar";
        public const string Eliminar = "Eliminar";
    }
    /// <summary>
    /// Clase que retorna una lista menos cargada, de los datos guardados para cada proceso, en las liquidaciones.
    /// </summary>
    public class Guardados
    {
        public int id;
        public string llaveproc;
        public string tipo;
        public DateTime fecha;
        public string usuario;
        public bool autoGuardar;

        public Guardados(int id, string llaveproc, string tipo, DateTime fecha, string usuario, bool autoGuardar)
        {
            this.id = id;
            this.llaveproc = llaveproc;
            this.tipo = tipo;
            this.fecha = fecha;
            this.usuario = usuario;
            this.autoGuardar = autoGuardar;
        }
    }
}