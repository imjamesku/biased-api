namespace WebApi.Entities{
    // Join table
    public class Vote {
        public int UserId{get; set;}
        public User User {get; set;}
        public int OptionId{get; set;}
        public Option Option {get; set;}
    }
}