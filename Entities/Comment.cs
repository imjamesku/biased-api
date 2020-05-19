using System.Collections.Generic;
using System;
namespace WebApi.Entities{
    public class Comment {
        public int Id { get; set; }
        public User User {get; set;}
        // public Topic Topic {get; set;}
        public string Content{get; set;}

        public IList<Subcomment> Subcomments{get; set;} = new List<Subcomment>();

        //Toto: add CreatedAt, EditedAt, DeletedAt
    }
}