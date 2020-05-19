using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Vote
{
    public class CreateVoteModel
    {
        [Required]
        public int OptionId { get; set; }
    }
}