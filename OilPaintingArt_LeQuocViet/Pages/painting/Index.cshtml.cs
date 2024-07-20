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
    public class IndexModel : PageModel
    {

        public int TotalPage { get; set; }
        public int CurrentPage { get; set; } = 1;
        [BindProperty]
        public string? Search { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;

        private readonly IPaintingRepo _paintingRepo;
        public IndexModel(IPaintingRepo paintingRepo)
        {
            _paintingRepo = paintingRepo;
        }

        public IList<OilPaintingArt> OilPaintingArt { get;set; } = default!;

        public async Task OnGetAsync(string? search, int currentPage = 1)
        {
            Search = search ?? "";
            CurrentPage = currentPage;
            (TotalPage, OilPaintingArt) = await _paintingRepo.GetAll(search, currentPage);
        }

    }
}
