using AutoMapper;
using DataAccess.Entities;
using Model = Contracts.Models;

namespace DataAccess.Profiles;

public class SubTaskProfile : Profile
{
    public SubTaskProfile()
    {
        CreateMap<Model.SubTask, SubTaskEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FkCreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.FkTaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.FkAssignedToUserEmail, opt => opt.MapFrom(src => src.AssignedToUserEmail))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));

        CreateMap<SubTaskEntity, Model.SubTask>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.FkCreatedByUserEmail))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.FkTaskId))
            .ForMember(dest => dest.AssignedToUserEmail, opt => opt.MapFrom(src => src.FkAssignedToUserEmail))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
    }
}
