using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPaintingRepo
    {
        Task<(int, List<OilPaintingArt>)> GetAll(string? search, int currentPage = 1, int limit = 2);
        Task<OilPaintingArt?> GetOilPaintingArtById(int id);
        Task<bool> AddPainting(OilPaintingArt oilPaintingArt);
        Task<bool> UpdatePainting(OilPaintingArt oilPaintingArt);
        Task<bool> RemovePainting(int id);
    }
}
