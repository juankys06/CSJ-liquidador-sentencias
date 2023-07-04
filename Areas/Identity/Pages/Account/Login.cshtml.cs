using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using liquidador_web.Models;
using liquidador_web.Extra;
using Microsoft.Extensions.Options;

namespace liquidador_web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<LiquidadorUser> _signInManager;
        private readonly UserManager<LiquidadorUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private GoogleReCaptcha reCaptcha { get; set; }

        public LoginModel(SignInManager<LiquidadorUser> signInManager, ILogger<LoginModel> logger, UserManager<LiquidadorUser> userManager, IOptions<GoogleReCaptcha> settings)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            reCaptcha = settings.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Correo Electrónico")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "¿Recordarme?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            ViewData["ReCaptchaKey"] = reCaptcha.key;

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ViewData["ReCaptchaKey"] = reCaptcha.key;
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {

                if (!GoogleReCaptcha.ReCaptchaPassed(
                    Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
                    reCaptcha.secret,
                    _logger
                    ))
                {
                    ModelState.AddModelError(string.Empty, "Ha fallado el Captcha, por favor intentelo de nuevo.");
                    return Page();
                }

                var user = await _userManager.FindByEmailAsync(Input.Email);

                if (user == null) {
                    ModelState.AddModelError("Error", "Ese usuario no existe.");
                    return Page();
                }

                var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);

                if (!isConfirmed) {
                    ModelState.AddModelError(string.Empty, "Debe validar su cuenta para iniciar sesión.");
                    return Page();
                }

                if (!user.Active){
                    ModelState.AddModelError(string.Empty, "Su cuenta está inactiva. Contacte al administrador.");
                    return Page();
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
