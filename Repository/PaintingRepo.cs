using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PaintingRepo : IPaintingRepo
    {
        public async Task<bool> AddPainting(OilPaintingArt oilPaintingArt)
        {
            bool isSuccess;
            try
            {
                isSuccess = await PaintingDao.AddAsync(oilPaintingArt);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public async Task<(int, List<OilPaintingArt>)> GetAll(string? search, int currentPage = 1, int limit = 2)
        {
            var query = PaintingDao.GetAll();
            if (search is not null)
            {
                query = query.Where(o => o.Artist.Contains(search) || o.OilPaintingArtStyle.Contains(search));
            }

            var count = await query.CountAsync();

            var result = await query
                .OrderByDescending(o => o.CreatedDate)
                .Include(o => o.Supplier)
                .Skip((currentPage - 1) * limit)
                .Take(limit)
                .ToListAsync();

            var total = (int)Math.Ceiling(decimal.Divide(count, limit));

            return (total, result);
        }

        public async Task<OilPaintingArt?> GetOilPaintingArtById(int id)
        {
            return await PaintingDao.GetByIdAsync(id);
        }

        public async Task<bool> RemovePainting(int id)
        {
            bool isSuccess;
            try
            {
                var painting = await PaintingDao.GetByIdAsync(id);
                if (painting == null)
                {
                    return false;
                }
                isSuccess = await PaintingDao.DeleteAsync(painting);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public async Task<bool> UpdatePainting(OilPaintingArt oilPaintingArt)
        {
            bool isSuccess;
            try
            {
                var painting = await PaintingDao.GetByIdAsync(oilPaintingArt.OilPaintingArtId);
                if (painting == null)
                {
                    return false;
                }
                painting = oilPaintingArt;
                isSuccess = await PaintingDao.UpdateAsync(painting);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
