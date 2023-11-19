using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Sucre_Core;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository;
using Sucre_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ClaimsIdentity> Authenticate(string email)
        {
            //var gg = DelListUser.ListUser.SingleOrDefault(i => i.Email == userName);
            //var ee = gg.PasswordHash == GenerateMD5Hash(passwordHash)?true:false;
            //var jj = IsUserExist(userName);

            var claims = new List<Claim>()
            { new Claim(ClaimsIdentity.DefaultNameClaimType, email)};
            var claimsIdentity = new ClaimsIdentity(claims, 
                "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private string GenerateMD5Hash(string str)
        {
            var salt = _configuration["AppSettings:PasswordSalt"];
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{str}{salt}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var res = Convert.ToHexString(hashBytes);

                return Convert.ToHexString(hashBytes);
            }

            //return ""; 
        }

        public async Task<bool> IsPasswordCorrect(string email, string password, bool findEmail=false)
        {            
            if (findEmail &&
                !DelListUser.ListUser.Any(item => item.Email == email))
                return false;
            var passwordHash = GenerateMD5Hash(password);
            if (DelListUser.ListUser.FirstOrDefault(item => item.Email == email).PasswordHash == passwordHash)
                return true;
            return false;
        }

        public async Task<bool> IsUserExist(string email)
        {
            //var hh=DelListUser.ListUser.Find(item => item.Email == email).Email.Any();
            var gg = DelListUser.ListUser.Any(item => item.Email == email);

            if (DelListUser.ListUser.Count==0) return false;

            if (gg)//(DelListUser.ListUser.FirstOrDefault(item => item.Email == email).Email.Any())
            {
                return true;
            }
            return false;
        }

        public async Task<int> RegisterUser(AppUserRegTdo userTdo)
        {
            int id = DelListUser.ListUser.Max(i => i.Id);

            int grm = userTdo.GroupId;

            var userReg = new AppUserTdo()
            {
                Id = id + 1,
                Email = userTdo.Email,
                PasswordHash = GenerateMD5Hash(userTdo.Password),
                GroupId = userTdo.GroupId,
            };

            AppUser userAdd = new AppUser
            {
                Id=userReg.Id,
                Email = userReg.Email,
                PasswordHash = userReg.PasswordHash,
                GroupId = userReg.GroupId,
                Name = userReg.Name,
                Description = userReg.Description
            };

            DelListUser.AddInListUser(userAdd);
            return 0;
            
        }

       
        public string getmd5hash(string sss)
        {
            return GenerateMD5Hash(sss);
        }


    }
}
