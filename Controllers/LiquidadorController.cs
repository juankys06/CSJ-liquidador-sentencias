using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using liquidador_web.Models;
using liquidador_web.Interfaces;
using liquidador_web.Extra;
using liquidador_web.Data;
using System.Globalization;

namespace liquidador_web.Controllers
{
    [Authorize(Roles = "Administrador General, Usuario Liquidador, Juez")]
    public class LiquidadorController : Controller
    {
        private readonly IConsultas _consultas;
        private IMemoryCache _cache;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LiquidadorUser> _users;

        public LiquidadorController(IConsultas consultas, IMemoryCache cache, ApplicationDbContext context, UserManager<LiquidadorUser> users)
        {
            _consultas = consultas;
            _cache = cache;
            _context = context;
            _users = users;
        }

        [HttpPost]
        public async Task<IActionResult> Procesos([FromServices]IConfiguration _config, string año, string codProceso, string ciudad, string entidad, string especialidad, string despacho, string numero = null)
        {
            var currentUser = await _users.GetUserAsync(User);
            var isGod = await _users.IsInRoleAsync(currentUser, "Administrador General");
            Procesos[] procesos;
            Procesos[] procesosAux;
            Boolean retornoProcesos = false;
            ConexionDB c = new ConexionDB();
            
                
            if (isGod || currentUser.CodDespacho == null)
            {
                procesos = await _consultas.Procesos(año,codProceso,ciudad,entidad,especialidad,despacho,numero);
            }
            else
            {
                if(ciudad!=null && entidad != null && especialidad != null && despacho != null)
                {
                    string cod = ciudad + entidad + especialidad + despacho;
                    string distrito = c.ObtenerDistrito(cod);
                    if (ciudad.Equals("25001"))
                        distrito = "CUNDINAMARCA";
                    if (currentUser.CodDespacho.Equals(distrito))
                    {
                        procesosAux = await _consultas.Procesos(año, codProceso, ciudad, entidad, especialidad, despacho, numero);
                        if (procesosAux.Length > 0)
                        {
                            retornoProcesos = true;
                        }

                        procesos = ValidarProceso(currentUser, procesosAux);
                    }
                    else
                    {
                        HttpContext.Response.StatusCode = 403;
                        return Json(new { message = "No está asignado al distrito al que pertenece el proceso." });
                    }
                }
                else
                {
                    procesosAux = await _consultas.Procesos(año, codProceso, ciudad, entidad, especialidad, despacho, numero);
                    if (procesosAux.Length > 0)
                    {
                        retornoProcesos = true;
                    }

                    procesos = ValidarProceso(currentUser, procesosAux);
                }
                
            }
            
            
            List<Guardados>[] guardados = new List<Guardados>[procesos.Length];

            try
            {
                for (int i = 0; i < procesos.Length; i++)
                    guardados[i] = _context.DataLiquidacion.
                                    Where(guardado => guardado.llaveproc == procesos[i].Completo).
                                    Where(guardado => guardado.tipo.codigo == HttpContext.Session.GetInt32("Type")).
                                    Select(guardado => new Guardados(guardado.ID, guardado.llaveproc, guardado.tipo.nombre, guardado.fecha, guardado.creador.Email, guardado.autoGuardar)).
                                    OrderByDescending(guardado => guardado.autoGuardar).
                                    ToList();
            }
            catch (Exception ex) { HttpContext.Response.StatusCode = 500; return Json(ex); }

            if(procesos.Length==0 && retornoProcesos)
            {
                HttpContext.Response.StatusCode = 403;
                return Json(new { message = "No está asignado al distrito al que pertenece el proceso." });
            }

            if (procesos.Length > 0)
                return Json(new { procesos, guardados });
            else
                try
                {
                    Boolean entra = false;
                    if (isGod || currentUser.CodDespacho == null)
                    {
                        entra = true;
                    }
                    else
                    {
                        
                        string cod = ciudad + entidad + especialidad + despacho;
                        string distrito = c.ObtenerDistrito(cod);
                        if (ciudad.Equals("25001"))
                            distrito = "CUNDINAMARCA";
                        if (currentUser.CodDespacho.Equals(distrito))
                        {
                            entra = true;
                        }
                    }

                    if (entra)
                    {
                        HttpClient httpClient = new HttpClient();
                        var response = await httpClient.GetAsync(_config["CPNUEndpoint"] + ciudad + entidad + especialidad + despacho + año + codProceso + numero);
                        response.EnsureSuccessStatusCode();

                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            HttpContext.Response.StatusCode = 404;
                            return Json(new { message = "No se encontró el proceso." });
                        }

                        var data = await response.Content.ReadAsStringAsync();
                        dynamic json = JArray.Parse(data);
                        int ValorUsar = 999;
                        for (int i = 0; i < json.Count; i++) 
                        {
                            if (json[i].EsPrivado==false)
                            {
                                ValorUsar = i;
                                break;
                            }
                                
                        }

                        if (ValorUsar==999) 
                        {
                            HttpContext.Response.StatusCode = 404;
                            return Json(new { message = "No se encontró el proceso." });
                        }
                        
                        string demandante = json[ValorUsar].Demandante;
                        string[] demand = demandante.Split(":");
                        string demandante_id = demand[0], demandante_nombre = demand[1];

                        string demandado = json[ValorUsar].Demandado;
                        string[] demond = demandado.Split(":");
                        string demandado_id = demond[0], demandado_nombre = demond[1];

                        ValidarRespuesta(json,ValorUsar);


                        await _consultas.CrearProceso(ciudad, entidad, especialidad, despacho, año, codProceso, numero, (string)json[ValorUsar].CodTipoProceso, (string)json[ValorUsar].CodClaseProceso, (string)json[ValorUsar].CodSubclaseProceso, demandante_nombre, demandante_id, demandado_nombre, demandado_id);

                        procesos = new[] { new Procesos
                        {
                            Completo = json[0].LlaveProceso,
                            DEMANDADO = demandado_nombre,
                            DEMANDANTE = demandante_nombre,
                            Año = año,
                            Ciudad = ciudad,
                            Entidad = entidad,
                            Especialidad = especialidad,
                            Despacho = despacho,
                            Numero = numero,
                            Codigo = codProceso,
                            Clase = json[0].ClaseProceso,
                            Tipo = json[0].TipoProceso,
                            Descripcion = json[0].SubclaseProceso
                        } };

                        return Json(new { procesos, guardados });
                    }
                    else
                    {
                        HttpContext.Response.StatusCode = 401;
                        return Json(new { message = "No está asignado al distrito al que pertenece el proceso." });
                    }
                    

                    
                }
                catch (HttpRequestException ex)
                {
                    HttpContext.Response.StatusCode = 502;
                    return Json(ex);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    HttpContext.Response.StatusCode = 500;
                    return Json(ex);
                }
                catch (DbUpdateException ex)
                {
                    HttpContext.Response.StatusCode = 500;
                    return Json(ex);
                }
                catch (Exceptions.NotFoundException ex) {
                    HttpContext.Response.StatusCode = 404;
                    return Json(ex);
                }
        }

