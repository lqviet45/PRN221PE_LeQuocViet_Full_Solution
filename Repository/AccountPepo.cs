using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountPepo : IAccountRepo
    {
        public async Task<SystemAccount> Login(string username, string password)
        {
            var account = await AccountDao.GetAccountByUsernameAndPassword(username, password);
            if (account == null)
            {
                throw new Exception("You do not have permission to do this function!");
            }

            if (account.Role == 2 || account.Role == 3)
            {
                return account;
            }

            throw new Exception("You do not have permission to do this function!");
        }
    }
}
