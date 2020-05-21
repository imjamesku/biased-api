using System;
using WebApi.Models.Users;
namespace WebApi.Models.Comment
{
    public class CommentResourceModel
    {
        public int Id {get; set;}
        public UserModel User {get; set;}
        public string Content {get; set;}
        public int SubcommentCount{get; set;}
        public DateTime CreatedAt{get; set;}
        public DateTime EditedAt{get; set;}
    }
}