using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using liquidador_web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace liquidador_web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<LiquidadorUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<LiquidadorUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                // var callbackUrl = Url.Page(
                //     "/Account/ResetPassword",
                //     pageHandler: null,
                //     values: new { code },
                //     protocol: Request.Scheme);

                var callbackUrl = Url.Action(
                    "ResetPassword", "Account",
                    new { Area = "Identity", code },
                    "https", "liquidador.ramajudicial.gov.co"
                );

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Restaurar Contraseña",
                    $"Por favor, para restaurar su contraseña <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haga click aquí</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
