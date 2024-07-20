using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class SupplierDao
    {
        public static async Task<List<SupplierCompany>> GetAll()
        {
            var context = new OilPaintingArt2024DbContext();

            return await context.SupplierCompanies.ToListAsync();
        }
    }
}
