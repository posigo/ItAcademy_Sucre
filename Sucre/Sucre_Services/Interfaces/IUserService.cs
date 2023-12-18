using Sucre_Core;
using Sucre_Core.DTOs;
using System.Security.Claims;

namespace Sucre_Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// User authentication by email
        /// </summary>
        /// <param name="email">email user</param>
        /// <param name="super">Super user</param>
        /// <returns></returns>
        public Task<ClaimsIdentity> Authenticate(string useName, bool super = false);
        /// <summary>
        /// Remove user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeleteUserIdAsync(Guid Id);
        /// <summary>
        /// Get a user with email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<AppUserTdo> GetAppUserAsync(string email);        
        /// <summary>
        /// Get a user with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppUserTdo> GetAppUserAsync(Guid id);
        /// <summary>
        /// Get a app user and assigned app role. Get app role not 
        /// assigned role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appUsersAssigned"></param>
        /// <param name="appUsersNotAssigned"></param>
        /// <returns></returns>
        AppUserTdo GetAppUserRoles(
            Guid id,
            ref IEnumerable<AppRoleTdo> appRolesAssigned,
            ref IEnumerable<AppRoleTdo> appRolesNotAssigned);
        /// <summary>
        /// Get list users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AppUserTdo>> GetListUsersAsync();
        /// <summary>
        /// Full user name as a string
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetStringAppUserAsync(Guid Id);
        /// <summary>
        /// Password verification
        /// </summary>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsPasswordCorrect(string password, string email = null);
        /// <summary>
        /// User verification
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsUserExist(string email);
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="userTdo"></param>
        /// <returns></returns>
        public Task<int> RegisterUserAsync(AppUserRegTdo userTdo);

        string getmd5hash(string sss);

        /// <summary>
        /// Removing/adding a role associated with a user
        /// </summary>
        /// <param name="idUser">id user</param>
        /// <param name="idRole">id role</param>
        /// <param name="action">action performed on the user</param>
        /// <returns></returns>
        Task<bool> ListAppUserRolesActionAsync(Guid idUser, Guid idRole, ActionRoleUser action = ActionRoleUser.None);
        /// <summary>
        /// Creating or updating user
        /// </summary>
        /// <param name="appUserTdo"></param>
        /// <returns></returns>
        Task<bool> UpsertUserAsync(AppUserTdo appUserTdo);

    }
}
