using System;
using System.Threading.Tasks;
using liquidador_web.Models;

namespace liquidador_web.Interfaces
{
    public interface IConsultas
    {
        /// <summary>
        /// Obtiene el/los procesos según el año y el código del proceso requerido
        /// </summary>
        /// <param name="año"></param>
        /// <param name="codProceso"></param>
        /// <returns></returns>
        Task<Procesos[]> ProcesosAdmin(string año, string codProceso);
        Task<Procesos[]> Procesos(string año, string codProceso, string codLocalidad, string codEntidad,string codEspecialida,string CodDespacho,string numero);
        /// <summary>
        /// Crea el proceso a partir de los parámetros obtenidos.
        /// </summary>
        /// <param name="ciudad"></param>
        /// <param name="entidad"></param>
        /// <param name="especialidad"></param>
        /// <param name="despacho"></param>
        /// <param name="año"></param>
        /// <param name="codProceso"></param>
        /// <param name="numero"></param>
        /// <param name="tipo"></param>
        /// <param name="area"></param>
        /// <param name="clase"></param>
        /// <param name="descripcion"></param>
        /// <param name="demandante_nombre"></param>
        /// <param name="demandante_id"></param>
        /// <param name="demandado_nombre"></param>
        /// <param name="demandado_id"></param>
        Task CrearProceso(string ciudad, string entidad, string especialidad, string despacho, string año, string codProceso, string numero, string tipo, string clase, string descripcion, string demandante_nombre, string demandante_id, string demandado_nombre, string demandado_id);
        /// <summary>
        /// Obtiene el último registro del tipo de tasa en la BD, para mostrarlo como referencia.
        /// </summary>
        /// <param name="moneda">Tipo de Moneda</param>
        /// <returns></returns>
        Task<Datasainte> Get_LastItem(string moneda);
        /// <summary>
        /// Obtiene la última tasa registrada en la BD.
        /// </summary>
        /// <returns>Retorna la última tasa registrada.</returns>
        Task<Datasainte> Get_LastItem();
        /// <summary>
        /// Retorna la tasa al cambio -moneda- solicitado.
        /// </summary>
        /// <param name="moneda">Tipo de Cambio a aplicar</param>
        /// <param name="Fecha">Día en que se consultó el cambio</param>
        /// <returns>Retorna la tasa solicitada.</returns>
        Task<Datasainte> CalcTasa(string moneda, DateTime Fecha);
        /// <summary>
        /// Obtiene los registros para la el tipo de tasa indicado.
        /// </summary>
        /// <param name="moneda">Tipo de Tasa</param>
        /// <returns>Valores de las distintas tasas en el tiempo.</returns>
        Task<Datasainte[]> getTasas(string moneda);
        /// <summary>
        /// Guarda la Liquidación ejecutada.
        /// </summary>
        /// <param name="cod_proc">Código del Proceso</param>
        /// <param name="t_liquidacion">Tipo de Liquidación</param>
        /// <param name="xml">Objeto Completo</param>
        /// <param name="cod_user">Usuario que ejecutó la operación</param>
        /// <returns>Retorna 1 en caso de haber guardado correctamente.</returns>
        Task<bool> EjecutarLiquidacion(string cod_proc, string t_liquidacion, string xml, string cod_user);
        /// <summary>
        /// Busca la última liquidación que se ejecuto en un proceso dado.
        /// </summary>
        /// <param name="LLAVPROC">Código del Proceso</param>
        /// <returns></returns>
        int ObtenerConsecutivoPorNoUnico(string LLAVPROC);
    }
}