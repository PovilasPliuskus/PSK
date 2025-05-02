using BusinessLogic.Interfaces;
using Contracts.RequestBodies;
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
    public async Task<IActionResult> GetWorkspacePageAsync(int pageNumber, int pageSize)
    {
        GetWorkspacesResponse response = await _workspaceService.GetWorkspacePageAsync(pageNumber, pageSize);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkspaceAsync(Guid id)
    {
        GetWorkspaceResponse response = await _workspaceService.GetWorkspaceByIdAsync(id);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkspaceAsync([FromBody] CreateWorkspaceRequest request)
    {
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