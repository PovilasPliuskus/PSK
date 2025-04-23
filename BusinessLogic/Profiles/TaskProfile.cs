using AutoMapper;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;
using Model = Contracts.Models;

namespace BusinessLogic.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<CreateTaskRequest, Model.Task>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.WorkspaceId, opt => opt.MapFrom(src => src.WorkspaceId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));

        CreateMap<Model.Task, GetTaskResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail))
            .ForMember(dest => dest.AssignedToUserEmail, opt => opt.MapFrom(src => src.AssignedToUserEmail))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));

        CreateMap<List<Model.Task>, GetWorkspaceTasksResponse>()
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src));

        CreateMap<UpdateTaskRequest, Model.Task>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.AssignedToUserEmail, opt => opt.MapFrom(src => src.AssignedToUserEmail))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Estimate, opt => opt.MapFrom(src => src.Estimate))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
    }
}
