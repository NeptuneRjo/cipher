using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CipherApp.API.Pages
{
    public class SignoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync();

            return RedirectToPage("/Login");
        }
    }
}
