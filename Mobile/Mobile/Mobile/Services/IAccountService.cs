using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public interface IAccountService
    {
        Task<bool> Login(string email, string password);
        Task Logout();
    }
}
