using Gyneco.Application.Models.Identity;
using Gyneco.Application.Models.Search;

namespace Gyneco.Domain.Contracts.Identity
{
    public interface IUserService
    {
        Task<SearchResult<UserModel>> GetUtilisateursListPageAsync(int pageIndex, int pageSize, Dictionary<string, string> filters);
        Task<UserModel> GetUserAsync(Guid userId);
        Task<bool> UserExistAsync(Guid userId);
        Task<List<RoleModel>> GetRolesAsync();
        Task<string> CreateRoleAsync(CreateRoleModel role);
        Task<string> DeleteRoleAsync(Guid roleId);
        Task<UserModelUpdate> UpdateUserAsync(UserModelUpdate model);
        Task<bool> DeleteUserAsync(Guid id);
        public string UserId { get; }
    }
}
