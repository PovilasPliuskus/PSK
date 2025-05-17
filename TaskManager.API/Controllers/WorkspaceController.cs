using BusinessLogic.Interfaces;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using DataAccess.Extensions;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Exceptions;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkspaceController : ControllerBase
{

    private readonly IWorkspaceService _workspaceService;

    public WorkspaceController(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkspacePageAsync(int pageNumber, int pageSize)
    {
        var userEmail = User.GetUserEmail();
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new NotAuthenticatedException("User is not authenticated");
        }

        GetWorkspacesResponse response = await _workspaceService.GetWorkspacePageAsync(pageNumber, pageSize, userEmail);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkspaceAsync(Guid id)
    {
        GetWorkspaceResponse response = await _workspaceService.GetWorkspaceByIdAsync(id);

        return Ok(response);
    }

    [HttpGet("users/{workspaceId}")]
    public async Task<IActionResult> GetUsersInWorkspaceAsync(int pageNumber, int pageSize, Guid workspaceId)
    {
        var userEmail = User.GetUserEmail();
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new NotAuthenticatedException("User is not authenticated");
        }

        var response = await _workspaceService.GetUsersInWorkspaceAsync(pageNumber, pageSize, workspaceId, userEmail);

        return Ok(response);
    }

    [HttpPost("users/{workspaceId}")]
    public async Task<IActionResult> AddUserToWorkspaceAsync(Guid workspaceId, [FromBody] AddUserRequest request)
    {
        var currentUserEmail = User.GetUserEmail();
        if (string.IsNullOrEmpty(currentUserEmail))
        {
            throw new NotAuthenticatedException("User is not authenticated");
        }

        await _workspaceService.AddUserToWorkspaceAsync(workspaceId, request);

        return Ok();
    }

    [HttpDelete("users/{workspaceId}")]
    public async Task<IActionResult> RemoveUserFromWorkspaceAsync(Guid workspaceId, [FromBody] RemoveUserRequest request)
    {
        var currentUserEmail = User.GetUserEmail();
        if (string.IsNullOrEmpty(currentUserEmail))
        {
            throw new NotAuthenticatedException("User is not authenticated");
        }

        await _workspaceService.RemoveUserFromWorkspaceAsync(workspaceId, request);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkspaceAsync([FromBody] CreateWorkspaceRequest request)
    {
        var userEmail = User.GetUserEmail();

        if (string.IsNullOrEmpty(userEmail))
        {
            throw new NotAuthenticatedException("User is not authenticated");
        }
        request.CreatedByUserEmail = userEmail;
        await _workspaceService.CreateWorkspaceAsync(request);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWorkspaceAsync([FromBody] UpdateWorkspaceRequest request)
    {
        await _workspaceService.UpdateWorkspaceAsync(request);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkspaceAsync(Guid id)
    {
        await _workspaceService.DeleteWorkspaceAsync(id);

        return Ok();
    }
}