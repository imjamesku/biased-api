using System;
namespace WebApi.Models.Recaptcha
{
    public class RecaptchaResponseModel
    {
        public bool success {get; set;}
        public DateTime challenge_ts {get; set;}
        public string hostname {get; set;}
        public float score {get; set;}
        public string action {get; set;}
    }
}