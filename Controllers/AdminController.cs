using liquidador_web.Data;
using liquidador_web.Extra;
using liquidador_web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;

namespace liquidador_web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<LiquidadorUser> users;
        private readonly RoleManager<IdentityRole> roles;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public AdminController(UserManager<LiquidadorUser> users, RoleManager<IdentityRole> roles, ApplicationDbContext dbContext, ApplicationDbContext context, IEmailSender emailSender, IConfiguration config) {
            this.users = users;
            this.roles = roles;
            _context = context;
            _emailSender = emailSender;
            _config = config;
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Administración de Usuarios";

            List<IList<string>> rolesByUser = new List<IList<string>>();

            var currentUser = await users.GetUserAsync(User);
            var isGod = await users.IsInRoleAsync(currentUser, "Administrador General");
            var webServiceUser = await users.FindByIdAsync("servicioweb");

            if (isGod)
            {
                foreach (LiquidadorUser usuario in users.Users.Except(new [] { webServiceUser }))
                    rolesByUser.Add(await users.GetRolesAsync(usuario));

                ViewData["roles"] = rolesByUser;
                return View(users.Users.Except(new [] { webServiceUser }));
            }
            else {
                var usuarios = users.Users.Where(x => x.Creator == currentUser.Id);

                foreach (LiquidadorUser usuario in usuarios)
                    if (usuario.Creator == currentUser.Id)
                        rolesByUser.Add(await users.GetRolesAsync(usuario));

                ViewData["roles"] = rolesByUser;

                return View(usuarios);
            }
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        public IActionResult Create() {
            ViewData["Title"] = "Crear Usuario";
            ViewData["roles"] = roles.Roles;
            ConexionDB conexion = new ConexionDB();
            ViewData["distritos"] = conexion.ListarDistritos();

            return View();
        }

        [HttpPost]
        public List<MunicipioDespacho> GetMunicipios(string departamentos)
        {
            ConexionDB conexion = new ConexionDB();

            return conexion.ListarMunicipios(departamentos);
        }

        [HttpPost]
        public List<Despacho> GetDespachos(string departamento, string municipio, string entidad, string especialidad)
        {
            ConexionDB conexion = new ConexionDB();

            return conexion.ListarDespachos(departamento,municipio,entidad,especialidad);
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(string distrito, string role, [Bind("Email, FullName")] LiquidadorUser user) {

            if (!PasswordGenerator.IsValidEmail(user.Email, _config.GetSection("EmailDomains").Get<string[]>()))
            {
                TempData["Error"] = $"<b>ERROR:</b> <i>{user.Email}</i> No es una dirección de correo electrónico válida.";
                return RedirectToAction("Index");
            }

            user.UserName = user.Email;
            user.Active = true;
            user.Creator = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            string password = PasswordGenerator.GeneratePassword();
            while (!PasswordGenerator.PasswordIsValid(password))
                password = PasswordGenerator.GeneratePassword();

            user.Token = await users.GenerateEmailConfirmationTokenAsync(user);
            user.CodDespacho = distrito;
            IdentityResult result = await users.CreateAsync(user, password);



            if (result.Succeeded)
            {
                users.AddToRoleAsync(user, role).Wait();
                _context.Auditoria.Add(new Auditoria { usuario = await users.GetUserAsync(User), modulo = "Usuarios", fecha = DateTime.Now, evento = Eventos.Añadir, logActual = JsonConvert.SerializeObject(user) });

                //var code = await users.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Page(
                //        "/Account/ConfirmEmail",
                //        pageHandler: null,
                //        values: new { userId = user.Id, code },
                //        protocol: Request.Scheme);

                var callbackUrl = Url.Action("ConfirmEmail", "Account",
                    new { Area = "Identity", userId = user.Id, code = user.Token },
                    "https", "liquidador.ramajudicial.gov.co");
                TempData["UserCreated"] = $"El usuario {user.Email} se ha creado. Debe activar su cuenta haciendo click sobre el link que se le envió a su respectivo correo electrónico.";

                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "¡Bienvenido!",
                        $@"Bienvenido al nuevo liquidador de sentencias judiciales. 
                        Tu nombre de usuario es {user.Email} y tu contraseña es <b>{password}</b> 
                        Confirme su cuenta para activarla, y pueda cambiar su contraseña 
                        <a href='{callbackUrl}'>accediendo a este link</a>.");
                }
                catch (Exception) { }

                try { await _context.SaveChangesAsync(); } catch (Exception) {/*LOG*/}

                return RedirectToAction("Index");
            }
            else {
                TempData["Error"] = $"<b>ERROR:</b> el usuario <i>{user.Email}</i> no se pudo crear. Intentelo de nuevo.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        public async Task<IActionResult> Edit(string id) {
            ViewData["Title"] = "Editar Usuario";

            if (id == null)
                return NotFound();
            ViewData["role"] = "null";
            ViewData["distrito"] = "null";
            LiquidadorUser user = await users.FindByIdAsync(id);
            ViewData["roles"] = roles.Roles;
            IList<string> lista = await users.GetRolesAsync(user);
            ViewData["role"] = lista[0];
            ConexionDB conexion = new ConexionDB();
            if(user.CodDespacho!=null)
                ViewData["distrito"] = user.CodDespacho;

            ViewData["distritos"] = conexion.ListarDistritos();
            

            if (user == null)
                return NotFound();
            else
                return View(user);
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(string distrito, string id, string role, [Bind("Id, Email, FullName")] LiquidadorUser newUser) {
            if (id != newUser.Id)
                return NotFound();

            LiquidadorUser user = await users.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await users.GetRolesAsync(user);
            IdentityResult result = await users.RemoveFromRolesAsync(user, roles.ToArray());

            if (!result.Succeeded)
                return BadRequest();

            var oldUser = JsonConvert.SerializeObject(user);
            string oldEmail = user.Email;
            user.UserName = newUser.Email;
            user.Email = newUser.Email;
            user.FullName = newUser.FullName;
            user.NormalizedEmail = newUser.Email.ToUpper();
            user.NormalizedUserName = newUser.Email.ToUpper();
            user.CodDespacho = distrito;

            result = await users.UpdateAsync(user);

            if (result.Succeeded) {
                _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Usuarios", evento = Eventos.Actualizar, logAnterior = oldUser, logActual = JsonConvert.SerializeObject(user) });
                users.AddToRoleAsync(user, role).Wait();

                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "Actualización de Correo",
                        $@"Estimado usuario, se le informa que su correo electrónico, y su acceso a la aplicación del Liquidador de Sentencias Judiciales ha sido actualizado a la dirección de correo electrónico presente. Si cree que esto ha sido un error, contacte inmediatamente con el administrador.
                            Correo anterior: {oldEmail}
                            Correo actual: {user.Email}");
                }
                catch (Exception) { }

                try { await _context.SaveChangesAsync(); } catch (Exception) {/*LOG*/}

                TempData["Edited"] = $"El usuario ha sido actualizado.";

                return RedirectToAction("Index");
            } else
                return View();
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        public async Task<IActionResult> Delete(string id) {
            if (id == null)
                return NotFound();

            LiquidadorUser user = await users.FindByIdAsync(id);
            var tempEmail = user.Email;
            IdentityResult result = await users.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Eliminado"] = $"El usuario <i>{tempEmail}</i> ha sido eliminado.";
                _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Usuarios", evento = Eventos.Eliminar, logAnterior = JsonConvert.SerializeObject(user) });

                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "Usuario Eliminado",
                        $@"Hola {tempEmail}, se le informa que su usuario ha sido eliminado del liquidador de sentencias judiciales.
                        Si cree que esto es un error, por favor comuniquese con alguno de nuestros administradores.");
                }
                catch (Exception) { }

                try { await _context.SaveChangesAsync(); } catch (Exception) {/*LOG*/}
            }
            else
                TempData["Error"] = "<b>ERROR:</b> No se pudo eliminar al usuario.";

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        public async Task<IActionResult> Activate(string id, bool status)
        {
            if(id == null)
                return NotFound();

            LiquidadorUser user = await users.FindByIdAsync(id);

            try {
                var oldUser = JsonConvert.SerializeObject(user);
                user.Active = status;

                IdentityResult result = await users.UpdateAsync(user);

                if (result.Succeeded) {
                    TempData["Activated"] = $"<i>{user.Email}</i> ha sido {(user.Active ? "activado" : "inactivado")}.";
                    _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Usuarios", evento = Eventos.Actualizar, logAnterior = oldUser, logActual = JsonConvert.SerializeObject(user) });
                    if(!user.Active) //-- If deactivated, send email informing.
                        try
                        {
                            await _emailSender.SendEmailAsync(user.Email, "Usuario Desactivado",
                                $@"Hola {user.Email}, se le informa que su usuario ha sido desactivado para iniciar sesión en el liquidador de sentencias judiciales.
                                Si cree que esto es un error, por favor comuniquese con alguno de nuestros administradores.");
                        }
                        catch (Exception) { }
                }
                else
                    TempData["Error"] = $"ERROR: no se ha podido modificar el usuario <i>{user.Email}</i>. Intente de nuevo más tarde o contacte al administrador.";

                await _context.SaveChangesAsync();
            }
            catch (Exception) {
                TempData["Error"] = $"ERROR: Hubo un problema al actualizar el usuario <i>{user.Email}</i>. Intente de nuevo más tarde o contacte al administrador";
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador General, Administrador Seccional")]
        public async Task<IActionResult> ResendConfirmation(string id) {
            if (id == null)
                return NotFound();

            var targetUser = await users.FindByIdAsync(id);

            if (targetUser == null)
                return NotFound();

            if (targetUser.EmailConfirmed) {
                TempData["Confirmed"] = $"El usuario {targetUser.Email} ya validó su cuenta.";
                goto alreadyValidated;
            }

            try
            {
                var code = await users.GenerateEmailConfirmationTokenAsync(targetUser);

                targetUser.Token = code;
                
                await users.UpdateAsync(targetUser);

                var callbackUrl = Url.Action(
                    "ConfirmEmail", "Account",
                    new { Area = "Identity", userId = targetUser.Id, code },
                    "https", "liquidador.ramajudicial.gov.co"
                );

                await _emailSender.SendEmailAsync(targetUser.Email, "¡Bienvenido!",
                    $@"Bienvenido al nuevo liquidador de sentencias judiciales. 
                    Tu nombre de usuario es {targetUser.Email} y al confirmar el usuario debe regenerar su clave. 
                    Confirme su cuenta para activarla, y pueda cambiar su contraseña 
                    <a href='{callbackUrl}'>accediendo a este link</a>.");

                TempData["Sented"] = $"Ha sido re-envíado el email de validación para el usuario {targetUser.Email}.";
            }
            catch (Exception ex) {
                TempData["Error"] = $"<b>ERROR:</b> Hubo un problema al enviar el correo electrónico.<br><br>Información de la excepción: <i>{ex.Message}</i>";
            }

        alreadyValidated:
            return View();
        }

        [Authorize(Roles = "Administrador General, Administrador de Actualización de las Tasas")]
        public IActionResult Tasas() {
            ViewData["Title"] = "Crear Tasa";
            ViewData["Tasas"] = _context.TiposTasas.ToArray();
            ViewData["Periodos"] = _context.TiposPeriodo.ToArray();

            return View();
        }

        [Authorize(Roles = "Administrador General, Administrador de Actualización de las Tasas")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Tasas(Datasainte tasa) {
            if (!ModelState.IsValid)
               return RedirectToAction("Tasas");

            //var currentUser = await users.GetUserAsync(User);
            //tasa.CreatedBy = currentUser.Email;

            _context.DATASAINTE.Add(tasa);
            _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Tasas", evento = Eventos.Añadir, logActual = JsonConvert.SerializeObject(tasa) });

            try
            {
                var guardar = await _context.SaveChangesAsync();

                // ViewData["Tasas"] = _context.TiposTasas.ToArray();
                // ViewData["Periodos"] = _context.TiposPeriodo.ToArray();

                if (guardar > 0)
                    TempData["Created"] = "La tasa ha sido añadida";
                else
                    TempData["NotCreated"] = "No se pudo añadir la tasa";
            }
            catch (Exception ex) { 
                TempData["NotCreated"] = $"{ex.InnerException.Message}" ;
            }

            return RedirectToAction("Tasas");
        }

        [Authorize]
        public IActionResult ConsultaTasas() {
            ViewData["Title"] = "Consulta de Tasas";
            ViewData["Tasas"] = _context.TiposTasas.ToArray();

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ConsultaTasas(string tipo, DateTime fromDate, DateTime toDate) {
            IQueryable<Datasainte> q = _context.DATASAINTE;

            if (tipo != null || tipo != "")
                q = q.Where(data => data.TipoTasa == tipo);

            if (fromDate != DateTime.MinValue)
                q = q.Where(data => data.VigenteDesde >= fromDate);

            if (toDate != DateTime.MinValue)
                q = q.Where(data => data.VigenteHasta <= toDate);

            if (await q.AnyAsync())
                return Json(q.ToList());
            else
                return NoContent();
        }

        [Authorize(Roles = "Administrador General, Administrador de Actualización de las Tasas")]
        public async Task<IActionResult> EditarTasa(int id) {
            ViewData["Title"] = "Editar Tasa";

            ViewData["Tasas"] = _context.TiposTasas.ToArray();
            ViewData["Periodos"] = _context.TiposPeriodo.ToArray();

            var tasa = await _context.DATASAINTE.FindAsync(id);

            if (tasa == null)
                return NotFound();

            return View(tasa);
        }

        [Authorize(Roles = "Administrador General, Administrador de Actualización de las Tasas")]
        [HttpPost]
        public async Task<IActionResult> EditarTasa([Bind("IDTasa", "TipoTasa", "VigenteDesde", "VigenteHasta", "ValorTasa", "Periodo", "ResVigencia")] Datasainte newTasa) {
            if (!ModelState.IsValid)
                return RedirectToAction("ConsultaTasas");

            var oldTasa = JsonConvert.SerializeObject(await _context.DATASAINTE.AsNoTracking().FirstAsync(data => data.IDTasa == newTasa.IDTasa));

            _context.Update(newTasa);
            _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Tasas", evento = Eventos.Actualizar, logActual = JsonConvert.SerializeObject(newTasa), logAnterior = oldTasa });
            try
            {
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                    TempData["Edited"] = "La tasa ha sido actualizada";
                else
                    TempData["Error"] = $"<b>ERROR:</b> La tasa no pudo ser actualizada. Por favor, intentelo de nuevo.";
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["Error"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
            }
            catch (DbUpdateException) {
                TempData["Error"] = $"<b>ERROR:</b> No se pudo realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
            }

            ViewData["Tasas"] = _context.TiposTasas.ToArray();
            ViewData["Periodos"] = _context.TiposPeriodo.ToArray();

            return View(newTasa);
        }

        [Authorize(Roles = "Administrador General, Administrador de Actualización de las Tasas")]
        public async Task<IActionResult> DeleteTasa(int id) {

            var tasa = await _context.DATASAINTE.FindAsync(id);

            if (tasa == null)
                return NotFound();

            _context.DATASAINTE.Remove(tasa);
            _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Tasas", evento = Eventos.Eliminar, logAnterior = JsonConvert.SerializeObject(tasa) });

            try
            {
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                    TempData["Deleted"] = "La tasa ha sido eliminada";
                else
                    TempData["Error"] = $"<b>ERROR:</b> La tasa no pudo ser eliminada. Por favor, intentelo de nuevo.";
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["Error"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
            }
            catch (DbUpdateException) {
                TempData["Error"] = $"<b>ERROR:</b> No se pudo completar la operación en la base de datos. Intentelo de nuevo.";
            }

            return RedirectToAction("ConsultaTasas");
        }

        [Authorize(Roles = "Administrador General")]
        public async Task<IActionResult> Auditorias(DateTime desde, DateTime hasta, string usuario, string modulo, int? pageNumber)
        {
            ViewData["Title"] = "Auditorias" ;

            var registros = from s in _context.Auditoria select s;
            var items = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            ViewData["actualDesde"] = desde != DateTime.MinValue ? desde.ToString("yyyy-MM-dd") : "";
            ViewData["actualHasta"] = hasta != DateTime.MinValue ? hasta.ToString("yyyy-MM-dd") : "";
            ViewData["actualModulo"] = modulo;
            ViewData["actualUsuario"] = usuario;

            if (desde != DateTime.MinValue)
                registros = registros.Where(data => data.fecha >= desde);

            if(hasta != DateTime.MinValue)
                registros = registros.Where(data => data.fecha <= hasta.AddHours(23).AddMinutes(59).AddSeconds(59));

            if (!string.IsNullOrEmpty(modulo))
                registros = registros.Where(data => data.modulo == modulo);

            if (!string.IsNullOrEmpty(usuario))
                registros = registros.Where(data => data.usuario.Email == usuario);

            registros = registros.OrderByDescending(data => data.fecha);

            return View(await Paginador<Auditoria>.CreateAsync(registros.Include(elemento => elemento.usuario), pageNumber ?? 1, 10));
        }

        [Authorize(Roles = "Administrador General")]
        public async Task<IActionResult> Auditoria(int id)
        {
            ViewData["Title"] = "Auditorias";

            ConexionDB c = new ConexionDB();

            var prueba = await _context.Auditoria.Include(data => data.usuario).FirstAsync(data => data.ID == id);

            return View("Auditoria", prueba );
        }

        [Authorize(Roles = "Administrador General")]
        public IActionResult Reporte()
        {
            ViewData["Title"] = "Reportes";

            ViewData["roles"] = roles.Roles;

            return View();
            
            
        }

        [Authorize(Roles = "Administrador General")]
        [HttpPost]
        public async Task<IActionResult> Reporte(string tipo,string rolUser)
        {
            ViewData["Title"] = tipo;

            var liqui = from l in _context.Liquidaciones select l;
            var DataL = from dl in _context.DataLiquidacion select dl;
            var usersR = from u in _context.Users select u;
            
            if (tipo == "Liquidaciones Realizadas" && await liqui.AnyAsync())
                return Json(liqui.Include(elemento => elemento.tipo).Include(data => data.autor));
            else if (tipo == "Liquidaciones Guardadas" && await DataL.AnyAsync())
                return Json(DataL.Include(elemento => elemento.creador));
            else if (tipo == "Usuarios registrados" && await usersR.AnyAsync()) {

                if (rolUser != "Seleccionar")
                {
                    var query = from u in _context.Users
                                join ur in _context.UserRoles on u.Id equals ur.UserId
                                where ur.RoleId == rolUser
                                select u;
                    return Json(query.ToList());

                }
                else
                {
                    return Json(usersR.ToList());
                }
                    

                
            }
            else
                return NoContent();

        }

        public async Task<IActionResult> DescargaExcel()
        {
            MemoryStream memoryStream = new MemoryStream();

            XLWorkbook workbook = new XLWorkbook();
            workbook.Author = "Liquidador de Sentencias Judiciales Web";
            var workSheet = workbook.Worksheets.Add("Liquidación Intereses Plazo y M");
            int i = 5;

            workSheet.Range("A1:D1").Merge().SetValue("República de Colombia").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A2:D2").Merge().SetValue("Consejo Superior de la Judicatura").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A3:D3").Merge().SetValue("RAMA JUDICIAL").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            workSheet.Cell("A5").SetValue("Cantidad de usuarios registrados").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
           // workSheet.Cell("A6").SetValue("Cant de usuarios Administrador General").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
           // workSheet.Cell("A7").SetValue("Cant de usuarios Liquidador").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
           // workSheet.Cell("A8").SetValue("Cant de usuarios Juez").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
           // workSheet.Cell("A9").SetValue("Cant de usuarios Administrador de Actualización de las Tasas").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
           // workSheet.Cell("A10").SetValue("Cant de usuarios Administrador Seccional").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            workSheet.Cell("A11").SetValue("Cantidad de liquidadciones realizadas dentro del sistema").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            workSheet.Cell("A12").SetValue("Cantidad de Liquidaciones guardadas").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            workSheet.Cell("A14").SetValue("Top 3 Usuarios con mas liquidaciones").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            workSheet.Cell("A18").SetValue("Top 3 Usuarios con mas liquidaciones Guardadas").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            var liqui = from l in _context.Liquidaciones select l;
            var DataL = from dl in _context.DataLiquidacion select dl;
            var usersR = from u in _context.Users select u;

            foreach (IdentityRole role in (IQueryable<IdentityRole>)roles.Roles)
            {
                workSheet.Cell("A"+(++i)).SetValue("Cantidad de usuarios "+role.Name).Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                workSheet.Cell("B"+i).Value = (from u in _context.Users
                                              join ur in _context.UserRoles on u.Id equals ur.UserId
                                              where ur.RoleId == role.Id
                                              select u).Count();
            }
           
            workSheet.Cell("B5").Value = usersR.Count();
            workSheet.Cell("B11").Value = liqui.Count();
            workSheet.Cell("B12").Value = DataL.Count();
            var ul = from dal in _context.Liquidaciones
                      join u in _context.Users on dal.autor.Id equals u.Id
                      group u by u.Email into g orderby g.Count() descending
                      select new { name= g.Key, count =g.Count()};
            var j = 0; i = 13;
            ul.ToList().ForEach( data =>
            {   if (j < 3)
                {
                    workSheet.Cell("B" + (++i)).Value = data.count;
                    workSheet.Cell("C" + (i)).Value = data.name;
                }
                    

                ++j;
            });

            var udl = from dal in _context.DataLiquidacion
                     join u in _context.Users on dal.creador.Id equals u.Id
                     group u by u.Email into g
                     orderby g.Count() descending
                     select new { name = g.Key, count = g.Count() };
            j = 0; i = 17;
            udl.ToList().ForEach(data =>
            {
                if (j < 3)
                {
                    workSheet.Cell("B" + (++i)).Value = data.count;
                    workSheet.Cell("C" + (i)).Value = data.name;
                }


                ++j;
            });

            workSheet.Columns(1, 18).AdjustToContents(); //-- Ajusta el tamaño de las columnas, a su contenido.

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte General" + ".xlsx");
               
        }

        public IActionResult AdminAyuda()
        {
            ViewData["Title"] = "Administracion de Ayuda";

            IQueryable<Ayuda> q = _context.Ayuda;            

            return View(q);
        }

        public IActionResult CreateAyuda()
        {
            ViewData["Title"] = "Crear Ayuda";
            ViewData["roles"] = roles.Roles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAyuda(List<IFormFile> urlDocumento, string titulo, string[] role)
        {
            var roles = "";
            foreach(var r in role)
            {
                roles += r+"/";
            }
            if(urlDocumento.Count > 0)
            {
                foreach(var file in urlDocumento)
                {
                    var filePath = "wwwroot/documentos/"+file.FileName;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    Ayuda ayudaNew = new Ayuda();
                    ayudaNew.titulo = titulo;
                    ayudaNew.urlDocumento = filePath;
                    ayudaNew.roles = roles;
                    ayudaNew.fecha = DateTime.Now;

                    _context.Ayuda.Add(ayudaNew);
                    _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Ayuda", evento = Eventos.Añadir, logActual = JsonConvert.SerializeObject(ayudaNew) });
                    try
                    {
                        var guardar = await _context.SaveChangesAsync();

                        // ViewData["Tasas"] = _context.TiposTasas.ToArray();
                        // ViewData["Periodos"] = _context.TiposPeriodo.ToArray();

                        if (guardar > 0)
                            TempData["Created"] = "La ayuda ha sido añadida";
                        else
                            TempData["NotCreated"] = "No se pudo añadir la ayuda";
                    }
                    catch (Exception ex)
                    {
                        TempData["NotCreated"] = $"{ex.InnerException.Message}";
                    }
                }
                
            }

            return RedirectToAction("AdminAyuda");
        }

        public async Task<IActionResult> DeleteAyuda(int id)
        {

            var ayuda = await _context.Ayuda.FindAsync(id);

            if (ayuda == null)
                return NotFound();

            _context.Ayuda.Remove(ayuda);
            _context.Auditoria.Add(new Auditoria { fecha = DateTime.Now, usuario = await users.GetUserAsync(User), modulo = "Ayuda", evento = Eventos.Eliminar, logAnterior = JsonConvert.SerializeObject(ayuda) });

            try
            {
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                    TempData["Removed"] = "La ayuda ha sido eliminada";
                else
                    TempData["Error"] = $"<b>ERROR:</b> La ayuda no pudo ser eliminada. Por favor, intentelo de nuevo.";
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["Error"] = $"<b>ERROR:</b> Un error de concurrencia no permitió realizar la operación en la base de datos. Por favor, intentelo de nuevo.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = $"<b>ERROR:</b> No se pudo completar la operación en la base de datos. Intentelo de nuevo.";
            }

            return RedirectToAction("AdminAyuda");
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName.Replace("wwwroot/documentos/",""));
        }
    }
}
