using Gyneco.Application.DTOs.Search;
using Gyneco.Identity.Models.Search;
using Gyneco.Persistence.Contracts.Identity;
using Gyneco.Persistence.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kada.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: api/<ClientController>
        [HttpPost]
        public async Task<SearchResult<UserModel>> GetUserListPage([FromBody] SearchDTO search_)
        {
            try
            {
                return await _userService.GetUtilisateursListPageAsync(search_.PageIndex, search_.PageSize, search_.Filters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<UserModel> GetUser(string id)
        {
            try
            {
                return await _userService.GetUtilisateur(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<UserModelUpdate> UpdateUser(UserModelUpdate userModel)
        {
            if (userModel == null) BadRequest("L'utilisateur ne peut etre null");
            try
            {
                return await _userService.UpdateUser(userModel);
            }
            catch (Exception)
            {

                throw;
            } 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var isDeleted = await _userService.DeleteUserAsync(id);
                if (!isDeleted) return BadRequest(false);
                return Ok(true);
            }
            catch (Exception)
            {

                throw;
            } 
        }

        [HttpGet]
        public async Task<List<RoleModel>> GetRoleListPage()
        {
            try
            {
                return await _userService.GetRoles();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<string> CreateRole(CreateRoleModel role)
        {
            try
            {
                return await _userService.CreateRole(role);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public async Task<string> DeleteRole(string roleId)
        {
            try
            {
                return await _userService.DeleteRole(roleId);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
