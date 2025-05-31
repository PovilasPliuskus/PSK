using AutoMapper;
using Contracts.Models;
using DataAccess.Entities;

namespace DataAccess.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Edited, opt => opt.MapFrom(src => src.Edited))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.FkTaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.FkWrittenByUserEmail, opt => opt.MapFrom(src => src.CreatedByUserEmail));

        CreateMap<CommentEntity, Comment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Edited, opt => opt.MapFrom(src => src.Edited))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.FkTaskId))
            .ForMember(dest => dest.CreatedByUserEmail, opt => opt.MapFrom(src => src.FkWrittenByUserEmail));
    }
}