using Sucre_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository
{
    public static class DelListUser
    {
        private static List<AppUser> _listUser=new List<AppUser>();

        static DelListUser()
        {

            List<AppUser> lst = new List<AppUser>()
            {
                new AppUser()
                {
                    Id = 1,
                    Name = "admin",
                    Description = "User ASUTP",
                    Email = "admin@admin.him",
                    PasswordHash ="27A34DCEA5D00E4EE9C8301CC2D84383",
                    
                    GroupId = 1
                },
                new AppUser()
                {
                    Id = 1,
                    Email = "user@user.him",
                    PasswordHash = "43B3788992A26B46532CC11C0563C46B"
                }
            };

            _listUser.AddRange(lst);

            //_listUser.Add(new AppUser
            //{
            //    Email="qwe@qwe.qwe"
            //});
        }

        public static List<AppUser> ListUser 
        { 
            get {  return _listUser; } 
        }

        public static void AddInListUser(AppUser user)
        {
            _listUser.Add(user);
        }
    }
}
