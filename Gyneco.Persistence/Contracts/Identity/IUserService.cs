﻿

using Gyneco.Identity.Models.Search;
using Gyneco.Persistence.Models.Identity;


namespace Gyneco.Persistence.Contracts.Identity
{
    public interface IUserService
    {
        Task<SearchResult<UserModel>> GetUtilisateursListPageAsync(int pageIndex, int pageSize, Dictionary<string, string> filters);
        Task<UserModel> GetUtilisateur(string userId);
        Task<List<RoleModel>> GetRoles();
        Task<string> CreateRole(CreateRoleModel role);
        Task<string> DeleteRole(string roleId);
        Task<UserModelUpdate> UpdateUser(UserModelUpdate model);
        Task<bool> DeleteUserAsync(Guid id);
        public string UserId { get; }
    }
}
