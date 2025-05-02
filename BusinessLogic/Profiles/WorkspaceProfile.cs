using AutoMapper;
using Contracts.Models;
using Contracts.Pagination;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;

namespace BusinessLogic.Profiles;

public class WorkspaceProfile : Profile
{
    public WorkspaceProfile()
    {
        CreateMap<PaginatedResult<WorkspaceWithoutTasks>, GetWorkspacesResponse>()
            .ForMember(dest => dest.Workspaces, opt => opt.MapFrom(src => src));
        
        CreateMap<Workspace, GetWorkspaceResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));

        CreateMap<CreateWorkspaceRequest, WorkspaceWithoutTasks>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail));
        
        CreateMap<UpdateWorkspaceRequest, WorkspaceWithoutTasks>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail));
    }
}