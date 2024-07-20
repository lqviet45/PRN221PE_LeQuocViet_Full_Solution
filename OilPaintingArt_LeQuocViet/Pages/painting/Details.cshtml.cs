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
    public class DetailsModel : PageModel
    {
        
        private readonly IPaintingRepo _paintingRepo;
        public DetailsModel(IPaintingRepo paintingRepo)
        {
            _paintingRepo = paintingRepo;
        }

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
    }
}
