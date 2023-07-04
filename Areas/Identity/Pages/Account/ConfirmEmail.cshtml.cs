using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using liquidador_web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace liquidador_web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<LiquidadorUser> _userManager;

        public ConfirmEmailModel(UserManager<LiquidadorUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
                return RedirectToPage("/Index");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound($"No se pudo cargar el usuario con el ID '{userId}'.");
           
            if (user.Token != code)
            {
                throw new InvalidOperationException($"Error confirmando el correo electrónico del usuario con el ID '{user.Email}':");
            }
            else
            {
                user.EmailConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
            }
                

            return Page();
        }
    }
}
