using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Topic
{
    public class CreateTopicModel
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Left { get; set; }
        [Required]
        public string Right { get; set; }
        [Required]
        public string Token {get; set;}
    }
}