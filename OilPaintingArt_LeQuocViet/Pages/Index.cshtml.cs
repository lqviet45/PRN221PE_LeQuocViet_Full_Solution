using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.ComponentModel.DataAnnotations;

namespace OilPaintingArt_LeQuocViet.Pages
{
    public class IndexModel : PageModel
    {
        [Required]
        [BindProperty]
        public string Username { get; set; } = null!;

        [Required]
        [BindProperty]
        public string Password { get; set; } = null!;

        private readonly IAccountRepo _accountRepo;
        public IndexModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var account = await _accountRepo.Login(Username, Password);
                HttpContext.Session.SetString("Role", account.Role.ToString() ?? "");

                return RedirectToPage("/painting/index");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("You do not have"))
                {
                    ModelState.AddModelError("LoginFailed", ex.Message);
                }
                return Page();
            }
        }		
    }
}
