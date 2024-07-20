using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SupplierRepo : ISupplierRepo
    {
        public async Task<List<SupplierCompany>> GetSupplierCompanies()
        {
            return await SupplierDao.GetAll();
        }
    }
}
