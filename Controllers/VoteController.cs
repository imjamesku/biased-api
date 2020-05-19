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
using WebApi.Models.Vote;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class Vote : ControllerBase
    {
        private IVoteService _voteService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public Vote(
            IVoteService voteService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _voteService = voteService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateVoteModel model)
        {
            try
            {
                var topic = _voteService.Create(model.OptionId, int.Parse(User.Identity.Name));
                if (topic != null) {
                    return Ok(new {message = "Voted"});
                } else {
                    return Ok(new {message = "Unvoted"});
                }
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        // [HttpGet]
        // public IActionResult GetAll()
        // {
        //     var topics = _topicService.GetAll();
        //     var topicResourceList = _mapper.Map<IList<TopicResourceModel>>(topics);
        //     return Ok(topicResourceList);
        // }

        // [HttpGet("{id}")]
        // public IActionResult GetById(int id)
        // {
        //     var user = _voteService.GetById(id);
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
