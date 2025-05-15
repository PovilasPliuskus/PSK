using AutoMapper;
using Contracts.Models;
using Contracts.Pagination;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using DataAccess.Entities;

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
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version));

        CreateMap<CreateWorkspaceRequest, WorkspaceWithoutTasks>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail));

        CreateMap<UpdateWorkspaceRequest, WorkspaceWithoutTasks>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.Force, opt => opt.MapFrom(src => src.Force));

        // Entity -> Model
        CreateMap<WorkspaceUsersEntity, WorkspaceUser>()
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.FkUserEmail))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FkWorkspaceId))
            .ForMember(dest => dest.IsOwner, opt => opt.MapFrom(src => src.IsOwner));

        // Paginated wrapper -> response
        CreateMap<PaginatedResult<WorkspaceUser>, GetUsersInWorkspaceResponse>()
            .ForMember(dest => dest.WorkspacesUsers, opt => opt.MapFrom(src => src));

    }
}