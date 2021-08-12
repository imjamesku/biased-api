using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models.Topic;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using WebApi.Models.Recaptcha;
using System.Text.Json;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TopicsController : ControllerBase
    {
        private ITopicService _topicService;
        private ISecretService _secretService;

        // private readonly UserManager<IdentityUser> _userManager;
        private IMapper _mapper;

        public TopicsController(
            ITopicService topicService,
            // UserManager<IdentityUser> userManager,
            ISecretService secretService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _topicService = topicService;
            _secretService = secretService;
            // _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTopicModel model)
        {
            try
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["secret"] = _secretService.GetSecret("recaptcha-secret");
                    data["response"] = model.Token;

                    var response = wb.UploadValues(_secretService.GetSecret("recaptcha-verify-url"), "POST", data);
                    var responseObj = JsonSerializer.Deserialize<RecaptchaResponseModel>(response);
                    if (responseObj.success)
                    {
                        var topic = _topicService.Create(model, int.Parse(User.Identity.Name));
                        var topicResource = _mapper.Map<TopicResourceModel>(topic);
                        return Ok(topicResource);
                    }
                    return BadRequest(new { message = "Recaptcha verification failed" });
                }
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll([FromQuery] int offset)
        {
            var topics = _topicService.GetAll(offset);
            var topicResourceList = _mapper.Map<IList<TopicResourceModel>>(topics);
            return Ok(new
            {
                topicResourceList = topicResourceList,
                nextOffset = offset + topicResourceList.Count
            });
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var topic = _topicService.GetById(id);
            var topicResource = _mapper.Map<TopicResourceModel>(topic);
            // return Ok(topic);
            return Ok(topicResource);
        }

        // [HttpPut("{id}")]
        // public IActionResult Update(int id, [FromBody]UpdateModel model)
        // {
        //     // map model to entity and set id
        //     var user = _mapper.Map<User>(model);
        //     user.Id = id;

        //     try
        //     {
        //         // update user 
        //         _userService.Update(user, model.Password);
        //         return Ok();
        //     }
        //     catch (AppException ex)
        //     {
        //         // return error message if there was an exception
        //         return BadRequest(new { message = ex.Message });
        //     }
        // }

        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     _userService.Delete(id);
        //     return Ok();
        // }
    }
}
