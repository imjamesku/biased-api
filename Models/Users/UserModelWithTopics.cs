using System.Collections.Generic;
using WebApi.Models.Topic;

namespace WebApi.Models.Users
{
  public class UserModelWithTopics
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public IList<TopicEssentialModel> Topics {get; set;}
    }
}