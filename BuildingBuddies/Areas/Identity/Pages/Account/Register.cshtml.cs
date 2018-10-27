using BuildingBuddies.Helpers;
using BuildingBuddies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BuildingBuddies.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly BuildingBuddiesContext _context;
        public SelectList DepartmentNameSL { get; set; }
        public Department Department { get; set; }

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            BuildingBuddiesContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool MeetingRegistration { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Department")]
            public Department Department { get; set; }

            public int DepartmentID { get; set; }

        }

        public void PopulateDepartmentsDropDownList(BuildingBuddiesContext _context, string meetingLink, object selectedDepartment = null)
        {
            var meetingID = from m in _context.Meeting
                            where m.Link.Contains(meetingLink)
                            select m.MeetingID;

            var departmentsQuery = from d in _context.Department
                                   where d.MeetingID == meetingID.First()
                                   orderby d.Name
                                   select d;

            DepartmentNameSL = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        }

        public void OnGet(string meetingLink = null, string returnUrl = null)
        {
            // ako nema linka, radi se o registraciji sastanka
            MeetingRegistration = meetingLink == null ? true : false;

            ViewData["meetingRegistration"] = MeetingRegistration;
            if (!MeetingRegistration)
            {
                PopulateDepartmentsDropDownList(_context, meetingLink);
            }

            ReturnUrl = returnUrl;
        }

        public async void SendConfirmationEmail(User user)
        {
            _logger.LogInformation("User created a new account with password.");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code = code },
                protocol: Request.Scheme);

            MailSender ms = new MailSender();

            await ms.Send(user.Email, "Confirm your email", $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");
        }

        public async Task<IActionResult> OnPostAsync(string meetingLink = null)
        {
            string returnUrl = Url.Content("~/RegistrationSuccessful");

            if (meetingLink == null)
            {
                if (ModelState.IsValid)
                {
                    var user = new User { UserName = LinkGenerator.GenerateRandomString(10), Email = Input.Email, MeetingOrganizer = true };

                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        SendConfirmationEmail(user);
                        return LocalRedirect(returnUrl);
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return Page();
            }
            else
            {
                // traži se postoji li sastanak na tom linku
                Meeting SourceMeeting = await _context.Meeting.Where(m => m.Link.Contains(meetingLink)).FirstOrDefaultAsync();

                // provjera da je u ispravnoj domeni
                if (!Input.Email.Contains($"@{SourceMeeting.Domain}"))
                {
                    ModelState.AddModelError("Email", "Email is not correct");
                }

                if (ModelState.IsValid && SourceMeeting != null)
                {
                    var user = new User { UserName = LinkGenerator.GenerateRandomString(10), Email = Input.Email, DepartmentID = Input.DepartmentID, MeetingID = SourceMeeting.MeetingID };

                    if (SourceMeeting.Domain == user.Email.Split('@')[1])
                    {
                        user.MeetingID = SourceMeeting.MeetingID;
                        var result = await _userManager.CreateAsync(user, Input.Password);

                        if (result.Succeeded)
                        {
                            SendConfirmationEmail(user);
                            TempData["UserMessageTitle"] = "Success!";
                            TempData["UserMessageText"] = "Please check your inbox for the confirmation mail.";
                            return LocalRedirect(returnUrl);
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }

                // If we got this far, something failed, redisplay form
                PopulateDepartmentsDropDownList(_context, meetingLink);
                return Page();
            }
        }
    }
}
