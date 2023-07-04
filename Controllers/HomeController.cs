using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using liquidador_web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using liquidador_web.Data;
using liquidador_web.Interfaces;

namespace liquidador_web.Controllers
{
    public class HomeController : Controller
    {

        
        private readonly ApplicationDbContext _context;

        public HomeController( ApplicationDbContext dbContext, ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Inicio";

            if (User.Identity.IsAuthenticated)
                return View();
            else
                return Redirect("Identity/Account/Login");
        }

        public IActionResult Ayuda()
        {
            ViewData["Title"] = "Documentos";

            IQueryable<Ayuda> q = _context.Ayuda.OrderBy(obj => obj.titulo);

            return View(q);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Liquidador Judicial de Sentencias";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contacto";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult PdfSimple() {
        //    return File(Extra.Utilities.PdfSample(), "application/pdf");
        //}
        [Route("busqueda")]
        public async System.Threading.Tasks.Task<IActionResult> Busqueda(string q, int? pageNumber, [FromServices]Data.ApplicationDbContext _context)
        {
            ViewData["title"] = "Búsqueda" ;
            
            var registros = from s in _context.Liquidaciones select s;

            if (q != null)
                registros = registros.Where(data => data.llaveproc.A103llavproc.Contains(q) || data.ID.Contains(q));

            return View(await Extra.Paginador<Liquidaciones>.CreateAsync(registros.Include(usuario => usuario.autor).Include(tipo => tipo.tipo).Include(llave => llave.llaveproc), pageNumber??1, 10));
        }

        public IActionResult CrearProceso([FromServices]Data.ApplicationDbContext _context) {
            ViewData["tipos"] = _context.TipoProceso.OrderBy(obj => obj.descproc).ToArray();
            ViewData["clases"] = _context.Clase.OrderBy(obj => obj.descclas).ToArray();
            ViewData["descripciones"] = _context.SubClase.OrderBy(obj => obj.descsubc).ToArray();

            ViewData["nombres"] = _context.Sujeto.ToArray();

            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> CrearProceso(string ciudad, string entidad, string especialidad, string despacho, string año, string codProceso, string numero, string tipo, string clase, string descripcion, string demandante_nombre, string demandante_id, string demandado_nombre, string demandado_id, [FromServices]Interfaces.IConsultas consultas)
        {
            try
            {
                await consultas.CrearProceso(ciudad, entidad, especialidad, despacho, año, codProceso, numero, tipo, clase, descripcion, demandante_nombre, demandante_id, demandado_nombre, demandado_id);
            } catch (DbUpdateConcurrencyException ex) {
                HttpContext.Response.StatusCode = 500;
                return Json(ex);
            } catch(DbUpdateException ex) {
                HttpContext.Response.StatusCode = 500;
                return Json(ex);
            } catch (Exceptions.NotFoundException ex) {
                HttpContext.Response.StatusCode = 404;
                return Json(ex);
            }

            return NoContent();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
