using AutoMapper;
using BusinessLogic.Interfaces;
using Contracts.ResponseBodies;
using DataAccess.Repositories.Interfaces;

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
    
    public async Task<GetWorkspacesResponse> GetAllWorkspacesAsync()
    {
        var workspaces = await _workspaceRepository.GetAllAsync();
        
        return _mapper.Map<GetWorkspacesResponse>(workspaces);
    }

    public async Task<GetWorkspaceResponse> GetWorkspaceByIdAsync(Guid workspaceId)
    {
        var workspace = await _workspaceRepository.GetSingleAsync(workspaceId);
        
        return _mapper.Map<GetWorkspaceResponse>(workspace);
    }

    /*public Task CreateWorkspaceAsync(CreateWorkspaceRequest request)
    {
        throw new NotImplementedException();
    }

    public Task UpdateWorkspaceAsync(UpdateWorkspaceRequest request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWorkspaceAsync(Guid id)
    {
        throw new NotImplementedException();
    }*/
}