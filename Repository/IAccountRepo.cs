using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepo
    {
        Task<SystemAccount> Login(string username, string password);
    }
}
