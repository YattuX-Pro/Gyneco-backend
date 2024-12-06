using Gyneco.Application.Models.Identity;
using Gyneco.Application.Models.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Gyneco.Domain;
using Gyneco.Domain.Contracts.Identity;
using Gyneco.Domain.Identity;

namespace Kada.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RoleManager<ApplicationUserRoles> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor contextAccessor,
            RoleManager<ApplicationUserRoles> roleManager)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _roleManager = roleManager;
        }

        public string UserId { get => _contextAccessor?.HttpContext?.User?.FindFirstValue("uid"); }

        public async Task<UserModel> GetUserAsync(Guid userId)
        {
            var utilisateur = await _userManager.FindByIdAsync(userId.ToString());
            return new UserModel
            {
                Email = utilisateur.Email,
                Id = utilisateur.Id,
                Firstname = utilisateur.FirstName,
                Lastname = utilisateur.LastName,
                Roles = await GetRoleInfosAsync(utilisateur),
                PhoneNumber = utilisateur.PhoneNumber,
                Username = utilisateur.UserName,
            };
        }

        public async Task<UserModelUpdate> UpdateUserAsync(UserModelUpdate model)
        {
            var user =  _userManager.FindByIdAsync(model.Id.ToString()).Result;
            if(user == null)
            {
                return null;
            }
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.Firstname;
            user.LastName = model.Lastname;
            user.UserName = model.Username;

            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var normalizedRolesUser = await _roleManager.Roles
                    .Where(role => userRoles.Contains(role.Name))
                    .Select(role => role.NormalizedName)
                    .ToListAsync();

                    var rolesToRemove = normalizedRolesUser.Except(model.Roles).ToList();

                    if (rolesToRemove.Any())
                    {
                        var resultat = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    }

                    var rolesToAdd = model.Roles.Except(normalizedRolesUser).ToList();
                    if (rolesToAdd.Any())
                    {
                        var roles = await _userManager.AddToRolesAsync(user, rolesToAdd);
                        if (roles.Succeeded)
                        {
                            return model;
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<SearchResult<UserModel>> GetUtilisateursListPageAsync(int pageIndex, int pageSize, Dictionary<string, string> filters)
        {
            var filteredRequest = GetFilteredQuery(filters);
            var utilisateurs = (pageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            
            List<UserModel> usersWithRoles = new List<UserModel>();

            foreach (var utilisateur in utilisateurs)
            {
                usersWithRoles.Add(new UserModel {
                    Id = utilisateur.Id,
                    Email = utilisateur.Email,
                    Firstname = utilisateur.FirstName,
                    Lastname = utilisateur.LastName,
                    PhoneNumber = utilisateur.PhoneNumber,
                    Username = utilisateur.UserName,
                    UserRoles = utilisateur.UserRoles.Select(x => 
                    new RoleInfo{ 
                        Name = x.Role.Name,
                        NormalizedName = x.Role.NormalizedName
                    }).ToList()
                });
            }

            return new SearchResult<UserModel>
            {
                Page = pageIndex,
                CountPerPage = pageSize,
                TotalCount = filteredRequest.Count(),
                Results = usersWithRoles
            };
        }

        public IQueryable<ApplicationUser> GetFilteredQuery(Dictionary<string, string> filter)
        {
            IQueryable<ApplicationUser> userQuery = _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role);

            foreach (var key in filter.Keys)
            {
                if (string.IsNullOrEmpty(filter[key]))
                {
                    continue;
                }

                switch (key)
                {
                    case "email":
                        userQuery = userQuery.Where(x => x.Email.Contains(filter[key]));
                        break;
                    case "firstname":
                        userQuery = userQuery.Where(x => x.FirstName.Contains(filter[key]));
                        break;
                    case "lastname":
                        userQuery = userQuery.Where(x => x.LastName.Contains(filter[key]));
                        break;
                    case "phoneNumber":
                        userQuery = userQuery.Where(x => x.PhoneNumber.Contains(filter[key]));
                        break;
                }
            }
            return userQuery;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user =await  _userManager.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
            if(user == null)
            {
                return false;
            }
            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            var role = await _roleManager.Roles.ToListAsync();
            return role.Select(r => new RoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                NormalizedName = r.NormalizedName,
                ConcurrencyStamp= r.ConcurrencyStamp,
            }).ToList();
        }

        public async Task<string> CreateRoleAsync(CreateRoleModel role)
        {
            var result = await _roleManager.CreateAsync(new ApplicationUserRoles()
            {
                Name = role.Name,
                NormalizedName = role.NormalizedName,
            });

            return result.ToString();
        }

        public async Task<string> DeleteRoleAsync(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var result = await _roleManager.DeleteAsync(role);
            return result.ToString();
        }

        public async Task<bool> UserExistAsync(Guid userId)
        {
            return await _userManager.Users.AnyAsync(u => u.Id == userId);
        }

        private async Task<List<RoleInfo>> GetRoleInfosAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var item in roles)
            {
                Console.Write(item);
            }

            var rolesInfo = await _roleManager.Roles
                .Where(role => roles.Contains(role.Name))
                .Select(role => new RoleInfo
                {
                    Name = role.Name,
                    NormalizedName = role.NormalizedName
                }).ToListAsync();
            return rolesInfo;
        }
    }
    
}
