using System;
namespace WebApi.Entities{
    public class Subcomment {
        public int Id { get; set; }
        public User User {get; set;}
        // public Comment Comment {get; set;}
        public string Content{get; set;}

        //Toto: add CreatedAt, EditedAt, DeletedAt
    }
}