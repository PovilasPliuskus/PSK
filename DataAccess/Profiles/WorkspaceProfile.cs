using AutoMapper;
using Contracts.Models;
using DataAccess.Entities;

namespace DataAccess.Profiles;

public class WorkspaceProfile : Profile
{
    public WorkspaceProfile()
    {
        CreateMap<WorkspaceEntity, WorkspaceWithoutTasks>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.FkCreatedByUserEmail));
        
        CreateMap<WorkspaceWithoutTasks, WorkspaceEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FkCreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail));
        
        CreateMap<WorkspaceEntity, Workspace>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.FkCreatedByUserEmail))
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));
    }
}