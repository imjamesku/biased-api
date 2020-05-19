using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities{
    public class Comment {
        public int Id { get; set; }
        [Required]
        public User User {get; set;}
        // public Topic Topic {get; set;}
        public int TopicId {get; set;}
        [Required]
        public string Content{get; set;}

        public IList<Subcomment> Subcomments{get; set;} = new List<Subcomment>();

        //Todo: add CreatedAt, EditedAt, DeletedAt
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt{get; set;} = DateTime.UtcNow;
        public DateTime EditedAt{get; set;}
        public DateTime DeletedAt{get; set;}
    }
}