using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using liquidador_web.Data;
using liquidador_web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace liquidador_web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<LiquidadorUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ResetPasswordModel(UserManager<LiquidadorUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("Debe proveer un código para restaurar su contraseña.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }
            var oldUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                _context.Auditoria.Add(new Auditoria { usuario = user, modulo = "Usuarios", evento = "Actualizar", fecha = System.DateTime.Now, logAnterior = oldUser, logActual = Newtonsoft.Json.JsonConvert.SerializeObject(user)});

                try { await _context.SaveChangesAsync(); } catch (System.Exception) { }
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
