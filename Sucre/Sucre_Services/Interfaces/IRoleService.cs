using Sucre_Core;
using Sucre_Core.DTOs;

namespace Sucre_Services.Interfaces
{
    public interface IRoleService
    {        

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="appRoleTdo"></param>
        /// <returns></returns>
        Task<bool> DeleteRoleAsync(AppRoleTdo appRoleTdo);            
        //Task<bool> UpdateRole(AppRoleTdo appRoleTdo);
        /// <summary>
        /// Get a role by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppRoleTdo> GetAppRoleAsync(Guid id);
        /// <summary>
        /// Get list of roles
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AppRoleTdo>> GetListRolesAsync();
        /// <summary>
        /// Get a app role and assigned app user. Get app user not 
        /// assigned role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appUsersAssigned"></param>
        /// <param name="appUsersNotAssigned"></param>
        /// <returns></returns>
        AppRoleTdo GetAppRoleUsers(Guid id,
            ref IEnumerable<AppUserTdo> appUsersAssigned,
            ref IEnumerable<AppUserTdo> appUsersNotAssigned);
        /// <summary>
        /// Removing/adding a user associated with a role
        /// </summary>
        /// <param name="idRole">id role</param>
        /// <param name="idUser">id user</param>
        /// <param name="action">action performed on the role</param>
        /// <returns></returns>
        Task<bool> ListAppRoleUsersActionAsync(Guid idRole, Guid idUser, ActionRoleUser action);
        /// <summary>
        /// Create or update a role
        /// </summary>
        /// <param name="appRoleTdo"></param>
        /// <returns></returns>
        Task<bool> UpsertRoleAsync(AppRoleTdo appRoleTdo);
    }
}
