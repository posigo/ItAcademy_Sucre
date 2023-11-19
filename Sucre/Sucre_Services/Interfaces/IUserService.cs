using Sucre_Core;
using System.Security.Claims;

namespace Sucre_Services.Interfaces
{
    public interface IUserService
    {
        public Task<ClaimsIdentity> Authenticate(string useName);
        Task<bool> IsPasswordCorrect(string email, string password, bool findEmail = false);
        Task<bool> IsUserExist(string email);
        public Task<int> RegisterUser(AppUserRegTdo userTdo);

        string getmd5hash(string sss);


    }
}
