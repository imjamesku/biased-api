using System;
using System.Collections.Generic;

namespace WebApi.Entities{
    public class Topic {
        public int Id { get; set; }
        public IList<Option> Options {get; set;} = new List<Option>();
        public String Question{get; set;}

        public IList<Comment> Comments {get; set;} = new List<Comment>();
    }
}