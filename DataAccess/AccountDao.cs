using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class AccountDao
    {
        public static async Task<SystemAccount?> GetAccountByUsernameAndPassword(string username, string password)
        {
            var context = new OilPaintingArt2024DbContext();

            return await context.SystemAccounts
                .FirstOrDefaultAsync(a => a.AccountEmail == username && a.AccountPassword == password);
        }
    }
}
