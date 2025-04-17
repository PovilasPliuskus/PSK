using AutoMapper;
using DataAccess.Entities;
using Model = Contracts.Models;

namespace DataAccess.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Model.Task, TaskEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FkCreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.FkWorkspaceId, opt => opt.MapFrom(src => src.WorkspaceId))
            .ForMember(dest => dest.FkAssignedToUserEmail, opt => opt.MapFrom(src => src.AssignedToUserEmail))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));

        CreateMap<TaskEntity, Model.Task>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.FkCreatedByUserEmail))
            .ForMember(dest => dest.WorkspaceId, opt => opt.MapFrom(src => src.FkWorkspaceId))
            .ForMember(dest => dest.AssignedToUserEmail, opt => opt.MapFrom(src => src.FkAssignedToUserEmail))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
    }
}
