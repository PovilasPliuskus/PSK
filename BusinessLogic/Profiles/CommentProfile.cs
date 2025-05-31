using AutoMapper;
using Contracts.Models;
using Contracts.RequestBodies;
using Contracts.ResponseBodies;

namespace BusinessLogic.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, GetTaskCommentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Edited, opt => opt.MapFrom(src => src.Edited))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail));
        
        CreateMap<List<Comment>, GetTaskCommentsResponse>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src));

        CreateMap<AddTaskCommentRequest, Comment>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.CommentText))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail));
    }
}