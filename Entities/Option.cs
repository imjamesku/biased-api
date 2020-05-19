using System.ComponentModel.DataAnnotations.Schema;

using System.Collections.Generic;

namespace WebApi.Entities{
    public class Option {
        public int Id { get; set; }
        // public Topic Topic {get; set;}
        public string Name {get; set;}
        
        public IList<Vote> Votes {get; set;}
        public EOptionTypes Type {get; set;}
    }
}