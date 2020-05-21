namespace WebApi.Models.Topic
{
    public class TopicResourceModel
    {
        public int Id {get; set;}
        public string UserName {get; set;}
        public string Question { get; set; }
        public OptionResourceModel Left { get; set; }
        public OptionResourceModel Right { get; set; }
        public int CommentCount{get; set;}
        // Todo: Add voted
    }
}