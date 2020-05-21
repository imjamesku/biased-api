using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Comment;
using WebApi.Models.Topic;
using WebApi.Models.Users;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<Topic, TopicResourceModel>()
                .ForMember(tr => tr.UserName, b => b.MapFrom(t => t.User.Username))
                .ForMember(tr => tr.Left, opts => opts.MapFrom(src => new OptionResourceModel
                {
                    Id = src.Options[0].Id,
                    Name = src.Options[0].Name,
                    count = src.Options[0].Votes.Count
                }))
                .ForMember(tr => tr.Right, opts => opts.MapFrom(src => new OptionResourceModel
                {
                    Id = src.Options[1].Id,
                    Name = src.Options[1].Name,
                    count = src.Options[1].Votes.Count
                }));
            CreateMap<Comment, CommentResourceModel>();
                // .ForMember(cm => cm.User, opts => opts.MapFrom(src => new UserModel {
                //     Id = src.User.Id,
                //     FirstName = src.User.FirstName,
                //     LastName = src.User.LastName,
                //     Username = src.User.Username
                // }));
        }
    }
}