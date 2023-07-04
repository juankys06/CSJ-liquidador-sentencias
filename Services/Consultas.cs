using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using liquidador_web.Data;
using liquidador_web.Interfaces;
using liquidador_web.Models;
using System.Linq;

namespace liquidador_web.Services
{
    public class Consultas : IConsultas
    {
        private readonly ApplicationDbContext _context;

        public Consultas(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Procesos[]> ProcesosAdmin(string año, string codProceso)
        {
            var taño = new SqlParameter("@año", año);
            var tproceso = new SqlParameter("@codProceso", codProceso);

            return _context.Procesos.FromSql("EXECUTE dbo.ProcesosAdmin @año, @codProceso", taño, tproceso).ToArrayAsync();
        }
        public Task<Procesos[]> Procesos(string año, string codProceso, string codLocalidad,string codEntidad, string codEspecialidad, string codDespacho, string numero)
        {
            var taño = new SqlParameter("@año", año);
            var tproceso = new SqlParameter("@codProceso", codProceso);
            var tlocalidad = new SqlParameter("@codLocalidad", codLocalidad);
            var tentidad = new SqlParameter("@codEntidad", codEntidad);
            var tespecialidad = new SqlParameter("@codEspecialidad", codEspecialidad);
            var tdespacho = new SqlParameter("@codDespacho", codDespacho);
            var tnumero= new SqlParameter("@numero", numero);

            return _context.Procesos.FromSql("EXECUTE dbo.Procesos @año, @codProceso, @codLocalidad,@codEntidad,@codEspecialidad,@codDespacho,@numero", taño, tproceso, tlocalidad,tentidad,tespecialidad,tdespacho,tnumero).ToArrayAsync();
        }

        

        public Task CrearProceso(string ciudad, string entidad, string especialidad, string despacho, string año, string codProceso, string numero, string tipo, string clase, string descripcion, string demandante_nombre, string demandante_id, string demandado_nombre, string demandado_id)
        {
            string llaveproc = ciudad + entidad + especialidad + despacho + año + codProceso + numero;
            string codiproc = ciudad + entidad + especialidad + despacho + año + codProceso;
            const string area = "0003";

            if(!_context.Ciudad.Any(ciud => ciud.codiciud == ciudad )) throw new Exceptions.NotFoundException("Ciudad no encontrada.");
            if (!_context.Entidad.Any(enti => enti.codienti == entidad )) throw new Exceptions.NotFoundException("Entidad no encontrada.");
            if (!_context.Especialidad.Any(espe => espe.codiespe == especialidad)) throw new Exceptions.NotFoundException("Especialidad no encontrada.");

            if (!_context.TipoArea.Any(ta => ta.CODIAREA == area && ta.CODIPROC == tipo))
                _context.TipoArea.Add(new TipoArea { CODIAREA = area, CODIPROC = tipo});

            if (!_context.TipoAreaClase.Any(tac => tac.codiproc == tipo && tac.codiarea == area && tac.codiclas == clase))
                _context.TipoAreaClase.Add(new TipoAreaClase { codiarea = area, codiproc = tipo, codiclas = clase });

            if (!_context.TipoAreaSubClase.Any(tasc => tasc.codiproc == tipo && tasc.codiarea == area && tasc.codiclas == clase && tasc.codisubc == descripcion))
                _context.TipoAreaSubClase.Add(new TipoAreaSubClase { codiarea = area, codiproc = tipo, codiclas = clase, codisubc = descripcion });

            if (!_context.EntidadEspecialidad.Any(despa => despa.ciudad == ciudad && despa.entidad == entidad && despa.especialidad == especialidad && despa.despacho == despacho))
                _context.EntidadEspecialidad.Add(new EntidadEspecialidad { ciudad = ciudad, entidad = entidad, especialidad = especialidad, despacho = despacho, consradi = 98500, flagdesp = "SI" });

            var demandado = _context.Sujeto.FirstOrDefault(result => result.numesuje == demandado_id.ToUpper());
            var demandante = _context.Sujeto.FirstOrDefault(result => result.numesuje == demandante_id.ToUpper());

            _context.T103dainfoproc.Add(new T103dainfoproc
            {
                A103llavproc = llaveproc,
                A103numeproc = codiproc,
                A103ciudradi = ciudad,
                A103entiradi = entidad,
                A103esperadi = especialidad,
                A103nuenradi = despacho,
                A103anoradi = año,
                A103numeradi = codProceso,
                A103consproc = numero,
                A103codiproc = tipo,
                A103codiclas = clase,
                A103codisubc = descripcion,
                A103codiciuo = ciudad,
                A103codiento = entidad,
                A103codiespo = especialidad,
                A103codinumo = despacho,
                A103codiarea = area,
                A103codirecu = "0000",
                A103codirama = area + tipo + clase + descripcion + "0000"
            });

            if (demandado == null)
                _context.Sujeto.Add(new Dainfosuje { flagsanc = "NO", numesuje = demandado_id.ToUpper(), nombsuje = demandado_nombre.ToUpper() });
            _context.T112drsujeproc.Add(new T112drsujeproc { A112codisuje = "0002", A112funcabog = "F", A112flagdete = "NO", A112llavproc = llaveproc, A112numeproc = codiproc, A112consproc = numero, A112numesuje = demandado_id, A112nombsuje = demandado_nombre.ToUpper() });

            if (demandante == null)
                _context.Sujeto.Add(new Dainfosuje { flagsanc = "NO", numesuje = demandante_id.ToUpper(), nombsuje = demandante_nombre.ToUpper() });
            _context.T112drsujeproc.Add(new T112drsujeproc { A112codisuje = "0001", A112funcabog = "F", A112flagdete = "NO", A112llavproc = llaveproc, A112numeproc = codiproc, A112consproc = numero, A112numesuje = demandante_id, A112nombsuje = demandante_nombre.ToUpper() });

            return _context.SaveChangesAsync();
        }

        /**
         * Queda como tarea tratar/validar los parametros desde este mismo método, no antes de llamarlo.
         * Ergo, debe recibir como parámetro un String y un DateTime respectivamente.
         * Ej: en conversiones se valida la fecha, debe ser acá.
         * **/
        public async Task<Datasainte> CalcTasa(string moneda, DateTime fecha)
        {
            SqlParameter pmoneda = new SqlParameter("@A921TipoTasa", moneda),
                         pfecha = new SqlParameter("@A921Fecha", fecha);
            
            var tasa = await _context.DATASAINTE.FromSql("obtenerTasaVigente @A921TipoTasa, @A921Fecha", pmoneda, pfecha).LastOrDefaultAsync();

            if(tasa == null)
                throw new Exception($"ERROR: No se encontró la tasa para la fecha {fecha.ToLongDateString()}");

            return tasa;
        }

        public Task<Datasainte> Get_LastItem(string moneda)
        {
            return _context.DATASAINTE.Where(x => x.TipoTasa == moneda).OrderByDescending(x => x.VigenteHasta).FirstAsync();
        }

        public Task<Datasainte> Get_LastItem() {
            return _context.DATASAINTE.LastOrDefaultAsync();
        }

        public Task<Datasainte[]> getTasas(string moneda) {
            return _context.DATASAINTE.Where(x => x.TipoTasa == moneda).ToArrayAsync();
        }

        public async Task<bool> EjecutarLiquidacion(string cod_proc, string t_liquidacion, string xml, string cod_user)
        {
            var temp = new T926liquidaciones1();

            if (cod_user == null)
                temp.A926CODUSUARIO = "0000";
            else
                temp.A926CODUSUARIO = cod_user;

            temp.A926TIPOLIQ = Convert.ToString(CodigoLiquidacion(t_liquidacion));
            temp.A926LLAVPROC = cod_proc;
            temp.A926FECELABORA = DateTime.Today.ToLongDateString();
            temp.A926CONSLIQ = (int)Convert.ToDouble(cod_proc);
            temp.A926LIQUIDACION = xml;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public int ObtenerConsecutivoPorNoUnico(string LLAVPROC)
        {
            int temp;

            try {
                temp = _context.T926liquidaciones1.Where(e => e.A926LLAVPROC == LLAVPROC).Max(e => e.A926CONSLIQ);
            } catch (InvalidOperationException e) {
                Console.WriteLine(e);
                temp = 0;
            }

            return temp;
        }

        /// <summary>
        /// Retorna el código numérico del tipo de liquidación solicitado. 
        /// </summary>
        /// <param name="TipoLiquidacion">El tipo de Liquidación a obtener el código.</param>
        /// <returns>Código del Tipo de Liquidación</returns>
        private int CodigoLiquidacion(string TipoLiquidacion)
        {
            switch (TipoLiquidacion)
            {
                case "EjecutivoSingular":
                    return 90;
                case "EjecutivoSingularMultiplesCapitales":
                    return 91;
                case "CuotasAdministracion":
                    return 92;
                case "InteresMoraUVR":
                    return 93;
                case "InteresMoraViviendaPesos":
                    return 94;
                case "ReliquidacionUPACaUVR":
                    return 95;
                case "Indexacion":
                    return 96;
                case "Costas":
                    return 97;
                case "CuotasAlimentacion":
                    return 98;
                default:
                    return -1;
            }
        }
    }
}
