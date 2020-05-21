using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using WebApi.Models.Topic;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TopicsController : ControllerBase
    {
        private ITopicService _topicService;

        // private readonly UserManager<IdentityUser> _userManager;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TopicsController(
            ITopicService topicService,
            // UserManager<IdentityUser> userManager,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _topicService = topicService;
            // _userManager = userManager;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateTopicModel model)
        {
            try
            {
                var topic = _topicService.Create(model, int.Parse(User.Identity.Name));
                var topicResource = _mapper.Map<TopicResourceModel>(topic);
                return Ok(topicResource);
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var topics = _topicService.GetAll();
            var topicResourceList = _mapper.Map<IList<TopicResourceModel>>(topics);
            return Ok(topicResourceList);
        }

        // [HttpGet("{id}")]
        // public IActionResult GetById(int id)
        // {
        //     var user = _topicService.GetById(id);
        //     var model = _mapper.Map<UserModel>(user);
        //     return Ok(model);
        // }

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
