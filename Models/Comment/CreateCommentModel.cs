namespace WebApi.Models.Comment
{
    public class CreateCommentModel
    {
        public int TopicId{get; set;}
        public string Content { get; set; }
    }
}