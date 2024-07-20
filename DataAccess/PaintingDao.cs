using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class PaintingDao
    {
        public static IQueryable<OilPaintingArt> GetAll()
        {
            var context = new OilPaintingArt2024DbContext();

            return context.OilPaintingArts.AsQueryable();
        }

        public static async Task<OilPaintingArt?> GetByIdAsync(int id)
        {
            var context = new OilPaintingArt2024DbContext();

            return await context.OilPaintingArts
                .Include(o => o.Supplier)
                .SingleOrDefaultAsync(c => c.OilPaintingArtId == id);
        }

        public static async Task<bool> AddAsync(OilPaintingArt OilPaintingArt)
        {
            var context = new OilPaintingArt2024DbContext();
            var max = await context.OilPaintingArts.MaxAsync(t => t.OilPaintingArtId);
            OilPaintingArt.OilPaintingArtId = max + 1;

            context.Add(OilPaintingArt);

            return await context.SaveChangesAsync() > 0;
        }

        public static async Task<bool> UpdateAsync(OilPaintingArt OilPaintingArt)
        {
            var context = new OilPaintingArt2024DbContext();
            context.Entry(OilPaintingArt)
                .State = EntityState.Modified;

            return await context.SaveChangesAsync() > 0;
        }

        public static async Task<bool> DeleteAsync(OilPaintingArt OilPaintingArt)
        {
            var context = new OilPaintingArt2024DbContext();
            context.Remove(OilPaintingArt);

            return await context.SaveChangesAsync() > 0;
        }

    }
}
