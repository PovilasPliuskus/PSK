using AutoMapper;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using Model = Contracts.Models;

namespace BusinessLogic.Profiles;

public class SubTaskProfile : Profile
{
    public SubTaskProfile()
    {
        CreateMap<CreateSubTaskRequest, Model.SubTask>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.WorkspaceId, opt => opt.MapFrom(src => src.WorkspaceId))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId));

        CreateMap<Model.SubTask, GetSubTaskResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.AssignedToUserEmail, opt => opt.MapFrom(src => src.AssignedToUserEmail))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));

        CreateMap<List<Model.SubTask>, GetTaskSubTasksResponse>()
            .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src));

        CreateMap<UpdateSubTaskRequest, Model.SubTask>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.AssignedToUserEmail, opt => opt.MapFrom(src => src.AssignedToUserEmail))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.Force, opt => opt.MapFrom(src => src.Force))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
    }
}
