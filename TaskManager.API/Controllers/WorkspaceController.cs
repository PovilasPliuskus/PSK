using BusinessLogic.Interfaces;
using Contracts.ResponseBodies;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetWorkspacesAsync()
    {
        GetWorkspacesResponse response = await _workspaceService.GetAllWorkspacesAsync();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkspaceAsync(Guid id)
    {
        GetWorkspaceResponse response = await _workspaceService.GetWorkspaceByIdAsync(id);
        
        return Ok(response);
    }
}