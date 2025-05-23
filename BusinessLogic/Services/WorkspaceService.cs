using AutoMapper;
using BusinessLogic.Interfaces;
using Contracts.Models;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using DataAccess.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace BusinessLogic.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IMapper _mapper;

    public WorkspaceService(IWorkspaceRepository workspaceRepository, IMapper mapper)
    {
        _workspaceRepository = workspaceRepository;
        _mapper = mapper;
    }

    public async Task<GetWorkspacesResponse> GetWorkspacePageAsync(int pageNumber, int pageSize, string userEmail)
    {
        var workspaces = await _workspaceRepository.GetRangeAsync(pageNumber, pageSize, userEmail);

        return _mapper.Map<GetWorkspacesResponse>(workspaces);
    }

    public async Task<GetWorkspaceResponse> GetWorkspaceByIdAsync(Guid workspaceId)
    {
        var workspace = await _workspaceRepository.GetSingleAsync(workspaceId);

        return _mapper.Map<GetWorkspaceResponse>(workspace);
    }

    public async Task<GetUsersInWorkspaceResponse> GetUsersInWorkspaceAsync(int pageNumber, int pageSize, Guid workspaceId, string userEmail)
    {
        var users = await _workspaceRepository.GetUsersInWorkspaceAsync(pageNumber, pageSize, workspaceId, userEmail);

        return _mapper.Map<GetUsersInWorkspaceResponse>(users);
    }

    public async Task AddUserToWorkspaceAsync(Guid workspaceId, AddUserRequest request)
    {
        await _workspaceRepository.AddUserToWorkspaceAsync(workspaceId, request);
    }

    public async Task RemoveUserFromWorkspaceAsync(Guid workspaceId, RemoveUserRequest request)
    {
        await _workspaceRepository.RemoveUserFromWorkspaceAsync(workspaceId, request);
    }

    public async Task CreateWorkspaceAsync(CreateWorkspaceRequest request)
    {
        var workspace = _mapper.Map<WorkspaceWithoutTasks>(request);

        await _workspaceRepository.AddAsync(workspace);
    }

    public async Task UpdateWorkspaceAsync(UpdateWorkspaceRequest request)
    {
        var workspace = _mapper.Map<WorkspaceWithoutTasks>(request);

        await _workspaceRepository.UpdateAsync(workspace);
    }

    public async Task DeleteWorkspaceAsync(Guid id)
    {
        await _workspaceRepository.RemoveAsync(id);
    }
}