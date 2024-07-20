using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Repository;

namespace OilPaintingArt_LeQuocViet.Pages.painting
{
    public class EditModel : PageModel
    {
        private readonly IPaintingRepo _paintingRepo;
        private readonly ISupplierRepo _supplierRepo;

        public EditModel(IPaintingRepo paintingRepo, ISupplierRepo supplierRepo)
        {
            _paintingRepo = paintingRepo;
            _supplierRepo = supplierRepo;
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oilpaintingart =  await _paintingRepo.GetOilPaintingArtById(id.Value);
            if (oilpaintingart == null)
            {
                return NotFound();
            }
            OilPaintingArt = oilpaintingart;
            ViewData["SupplierId"] = new SelectList(await _supplierRepo.GetSupplierCompanies(), "SupplierId", nameof(SupplierCompany.CompanyName));
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["SupplierId"] = new SelectList(await _supplierRepo.GetSupplierCompanies(), "SupplierId", nameof(SupplierCompany.CompanyName));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ValidateTitle(OilPaintingArt.ArtTitle, true, false, true))
            {
                ModelState.AddModelError("Error", "Invalid Title");
                return Page();
            }

            try
            {
                var isSuccess = await _paintingRepo.UpdatePainting(OilPaintingArt);
                if (!isSuccess)
                {
                    throw new Exception("Error when adding painting");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private bool ValidateTitle(string title,
            bool mustStartWithUpperCaseEachWord,
            bool allowDigit = true,
            bool allowSpecialChar = true)
        {
            string[] words = title.Split(" ");
            foreach (var word in words)
            {
                if (mustStartWithUpperCaseEachWord && char.IsLower(word[0]))
                {
                    return false;
                }
                if (!allowDigit && word.Any(char.IsDigit))
                {
                    return false;
                }

                if (!allowSpecialChar && word.Any(char.IsPunctuation))
                {
                    return false;
                }
            }

            return true;
        }

    }
}
