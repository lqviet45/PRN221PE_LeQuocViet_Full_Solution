using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Repository;

namespace OilPaintingArt_LeQuocViet.Pages.painting
{
    public class DeleteModel : PageModel
    {
        private readonly IPaintingRepo _paintingRepo;

        public DeleteModel(IPaintingRepo paintingRepo)
        {
            _paintingRepo = paintingRepo;
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oilpaintingart = await _paintingRepo.GetOilPaintingArtById(id.Value);

            if (oilpaintingart == null)
            {
                return NotFound();
            }
            else
            {
                OilPaintingArt = oilpaintingart;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var isSuccess = await _paintingRepo.RemovePainting(id.Value);
                if (!isSuccess)
                {
                    throw new Exception("Error when deleting");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
