using BuildingBuddies.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly BuildingBuddiesContext _context;

        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, BuildingBuddiesContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
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
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _context.User.Where(u => u.NormalizedEmail == Input.Email.ToUpper()).FirstOrDefaultAsync();

            var MeetingOrganizer = false;
            var connected = false;

            if (user?.MeetingOrganizer == true)
            {
                returnUrl += "meetings";
                MeetingOrganizer = true;
            }
            else if (user?.AgreedMeetingID != null)
            {
                returnUrl += "signalr";
                connected = true;
            }
            else
            {
                returnUrl += "Countdown";
            }

            ViewData.Add("MeetingOrganizer", MeetingOrganizer);
            ViewData.Add("Connected", connected);

            if (ModelState.IsValid && user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.NormalizedUserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                var confirmedEmail = user.EmailConfirmed;

                if (result.Succeeded)
                {
                    if (!confirmedEmail)
                    {
                        ModelState.AddModelError(string.Empty, "You have to confirm your email to log in.");
                        return Page();
                    }
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
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
