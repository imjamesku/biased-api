using AutoMapper;
using WebApi.Entities;
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
                .ForMember(tr => tr.Left, b => b.MapFrom(t => t.Options[0].Name))
                .ForMember(tr => tr.Right, b => b.MapFrom(t => t.Options[1].Name));
        }
    }
}