        public Procesos[] ValidarProceso(LiquidadorUser currentUser, Procesos[] procesosAux) 
        {
            List<Procesos> lista = new List<Procesos>();
            ConexionDB c = new ConexionDB();

            for (int i = 0; i < procesosAux.Length; i++)
            {
                if (currentUser.CodDespacho != null)
                {
                    string distrito = c.ObtenerDistrito(procesosAux[i].Completo.Substring(0, 12));
                    if ( currentUser.CodDespacho.Equals(distrito) )
                    {
                        lista.Add(procesosAux[i]);
                    }
                }
                /*else
                {
                    if (currentUser.CodLocalidad != null && currentUser.CodEntidad != null && currentUser.CodEspecialidad != null)
                    {
                        if (currentUser.CodLocalidad.Length == 2 && procesosAux[i].Completo.Substring(0, 2).Equals(currentUser.CodLocalidad)
                            && procesosAux[i].Completo.Substring(6, 2).Equals(currentUser.CodEntidad)
                            && procesosAux[i].Completo.Substring(9, 2).Equals(currentUser.CodEspecialidad))
                        {
                            lista.Add(procesosAux[i]);
                        }
                        else if (currentUser.CodLocalidad.Length == 5 && procesosAux[i].Completo.Substring(0, 5).Equals(currentUser.CodLocalidad)
                           && procesosAux[i].Completo.Substring(6, 2).Equals(currentUser.CodEntidad)
                           && procesosAux[i].Completo.Substring(9, 2).Equals(currentUser.CodEspecialidad))
                        {
                            lista.Add(procesosAux[i]);
                        }
                    }
                    else if (currentUser.CodLocalidad != null && currentUser.CodEntidad != null && currentUser.CodEspecialidad == null)
                    {
                        if (currentUser.CodLocalidad.Length == 2 && procesosAux[i].Completo.Substring(0, 2).Equals(currentUser.CodLocalidad)
                            && procesosAux[i].Completo.Substring(6, 2).Equals(currentUser.CodEntidad))
                        {
                            lista.Add(procesosAux[i]);
                        }
                        else if (currentUser.CodLocalidad.Length == 5 && procesosAux[i].Completo.Substring(0, 5).Equals(currentUser.CodLocalidad)
                           && procesosAux[i].Completo.Substring(6, 2).Equals(currentUser.CodEntidad))
                        {
                            lista.Add(procesosAux[i]);
                        }
                    }
                    else if (currentUser.CodLocalidad != null && currentUser.CodEntidad == null && currentUser.CodEspecialidad != null)
                    {
                        if (currentUser.CodLocalidad.Length == 2 && procesosAux[i].Completo.Substring(0, 2).Equals(currentUser.CodLocalidad)
                            && procesosAux[i].Completo.Substring(9, 2).Equals(currentUser.CodEspecialidad))
                        {
                            lista.Add(procesosAux[i]);
                        }
                        else if (currentUser.CodLocalidad.Length == 5 && procesosAux[i].Completo.Substring(0, 5).Equals(currentUser.CodLocalidad)
                           && procesosAux[i].Completo.Substring(9, 2).Equals(currentUser.CodEspecialidad))
                        {
                            lista.Add(procesosAux[i]);
                        }

                    }
                    else if (currentUser.CodLocalidad == null && currentUser.CodEntidad != null && currentUser.CodEspecialidad != null)
                    {
                        if (procesosAux[i].Completo.Substring(6, 2).Equals(currentUser.CodEntidad)
                            && procesosAux[i].Completo.Substring(9, 2).Equals(currentUser.CodEspecialidad))
                        {
                            lista.Add(procesosAux[i]);
                        }

                    }
                    else if (currentUser.CodLocalidad != null && currentUser.CodEntidad == null && currentUser.CodEspecialidad == null)
                    {
                        if (currentUser.CodLocalidad.Length == 2 && procesosAux[i].Completo.Substring(0, 2).Equals(currentUser.CodLocalidad))
                        {
                            lista.Add(procesosAux[i]);
                        }
                        else if (currentUser.CodLocalidad.Length == 5 && procesosAux[i].Completo.Substring(0, 5).Equals(currentUser.CodLocalidad))
                        {
                            lista.Add(procesosAux[i]);
                        }

                    }
                    else if (currentUser.CodLocalidad == null && currentUser.CodEntidad != null && currentUser.CodEspecialidad == null)
                    {
                        if (procesosAux[i].Completo.Substring(6, 2).Equals(currentUser.CodEntidad))
                        {
                            lista.Add(procesosAux[i]);
                        }

                    }
                    else if (currentUser.CodLocalidad == null && currentUser.CodEntidad == null && currentUser.CodEspecialidad != null)
                    {
                        if (procesosAux[i].Completo.Substring(9, 2).Equals(currentUser.CodEspecialidad))
                        {
                            lista.Add(procesosAux[i]);
                        }

                    }
                }*/

            }

            return lista.ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(string formulario, int tipo)
        {
            if (HttpContext.Session.Get("idProceso") == null)
            {
                HttpContext.Response.StatusCode = 404;
                return Json(new { message = "No se encontró la llave del proceso." });
            }

            try
            {
                var liquidacion = new DataLiquidacion
                {
                    data = formulario,
                    fecha = DateTime.Now,
                    llaveproc = HttpContext.Session.GetString("idProceso"),
                    tipo = await _context.TipoLiquidacion.FindAsync(tipo),
                    creador = await _users.GetUserAsync(User),
                    autoGuardar = false
                };

                _context.DataLiquidacion.Add(liquidacion);

                await _context.SaveChangesAsync();

                return Json(new { message = "La Liquidación ha sido guardada.", data = new Guardados(liquidacion.ID, liquidacion.llaveproc, liquidacion.tipo.nombre, liquidacion.fecha, liquidacion.creador.Email,liquidacion.autoGuardar) });
            }
            catch (Exception ex){
                HttpContext.Response.StatusCode = 500;
                return Json(new { message = ex.Message, type = ex.InnerException });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AutoGuardar(string formulario, int tipo, string idProceso)
        {
            if (idProceso == null)
            {
                HttpContext.Response.StatusCode = 404;
                return Json(new { message = "No se encontró la llave del proceso." });
            }

            LiquidadorUser creador = await _users.GetUserAsync(User);

            try
            {
                int autoGuardado;

                autoGuardado = _context.DataLiquidacion.
                                Where(guardado => guardado.llaveproc == idProceso).
                                Where(guardado => guardado.tipo.codigo == tipo).
                                Where(guardado => guardado.creador == creador).
                                Where(guardado => guardado.autoGuardar == true).Count();


                if (autoGuardado == 0)
                {
                    var liquidacion = new DataLiquidacion
                    {
                        data = formulario,
                        fecha = DateTime.Now,
                        llaveproc = idProceso,
                        tipo = await _context.TipoLiquidacion.FindAsync(tipo),
                        creador = creador,
                        autoGuardar = true
                    };

                    _context.DataLiquidacion.Add(liquidacion);

                    await _context.SaveChangesAsync();

                    return Json(new { message = "La Liquidación ha sido guardada.", data = new Guardados(liquidacion.ID, liquidacion.llaveproc, liquidacion.tipo.nombre, liquidacion.fecha, liquidacion.creador.Email,liquidacion.autoGuardar) });
                }
                else
                {

                    DataLiquidacion data =  _context.DataLiquidacion.
                                                Where(guardado => guardado.llaveproc == idProceso).
                                                Where(guardado => guardado.tipo.codigo == tipo).
                                                Where(guardado => guardado.creador == creador).
                                                Where(guardado => guardado.autoGuardar == true).Single();
                    

                    data.data = formulario;
                    data.fecha = DateTime.Now;
                    data.tipo = await _context.TipoLiquidacion.FindAsync(tipo);
                    data.llaveproc = idProceso;
                    data.creador = creador;
                    

                    _context.DataLiquidacion.Update(data);

                    await _context.SaveChangesAsync();

                    return Json(new { message = "La Liquidación ha sido guardada.", data = new Guardados(data.ID, data.llaveproc, data.tipo.nombre, data.fecha, data.creador.Email, data.autoGuardar) });
                }
                // _context.DataLiquidacion.Find(liquidacion).data
                
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { message = ex.Message, type = ex.InnerException });
            }
        }

        [HttpPost]
        public IActionResult Cargar(int liquidacion)
        {
            Console.WriteLine(liquidacion);
            return Content(_context.DataLiquidacion.Find(liquidacion).data);
        }

        public async Task<IActionResult> DescargaExcel()
        {
            string prefijo = HttpContext.Session.GetString("prefijoCacheData");

            DateTime Fecha = DateTime.Now;
            if (HttpContext.Session.GetInt32("Type") == 90)
            { //-- Singular
                if (_cache.TryGetValue(prefijo + "tempLiquidacion", out List<Liquidacion> tempLiquidacion) && _cache.TryGetValue(prefijo + "tempResumen", out List<(string, double)> tempResumen) && _cache.TryGetValue(prefijo + "tempObservaciones", out string tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                    return File(Utilities.CreateExcelDoc(tempLiquidacion, tempResumen,tempObservaciones), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpContext.Session.GetString("idProceso") + "_EjecutivoSingular" + "-" + Fecha.ToString("ddMMyyyyhhmm") + ".xlsx");
                else
                    return NotFound();
            }
            else if (HttpContext.Session.GetInt32("Type") == 91)
            { //-- Singular Multiple
                if (_cache.TryGetValue(prefijo + "tempLiquidaciones", out List<Liquidacion>[] tempLiquidaciones) && _cache.TryGetValue(prefijo + "tempResumenes", out List<(string, double)>[] tempResumenes) && _cache.TryGetValue(prefijo + "tempObservaciones", out string[] tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                    return File(Utilities.CreateExcelDoc(tempLiquidaciones, tempResumenes, tempObservaciones), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpContext.Session.GetString("idProceso") + "_LiquidacionMultiplesCapitales" + "-" + Fecha.ToString("ddMMyyyyhhmm") + ".xlsx");
                else
                    return NotFound();
            }
            else if (HttpContext.Session.GetInt32("Type") == 92)
            {
                if (_cache.TryGetValue(prefijo + "tempLiquidacion", out List<Liquidacion> tempLiquidacion) && _cache.TryGetValue(prefijo + "tempResumen", out List<(string, double)> tempResumen) && _cache.TryGetValue(prefijo + "tempObservaciones", out string tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                    return File(Utilities.CreateExcelDoc(tempLiquidacion, tempResumen ,tempObservaciones), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpContext.Session.GetString("idProceso") + "_CuotaAdministracion" + "-" + Fecha.ToString("ddMMyyyyhhmm") + ".xlsx");
                else
                    return NotFound();
            }
            else if (HttpContext.Session.GetInt32("Type") == 98)
            {
                if (_cache.TryGetValue(prefijo + "tempLiquidacion", out List<Liquidacion> tempLiquidacion) && _cache.TryGetValue(prefijo + "tempResumen", out List<(string, double)> tempResumen) && _cache.TryGetValue(prefijo + "tempObservaciones", out string tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                    return File(Utilities.CreateExcelDoc(tempLiquidacion, tempResumen, tempObservaciones), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpContext.Session.GetString("idProceso") + "_CuotaAlimentacion" + "-" + Fecha.ToString("ddMMyyyyhhmm") + ".xlsx");
                else
                    return NotFound();
            }
            else if (HttpContext.Session.GetInt32("Type") == 93)
            {
                if (_cache.TryGetValue(prefijo + "tempLiquidacion", out List<Liquidacion> tempLiquidacion) && _cache.TryGetValue(prefijo + "tempResumen", out List<(string, double, double)> tempResumen) && _cache.TryGetValue(prefijo + "tempObservaciones", out string tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                {
                    try
                    {
                        var stream = Utilities.CreateExcelDocHipotecarioUVR(tempLiquidacion, tempResumen, tempObservaciones, out string fileName);
                        var liquidacion = new Liquidaciones { ID = fileName, fecha = DateTime.Now, 
                                                              autor = await _users.GetUserAsync(User), 
                                                              nameFile = fileName + ".xlsx", 
                                                              urlFile = "\\assets\\pdfDocuments\\" + fileName + ".xlsx",
                                                              llaveproc = await _context.T103dainfoproc.FirstAsync(proceso => proceso.A103llavproc == HttpContext.Session.GetString("idProceso")),
                                                              tipo = await _context.TipoLiquidacion.FirstAsync(tipo => tipo.codigo == HttpContext.Session.GetInt32("Type"))
                                                            };
                        _context.Liquidaciones.Add(liquidacion);
                        _context.Auditoria.Add(new Auditoria { usuario = await _users.GetUserAsync(User), fecha = DateTime.Now, evento = Eventos.Añadir, modulo = "Liquidaciones", logActual = JsonConvert.SerializeObject(liquidacion) });
                        await _context.SaveChangesAsync();

                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpContext.Session.GetString("idProceso") + "_HipotecarioUVR" + "-" + Fecha.ToString("ddMMyyyyhhmm") + ".xlsx");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("HipotecarioUVR");
                    }
                    catch (DbUpdateException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> No se pudo realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("HipotecarioUVR");
                    }
                }
                else
                    return NotFound();
            }
            else if (HttpContext.Session.GetInt32("Type") == 94)
            {
                if (_cache.TryGetValue(prefijo + "tempLiquidacion", out List<Liquidacion> tempLiquidacion) && _cache.TryGetValue(prefijo + "tempResumen", out List<(string, double)> tempResumen) && _cache.TryGetValue(prefijo + "tempObservaciones", out string tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                {
                    try
                    {
                        var stream = Utilities.CreateExcelDocHipotecario(tempLiquidacion, tempResumen, tempObservaciones, out string fileName);
                        var liquidacion = new Liquidaciones { ID = fileName, fecha = DateTime.Now, 
                                                              autor = await _users.GetUserAsync(User),
                                                              nameFile = fileName + ".xlsx",
                                                              urlFile = "\\assets\\pdfDocuments\\" + fileName + ".xlsx",
                                                              llaveproc = await _context.T103dainfoproc.FirstAsync(proceso => proceso.A103llavproc == HttpContext.Session.GetString("idProceso")),
                                                              tipo = await _context.TipoLiquidacion.FirstAsync(tipo => tipo.codigo == HttpContext.Session.GetInt32("Type"))
                                                            };
                        _context.Liquidaciones.Add(liquidacion);
                        _context.Auditoria.Add(new Auditoria { usuario = await _users.GetUserAsync(User), fecha = DateTime.Now, evento = Eventos.Añadir, modulo = "Liquidaciones", logActual = JsonConvert.SerializeObject(liquidacion) });
                        await _context.SaveChangesAsync();

                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpContext.Session.GetString("idProceso") + "_HipotecarioPesos" + "-" + Fecha.ToString("ddMMyyyyhhmm") + ".xlsx");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("HipotecarioPesos");
                    }
                    catch (DbUpdateException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> No se pudo realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("HipotecarioPesos");
                    }
                }else
                    return NotFound();
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Procesa y Crea un archivo de Excel listo para descargar para una liquidación de costas.
        /// </summary>
        /// <param name="asunto">Distintos asuntos</param>
        /// <param name="valor">Valores</param>
        /// <param name="idProceso">Llave del Proceso</param>
        /// <returns></returns>
        public IActionResult DescargarExcel(string[] asunto, double?[] valor, string idProceso){
            if(idProceso == null){
                TempData["Error"] = $"<b>ERROR:</b> No seleccionó un proceso.";
                return RedirectToAction("CostasProceso");
            }

            try {
                List<(string, double)> costas = new List<(string, double)>();

                for(int i = 0 ; i < asunto.Length ; i++)
                    costas.Add((asunto[i]??"", valor[i]??0));

                return File(Utilities.CreateExcelDoc(costas), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", idProceso + "_Costas" + ".xlsx");
            } catch(Exception){
                TempData["Error"] = $"<b>ERROR:</b> No se pudo imprimir la liquidación debido a un problema del servidor. Por favor, intente de nuevo más tarde.";
                return RedirectToAction("CostasProceso");
            }
        }

        public async Task<IActionResult> DownloadPdf()
        {
            string fileName;
            var currentUser = await _users.GetUserAsync(User);
            var llave = await _context.T103dainfoproc.FirstAsync(proceso => proceso.A103llavproc == HttpContext.Session.GetString("idProceso"));
            var temp_tipo = await _context.TipoLiquidacion.Where(tipo => tipo.codigo == HttpContext.Session.GetInt32("Type")).FirstAsync();
            string prefijo = HttpContext.Session.GetString("prefijoCacheData");

            if (HttpContext.Session.GetInt32("Type") == 90)
            {
                if (_cache.TryGetValue(prefijo + "tempLiquidacion", out List<Liquidacion> tempLiquidacion) && _cache.TryGetValue(prefijo + "tempResumen", out List<(string, double)> tempResumen) && _cache.TryGetValue(prefijo + "tempObservaciones", out string observaciones) && HttpContext.Session.GetString("idProceso") != null)
                {
                    try {
                        var stream = Utilities.PdfDoc(tempLiquidacion, tempResumen, observaciones, out fileName, HttpContext.Session.GetString("idProceso"));
                        var liquidacion = new Liquidaciones { ID = fileName, fecha = DateTime.Now, autor = currentUser, nameFile = fileName + ".pdf", urlFile = "\\assets\\pdfDocuments\\" + fileName + ".pdf", tipo = temp_tipo, llaveproc = llave };
                        _context.Liquidaciones.Add(liquidacion);
                        _context.Auditoria.Add(new Auditoria { usuario = currentUser, fecha = DateTime.Now, evento = Eventos.Añadir, modulo = "Liquidaciones",  logActual =  JsonConvert.SerializeObject(liquidacion)});
                        await _context.SaveChangesAsync();
                        return File(stream, "application/pdf");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("Singular");
                    }
                    catch (DbUpdateException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> No se pudo realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("Singular");
                    }
                }
                else
                    return NotFound();
            }
            else if (HttpContext.Session.GetInt32("Type") == 91 )
            { //-- Singular Multiple
                if (_cache.TryGetValue(prefijo + "tempLiquidaciones", out List<Liquidacion>[] tempLiquidaciones) && _cache.TryGetValue(prefijo + "tempResumenes", out List<(string, double)>[] tempResumenes) && _cache.TryGetValue(prefijo + "tempObservaciones", out string[] tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                {
                    try
                    {
                        var stream = Utilities.PdfDoc(tempLiquidaciones, tempResumenes, tempObservaciones, out fileName, HttpContext.Session.GetString("idProceso"));
                        var liquidacion = new Liquidaciones { ID = fileName, fecha = DateTime.Now, autor = currentUser, nameFile = fileName + ".pdf", urlFile = "\\assets\\pdfDocuments\\" + fileName + ".pdf", tipo = temp_tipo, llaveproc = llave };
                        _context.Liquidaciones.Add(liquidacion);
                        _context.Auditoria.Add(new Auditoria { usuario = currentUser, fecha = DateTime.Now, evento = Eventos.Añadir, modulo = "Liquidaciones", logActual = JsonConvert.SerializeObject(liquidacion) });
                        await _context.SaveChangesAsync();
                        return File(stream, "application/pdf");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("Singular_Multiple");
                    }
                    catch (DbUpdateException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> No se pudo realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("Singular_Multiple");
                    }
                }
                else
                    return NotFound();
            }
            else if (HttpContext.Session.GetInt32("Type") == 92 || HttpContext.Session.GetInt32("Type") == 98)
            {
                if (_cache.TryGetValue(prefijo + "tempLiquidacion", out List<Liquidacion> tempLiquidacion) && _cache.TryGetValue(prefijo + "tempResumen", out List<(string, double)> tempResumen) && _cache.TryGetValue(prefijo + "tempObservaciones", out string tempObservaciones) && HttpContext.Session.GetString("idProceso") != null)
                {
                    try
                    {
                        var stream = Utilities.PdfDoc(tempLiquidacion, tempResumen, tempObservaciones, out fileName, HttpContext.Session.GetString("idProceso"));
                        var liquidacion = new Liquidaciones { ID = fileName, fecha = DateTime.Now, autor = currentUser, nameFile = fileName + ".pdf", urlFile = "\\assets\\pdfDocuments\\" + fileName + ".pdf", tipo = temp_tipo, llaveproc = llave };
                        _context.Liquidaciones.Add(liquidacion);
                        _context.Auditoria.Add(new Auditoria { usuario = currentUser, fecha = DateTime.Now, evento = Eventos.Añadir, modulo = "Liquidaciones", logActual = JsonConvert.SerializeObject(liquidacion) });
                        await _context.SaveChangesAsync();
                        return File(stream, "application/pdf");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("CuotasAdministracion");
                    }
                    catch (DbUpdateException)
                    {
                        TempData["NotEdited"] = $"<b>ERROR:</b> No se pudo realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
                        return RedirectToAction("CuotasAdministracion");
                    }
                }
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        public IActionResult Singular()
        {
            ViewData["Title"] = "Liquidador Singular";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 90);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult Singular(double capital, double si_mora, double si_plazo, DateTime f_obligacion, DateTime f_exigibilidad, DateTime f_liquidacion,
                                     DateTime?[] f_abono, DateTime?[] f_capitales, double?[] abono, double?[] capitales, string idProceso,
                                     double? tasa_pacto_a, double? tasa_pacto_b, double? puntos_mora, double? puntos_corriente, string aplica_sancion = null,
                                     string i_corriente = "variable", string i_mora = "variable", string i_corriente_var = "IBC", string i_mora_var = "IBC", string observaciones ="")
        {
            Liquidador liquidador;

            if (capital > 0 && f_obligacion == null || f_exigibilidad == null || f_liquidacion == null || f_abono == null ||
                abono == null || idProceso == "" || idProceso == null)
            {
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR FATAL: hay elementos nulos enviados." });
            }

            if (f_obligacion.Date >= f_liquidacion.Date) {
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: las fechas no son coherentes." });
            }

            if (i_corriente == "variable")
                i_corriente = i_corriente_var;

            if (i_mora == "variable")
                i_mora = i_mora_var;

            liquidador = new Liquidador(_consultas, capital, false, aplica_sancion, si_plazo, si_mora, i_corriente.ToUpper(), i_mora.ToUpper(), abono, f_abono, capitales, f_capitales, f_obligacion, f_exigibilidad, f_liquidacion, tasa_pacto_b, tasa_pacto_a, puntos_corriente, puntos_mora);
            try
            {
                liquidador.Liquidar();
            }
            catch (Exception exception)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { exception.Message });
            }
            string prefijo = idProceso + "-"+DateTime.Now.ToString();

            //var temp = _consultas.ObtenerConsecutivoPorNoUnico(idProceso);
            //Utilities.Put(TempData, "tempLiquidacion", liquidador._TablaLiquidacion);
            //Utilities.Put(TempData, "tempResumen", liquidador.Resumen());
            _cache.Set(prefijo + "tempLiquidacion", liquidador._TablaLiquidacion, TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempResumen", liquidador.Resumen(), TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempObservaciones", observaciones, TimeSpan.FromMinutes(25));
            HttpContext.Session.SetString("idProceso", idProceso);
            HttpContext.Session.SetString("prefijoCacheData", prefijo);

            return Json(new { detalle_liquidacion = liquidador._TablaLiquidacion, resumen = liquidador.Resumen() });
        }

        public IActionResult Singular_Multiple()
        {
            ViewData["Title"] = "Singular Múltiple";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 91);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult Singular_Multiple(string data, string idProceso)
        {
            JArray json = JArray.Parse(data);
            Liquidador[] liquidaciones = new Liquidador[json.Count];
            List<Liquidacion>[] tablasLiquidaciones = new List<Liquidacion>[json.Count];
            List<(string, double)>[] tablasResumen = new List<(string, double)>[json.Count];
            string[] observaciones = new string[json.Count];
            RespJSON[] response = new RespJSON[json.Count];

            int i = 0;

            foreach (JObject element in json)
            {
                try {
                    if (element["abono[]"] == null) 
                    {
                        liquidaciones[i] = new Liquidador(_consultas, element.Value<double>("capital"),
                                   element.Value<DateTime>("f_obligacion"), element.Value<DateTime>("f_exigibilidad"),
                                   element.Value<DateTime>("f_liquidacion"), element.Value<bool>("base_360"),
                                   element.Value<bool>("aplica_sancion"), element.Value<double?>("tasa_pacto_b"),
                                   element.Value<double?>("tasa_pacto_a"), element.Value<double?>("puntos_corriente"),
                                   element.Value<double?>("puntos_mora"), element.Value<string>("i_corriente"),
                                   element.Value<string>("i_mora"),
                                   JArray.Parse("").ToObject<double[]>(),
                                   JArray.Parse("").ToObject<DateTime[]>());
                    }
                    else
                    {
                        liquidaciones[i] = new Liquidador(_consultas, element.Value<double>("capital"),
                                   element.Value<DateTime>("f_obligacion"), element.Value<DateTime>("f_exigibilidad"),
                                   element.Value<DateTime>("f_liquidacion"), element.Value<bool>("base_360"),
                                   element.Value<bool>("aplica_sancion"), element.Value<double?>("tasa_pacto_b"),
                                   element.Value<double?>("tasa_pacto_a"), element.Value<double?>("puntos_corriente"),
                                   element.Value<double?>("puntos_mora"), element.Value<string>("i_corriente"),
                                   element.Value<string>("i_mora"),
                                   JArray.Parse(element["abono[]"].ToString()).ToObject<double[]>(),
                                   JArray.Parse(element["f_abono[]"].ToString()).ToObject<DateTime[]>());
                    }
                    
                } catch (JsonReaderException e) {
                    Console.WriteLine("Error dándole formato de array a las fechas y montos de abonos... " +
                                      "Intentando como datos singulares. Mensaje del error: " + e.Message);

                    liquidaciones[i] = new Liquidador(_consultas, element.Value<double>("capital"),
                                   element.Value<DateTime>("f_obligacion"), element.Value<DateTime>("f_exigibilidad"),
                                   element.Value<DateTime>("f_liquidacion"), element.Value<bool>("base_360"),
                                   element.Value<bool>("aplica_sancion"), element.Value<double?>("tasa_pacto_b"),
                                   element.Value<double?>("tasa_pacto_a"), element.Value<double?>("puntos_corriente"),
                                   element.Value<double?>("puntos_mora"), element.Value<string>("i_corriente"),
                                   element.Value<string>("i_mora"), new[] { element.Value<double>("abono[]") },
                                   new[] { element.Value<DateTime>("f_abono[]") });
                } catch (Exception e) {
                    Console.WriteLine("Error dándole formato de objeto a las fechas y montos de abonos... " +
                                      "Intentando con datos vacíos. Mensaje del error: " + e.Message);
                    //if(e is FormatException || e is NullReferenceException)
                    liquidaciones[i] = new Liquidador(_consultas, element.Value<double>("capital"),
                                   element.Value<DateTime>("f_obligacion"), element.Value<DateTime>("f_exigibilidad"),
                                   element.Value<DateTime>("f_liquidacion"), element.Value<bool>("base_360"),
                                   element.Value<bool>("aplica_sancion"), element.Value<double>("tasa_pacto_b?"),
                                   element.Value<double?>("tasa_pacto_a"), element.Value<double?>("puntos_corriente"),
                                   element.Value<double?>("puntos_mora"), element.Value<string>("i_corriente"),
                                   element.Value<string>("i_mora"), null, null);
                } finally {
                    liquidaciones[i].Liquidar();
                    response[i] = new RespJSON(liquidaciones[i]._TablaLiquidacion, liquidaciones[i].Resumen());
                    tablasLiquidaciones[i] = liquidaciones[i]._TablaLiquidacion;
                    tablasResumen[i] = liquidaciones[i].Resumen();
                    observaciones[i] = element.Value<string>("observaciones");
                    i++;
                }
            }
            string prefijo = idProceso + "-" + DateTime.Now.ToString();
            _cache.Set(prefijo + "tempLiquidaciones", tablasLiquidaciones, TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempResumenes", tablasResumen, TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempObservaciones", observaciones, TimeSpan.FromMinutes(25));
            HttpContext.Session.SetString("idProceso", idProceso);
            HttpContext.Session.SetString("prefijoCacheData", prefijo);

            return Json(new RespMultiple(response, Utilities.ResumenGeneral(liquidaciones)));
        }

        public IActionResult CuotasAdministracion()
        {
            ViewData["Title"] = "Cuotas de Administración";
            ViewData["Message"] = "Liquidación Cuotas de Administración";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 92);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult CuotasAdministracion(string idProceso, double capital, DateTime f_exigibilidad, DateTime f_liquidacion,
                                                string i_mora, double? i_pactado, DateTime?[] f_cuota, double?[] cuota,
                                                DateTime?[] f_ext, double?[] ext, double?[] aSeguros,
                                                DateTime?[] f_multa, double?[] multa, DateTime?[] f_abono,
                                                double?[] abono, string[] calcInt = null, string[] intereses = null, string observaciones = "")
        {
            LiqCuotas liquidador;

            if (capital > 0 && f_exigibilidad == null || f_liquidacion == null){
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: hay elementos nulos enviados."});
            }

            if (idProceso == null || idProceso == ""){
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: Debe seleccionar un proceso." } );
            }

            if (i_mora != "PACTADO" && i_mora != "IBC"){
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: interés inválido." });
            }

            liquidador = new LiqCuotas(_consultas, capital, f_exigibilidad, f_liquidacion, i_mora, false, false, i_pactado, f_ext, ext, aSeguros, f_multa, multa, f_cuota, cuota, f_abono, abono, calcInt, intereses);

            // try
            // {
                liquidador.Liquidar();
            // }
            // catch (Exception exception)
            // {
            //     HttpContext.Response.StatusCode = 500;
            //     return Json(new { exception.Message });
            // }

            string prefijo = idProceso + "-" + DateTime.Now.ToString();
            _cache.Set(prefijo + "tempLiquidacion", liquidador._TablaLiquidacion, TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempResumen", liquidador.Resumen(), TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempObservaciones", observaciones, TimeSpan.FromMinutes(25));
            HttpContext.Session.SetString("idProceso", idProceso);
            HttpContext.Session.SetString("prefijoCacheData", prefijo);

            return Json(new { detalle_liquidacion = liquidador._TablaLiquidacion, resumen = liquidador.Resumen() });
        }

        public IActionResult CuotasAlimentacion()
        {
            ViewData["Title"] = "Cuotas de Alimentación";
            ViewData["Message"] = "Liquidación Cuotas de Alimentación";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 98);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult CuotasAlimentacion(string idProceso, double mandamiento, double capital, DateTime f_exigibilidad, DateTime f_liquidacion,
                                                string i_mora, double? i_pactado, DateTime?[] f_cuota, double?[] cuota,
                                                DateTime?[] f_ext, double?[] ext, double?[] aSeguros,
                                                DateTime?[] f_multa, double?[] multa, DateTime?[] f_abono,
                                                double?[] abono, string[] calcInt = null, string[] intereses = null, string observaciones = "")
        {
            LiqAlimentos liquidador;

            if (capital > 0 && f_exigibilidad == null || f_liquidacion == null)
            {
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: hay elementos nulos enviados." });
            }

            if (idProceso == null || idProceso == "")
            {
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: Debe seleccionar un proceso." });
            }

            if (i_mora != "PACTADO" && i_mora != "IBC")
            {
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: interés inválido." });
            }

            liquidador = new LiqAlimentos(_consultas, mandamiento, capital, f_exigibilidad, f_liquidacion, i_mora, false, false, i_pactado, f_ext, ext, aSeguros, f_multa, multa, f_cuota, cuota, f_abono, abono, calcInt, intereses);

            // try
            // {
            liquidador.Liquidar();
            // }
            // catch (Exception exception)
            // {
            //     HttpContext.Response.StatusCode = 500;
            //     return Json(new { exception.Message });
            // }

            string prefijo = idProceso + "-" + DateTime.Now.ToString();
            _cache.Set(prefijo + "tempLiquidacion", liquidador._TablaLiquidacion, TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempResumen", liquidador.Resumen(), TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempObservaciones", observaciones, TimeSpan.FromMinutes(25));
            HttpContext.Session.SetString("idProceso", idProceso);
            HttpContext.Session.SetString("prefijoCacheData", prefijo);

            return Json(new { detalle_liquidacion = liquidador._TablaLiquidacion, resumen = liquidador.Resumen() });
        }

        public IActionResult Indexacion()
        {
            ViewData["Title"] = "Indexacion";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 96);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult Indexacion(string idProceso, double capital, DateTime f_inicial, DateTime f_final)
        {
            HttpContext.Session.SetString("idProceso", idProceso);

            try{
                double ipc_inicial = _consultas.CalcTasa("IPC", f_inicial).Result.ValorTasa ?? 0,
                    ipc_final = _consultas.CalcTasa("IPC", f_final).Result.ValorTasa ?? 0,
                    correccionMonetaria = 0, vr = 0;

                if (ipc_inicial != 0 || ipc_final != 0 || capital != 0) {
                    vr = ipc_final / ipc_inicial * capital;
                    correccionMonetaria = vr - capital;
                }

                return Content("{ \"vr\":" + vr + ", \"ipc_inicial\":" + ipc_inicial + ", \"ipc_final\":" + ipc_final + " }", "application/json");
            } catch (NullReferenceException) {
                HttpContext.Response.StatusCode = 500;
                return Json(new { message = "No hay tasas registradas para las fechas seleccionadas." });
            }
        }

        public IActionResult HipotecarioUVR()
        {
            ViewData["Title"] = "Liquidación Hipotecaria en UVR";
            ViewData["Type"] = "UVR";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 93);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        public IActionResult HipotecarioPesos()
        {
            ViewData["Title"] = "Liquidación Hipotecaria en Pesos";
            ViewData["Type"] = "PESOS";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 94);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult HipotecarioUVR(string idProceso, DateTime f_contrato, double capital, DateTime f_capital,
                                           DateTime f_exigibilidad, DateTime f_liquidacion, bool ivs, double i_plazo,
                                           double i_mora, DateTime?[] f_abono, double?[] pago_abono, double?[] seguro_abono,
                                           DateTime?[] f_capitales, double?[] capitales, string observaciones = "")
        {
            if (idProceso == null || idProceso == ""){
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: Debe seleccionar un proceso." } );
            }

            HipotecarioUVR liquidador = new HipotecarioUVR(_consultas, f_contrato, capital, f_capital, f_exigibilidad, f_liquidacion, ivs, i_plazo, i_mora, f_abono, pago_abono, seguro_abono, f_capitales, capitales);

            try {
                liquidador.Liquidar();
            }
            catch (Exception exception)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { exception.Message });
            }

            string prefijo = idProceso + "-" + DateTime.Now.ToString();
            _cache.Set(prefijo + "tempLiquidacion", liquidador.TablaLiquidacion, TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempResumen", liquidador.ResumenUVR(), TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempObservaciones", observaciones, TimeSpan.FromMinutes(25));

            HttpContext.Session.SetString("idProceso", idProceso);
            HttpContext.Session.SetString("prefijoCacheData", prefijo);

            return Json(new { detalle_liquidacion = liquidador.TablaLiquidacion, resumen = liquidador.ResumenUVR() });
        }

        [HttpPost]
        public IActionResult HipotecarioPesos(string idProceso, DateTime f_contrato, double capital, DateTime f_capital,
                                           DateTime f_exigibilidad, DateTime f_liquidacion, bool ivs, double i_plazo,
                                           double i_mora, DateTime?[] f_abono, double?[] pago_abono, double?[] seguro_abono,
                                           DateTime?[] f_capitales, double?[] capitales, string observaciones = "")
        {
            if (idProceso == null || idProceso == ""){
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "ERROR: Debe seleccionar un proceso." } );
            }

            HipotecarioUVR liquidador = new HipotecarioUVR(_consultas, capital, f_contrato, f_capital, f_exigibilidad, f_liquidacion, ivs, i_plazo, i_mora, f_abono, pago_abono, seguro_abono, f_capitales, capitales);

            try
            {
                liquidador.Liquidar();
            }
            catch (Exception exception)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { exception.Message });
            }

            string prefijo = idProceso + "-" + DateTime.Now.ToString();
            _cache.Set(prefijo + "tempLiquidacion", liquidador.TablaLiquidacion, TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempResumen", liquidador.Resumen(), TimeSpan.FromMinutes(25));
            _cache.Set(prefijo + "tempObservaciones", observaciones, TimeSpan.FromMinutes(25));

            HttpContext.Session.SetString("idProceso", idProceso);
            HttpContext.Session.SetString("prefijoCacheData", prefijo);

            return Json(new { detalle_liquidacion = liquidador.TablaLiquidacion, resumen = liquidador.Resumen() });
        }

        public IActionResult ReliquidacionUVR() {
            ViewData["Title"] = "Reliquidación UVR.";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.Remove("Type");
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult ReliquidacionUVR(bool saldoIEnUPAC, double monto_sinicial, DateTime f_sinicial, bool saldoBancoEnUPAC, double monto_banco, double i_plazo, DateTime?[] f_movimiento, double?[] pago_movimiento, double?[] seguro_movimiento, double?[] mora_movimiento, double?[] otros_movimiento, bool[] ficticio, int factor, DateTime?[] f_tasas, double?[] tasas) {
            if (f_sinicial > DateTime.Parse("31/12/1999") && f_sinicial < DateTime.Parse("01/01/1993")){
                HttpContext.Response.StatusCode = 422;
                return Json(new { message = "La fecha debe ser menor o igual al 31/12/1999 y desde el 1993." });
            }

            Reliquidacion reliquidacion = new Reliquidacion(_consultas, monto_sinicial, f_sinicial, saldoIEnUPAC, saldoBancoEnUPAC, monto_banco, i_plazo, factor, f_movimiento, pago_movimiento, seguro_movimiento, mora_movimiento, otros_movimiento, ficticio, f_tasas, tasas);

            try {
                reliquidacion.Reliquidar();
                reliquidacion.Resumen();
            } catch (Exception ex) { 
                HttpContext.Response.StatusCode = 500;
                return Json(new { ex.Message });
            }

            return Json(new { detalle = reliquidacion.TablaReLiquidacion, resumen = reliquidacion.Resumen() });
        }

        public IActionResult CostasProceso()
        {
            ViewData["Title"] = "Costas del Proceso";

            HttpContext.Session.Remove("idProceso");
            HttpContext.Session.SetInt32("Type", 97);
            _cache.Remove("tempLiquidacion");
            _cache.Remove("tempResumen");
            _cache.Remove("tempLiquidaciones");
            _cache.Remove("tempResumenes");

            return View();
        }

        [HttpPost]
        public IActionResult ListaVIS() {
            return Json(_consultas.getTasas("SML").Result);
        }

        [HttpPost]
        public IActionResult GetValorCuota(DateTime Fecha, DateTime FechaIncremento, string Incremento, float ValorCuota) {
            JObject respuesta = new JObject();
            ConexionDB consultas = new ConexionDB();
            List<incrementoInteranualTasas> incrementos = new List<incrementoInteranualTasas>();
            if(Incremento.Equals("IPC") || Incremento.Equals("SML")) 
            {
                incrementos = consultas.ObtenerIncremento(Incremento);
            }
            float valorC = ValorCuota;
            Boolean Correcto = false;

            if (Fecha >= FechaIncremento)
            {
                if(Incremento.Equals("IPC") || Incremento.Equals("SML"))
                {
                    for(int i=0; i<incrementos.Count; i++)
                    {
                        if (Fecha.Month <= FechaIncremento.Month && Fecha.Day < FechaIncremento.Day)
                        {
                            if (i > 0)
                            {
                                if (incrementos[i].anhio >= FechaIncremento.Year && incrementos[i].anhio <= Fecha.Year-1)
                                {
                                    valorC += (valorC * ((float)incrementos[i].incremento / 100));
                                }
                                if (Fecha.Year == incrementos[i].anhio)
                                {
                                    Correcto = true;
                                }
                            }
                            
                        }
                        else
                        {
                            if (incrementos[i].anhio >= FechaIncremento.Year && incrementos[i].anhio <= Fecha.Year)
                            {
                                valorC += (valorC * ((float)incrementos[i].incremento / 100));
                            }
                            if (Fecha.Year == incrementos[i].anhio)
                            {
                                Correcto = true;
                            }
                        }
                            
                    }
                }
                else
                {
                    Correcto = true;
                    if (Fecha.Month <= FechaIncremento.Month && Fecha.Day < FechaIncremento.Day)
                    {
                        for (int i = FechaIncremento.Year; i <= Fecha.Year-1; i++)
                        {
                            valorC += (valorC * (float.Parse(Incremento) / 100));
                        }
                    }
                    else
                    {
                        for (int i = FechaIncremento.Year; i <= Fecha.Year; i++)
                        {
                            valorC += (valorC * (float.Parse(Incremento) / 100));
                        }
                    }
                }
                
                if (!Correcto)
                {
                    respuesta.Add("Error", "No se tienen todos los valores del incremento para calcular el valor de la cuota");
                }
                respuesta.Add("valor", valorC);
                return Json(respuesta);
            }
            else 
            {
                respuesta.Add("valor",ValorCuota);
                return Json(respuesta);
            }

            
        }

        public float GetValorCuota(DateTime Fecha, DateTime FechaIncremento, string Incremento, float ValorCuota, List<incrementoInteranualTasas> incrementos)
        {
            JObject respuesta = new JObject();
            float valorC = ValorCuota;
            Boolean Correcto = false;

            if (Fecha >= FechaIncremento)
            {
                if (Incremento.Equals("IPC") || Incremento.Equals("SML"))
                {
                    for (int i = 0; i < incrementos.Count; i++)
                    {
                        if (Fecha.Month <= FechaIncremento.Month && Fecha.Day < FechaIncremento.Day)
                        {
                            if (i > 0)
                            {
                                if (incrementos[i].anhio >= FechaIncremento.Year && incrementos[i].anhio <= Fecha.Year - 1)
                                {
                                    valorC += (valorC * ((float)incrementos[i].incremento / 100));
                                }
                                if (Fecha.Year == incrementos[i].anhio)
                                {
                                    Correcto = true;
                                }
                            }

                        }
                        else
                        {
                            if (incrementos[i].anhio >= FechaIncremento.Year && incrementos[i].anhio <= Fecha.Year)
                            {
                                valorC += (valorC * ((float)incrementos[i].incremento / 100));
                            }
                            if (Fecha.Year == incrementos[i].anhio)
                            {
                                Correcto = true;
                            }
                        }

                    }
                }
                else
                {
                    Correcto = true;
                    if (Fecha.Month <= FechaIncremento.Month && Fecha.Day < FechaIncremento.Day)
                    {
                        for (int i = FechaIncremento.Year; i <= Fecha.Year - 1; i++)
                        {
                            valorC += (valorC * (float.Parse(Incremento) / 100));
                        }
                    }
                    else
                    {
                        for (int i = FechaIncremento.Year; i <= Fecha.Year; i++)
                        {
                            valorC += (valorC * (float.Parse(Incremento) / 100));
                        }
                    }
                }

                if (!Correcto)
                {
                    valorC = 0;
                }
                
                return valorC;
            }
            else
            {
                
                return valorC;
            }
        }

        [HttpPost]
        public IActionResult GetValoresFechas(string jsonCuotas, DateTime FechaIncremento, string Incremento, float ValorCuota)
        {
            JArray respuesta = new JArray();
            JArray json = JArray.Parse(jsonCuotas);

            ConexionDB consultas = new ConexionDB();
            List<incrementoInteranualTasas> incrementos = new List<incrementoInteranualTasas>();
            if (Incremento.Equals("IPC") || Incremento.Equals("SML"))
            {
                incrementos = consultas.ObtenerIncremento(Incremento);
            }

            foreach (JObject e in json)
            {
                JObject v = new JObject();
                v.Add("fecha", e.GetValue("fecha"));
                DateTime f = DateTime.Parse(e.GetValue("fecha") + " 00:00:00.000");
                float res = GetValorCuota(f, FechaIncremento, Incremento, ValorCuota,incrementos);
                v.Add("valor", res);               
                respuesta.Add(v);
            }
            return Json(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerTasas(DateTime fecha, bool VIS, string moneda) {
            if (moneda == "UVR" && VIS && fecha >= DateTime.Parse("01/01/2000"))
                return Json(await _consultas.CalcTasa("UVIS", fecha));
            else if (moneda == "UVR" && !VIS && fecha >= DateTime.Parse("03/09/2000"))
                return Json(await _consultas.CalcTasa("UNVI", fecha));
            else if (moneda == "PESOS" && VIS && fecha >= DateTime.Parse("01/01/2001"))
                return Json(new { variacionUVR = _consultas.CalcTasa("VUVR", fecha).Result.ValorTasa, maxIntRemunatorio = _consultas.CalcTasa("PVIS", fecha).Result.ValorTasa });
            else if (moneda == "PESOS" && !VIS && fecha >= DateTime.Parse("03/09/2000"))
                return Json(new { variacionUVR = _consultas.CalcTasa("VUVR", fecha).Result.ValorTasa, maxIntRemunatorio = _consultas.CalcTasa("PNVI", fecha).Result.ValorTasa });
            else
                return NoContent();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private class RespJSON
        {
            public List<Liquidacion> detalle_liquidacion;
            public List<(string, double)> resumen;
            public List<(string, double, double)> resumen_hip;
            
            public RespJSON(List<Liquidacion> detalle_liquidacion, List<(string, double)> resumen)
            {
                this.detalle_liquidacion = detalle_liquidacion;
                this.resumen = resumen;
            }
            /// <summary>
            /// Constructor para Liquidación Hipotecaria en UVR's
            /// </summary>
            /// <param name="detalle_liquidacion">Tabla de la Liquidación</param>
            /// <param name="resumen">Resumen de la liquidación.</param>
            public RespJSON(List<Liquidacion> detalle_liquidacion, List<(string, double, double)> resumen)
            {
                this.detalle_liquidacion = detalle_liquidacion;
                resumen_hip = resumen;
            }
        }
        /// <summary>
        /// Respuesta JSON para las liquidaciones múltiples.
        /// </summary>
        private class RespMultiple
        {
            public RespJSON[] data;
            public List<(string, double)> resumen_general;

            public RespMultiple(RespJSON[] data, List<(string, double)> resumen_general) {
                this.data = data;
                this.resumen_general = resumen_general;
            }
        }

        public void ValidarRespuesta(dynamic json, int pos)
        {
            string ctp = (string)json[pos].CodTipoProceso;
            string ntp = (string)json[pos].TipoProceso;
            string ccp = (string)json[pos].CodClaseProceso;
            string ncp = (string)json[pos].ClaseProceso;
            string cscp = (string)json[pos].CodSubclaseProceso;
            string nscp = (string)json[pos].SubclaseProceso;
            var tp = _context.TipoProceso.Where(x => x.codiproc == ctp);
            if (tp.Count() == 0)
            {
                TipoProceso tipoProceso = new TipoProceso
                {
                    codiproc = ctp,
                    descproc = ntp
                };
                _context.TipoProceso.Add(tipoProceso);
            }
            var c = _context.Clase.Where(x => x.codiclas == ccp);
            if (c.Count() == 0)
            {
                Clase clase = new Clase
                {
                    codiclas = ccp,
                    descclas = ncp
                };
                _context.Clase.Add(clase);
            }
            var sc = _context.SubClase.Where(x => x.codisubc == cscp);
            if (sc.Count() == 0)
            {
                SubClase subClase = new SubClase
                {
                    codisubc = cscp,
                    descsubc = nscp
                };
                _context.SubClase.Add(subClase);
            }
            _context.SaveChanges();
        }
    }
}