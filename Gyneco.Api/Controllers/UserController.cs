﻿using Gyneco.Application.DTOs.Search;
using Gyneco.Application.Models.Identity;
using Gyneco.Application.Models.Search;
using Gyneco.Domain.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kada.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/<ClientController>
    [HttpPost]
    public async Task<SearchResult<UserModel>> GetUserListPage([FromBody] SearchDTO search_)
    {
        return await _userService.GetUtilisateursListPageAsync(search_.PageIndex, search_.PageSize, search_.Filters);
    }

    [HttpGet]
    public async Task<UserModel> GetUser(string id)
    {
        return await _userService.GetUtilisateur(id);
    }

    [HttpPut]
    public async Task<UserModelUpdate> UpdateUser(UserModelUpdate userModel)
    {
        if (userModel == null) BadRequest("L'utilisateur ne peut etre null");
        return await _userService.UpdateUser(userModel);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var isDeleted = await _userService.DeleteUserAsync(id);
        if (!isDeleted) return BadRequest(false);
        return Ok(true);
    }

    [HttpGet]
    public async Task<List<RoleModel>> GetRoleListPage()
    {
        return await _userService.GetRoles();
    }

    [HttpPost]
    public async Task<string> CreateRole(CreateRoleModel role)
    {
        return await _userService.CreateRole(role);
    }

    [HttpDelete]
    public async Task<string> DeleteRole(string roleId)
    {
        return await _userService.DeleteRole(roleId);
    }
}