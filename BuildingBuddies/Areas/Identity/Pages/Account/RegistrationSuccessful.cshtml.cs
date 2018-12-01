using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildingBuddies.Pages
{
    [AllowAnonymous]
    public class RegistrationSuccessfulModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}