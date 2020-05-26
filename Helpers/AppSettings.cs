namespace WebApi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string GoogleRecaptchaSecret {get; set;}
        public string GoogleRecaptchaVerifyUrl{get; set;}
    }
}