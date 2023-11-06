using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CipherApp.API.Pages.Chat
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
