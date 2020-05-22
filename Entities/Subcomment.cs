using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities{
    public class Subcomment {
        public int Id { get; set; }
        public User User {get; set;}
        // public Comment Comment {get; set;}
        public int CommentId{get; set;}
        [Required]
        public string Content{get; set;}
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt{get; set;} = DateTime.UtcNow;
        public DateTime EditedAt{get; set;}
        public DateTime DeletedAt{get; set;}
    }
}