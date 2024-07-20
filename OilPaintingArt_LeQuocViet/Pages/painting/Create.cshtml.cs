using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess;
using Repository;

namespace OilPaintingArt_LeQuocViet.Pages.painting
{
    public class CreateModel : PageModel
    {
        private readonly IPaintingRepo _paintingRepo;
        private readonly ISupplierRepo _supplierRepo;

        public CreateModel(ISupplierRepo supplierRepo, IPaintingRepo paintingRepo)
        {
            _supplierRepo = supplierRepo;
            _paintingRepo = paintingRepo;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["SupplierId"] = new SelectList(await _supplierRepo.GetSupplierCompanies(), "SupplierId", nameof(SupplierCompany.CompanyName));
            return Page();
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;

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
                OilPaintingArt.CreatedDate = DateTime.Now;
                var isSuccess = await _paintingRepo.AddPainting(OilPaintingArt);
                if (!isSuccess)
                {
                    throw new Exception("Error when Add painting");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
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
