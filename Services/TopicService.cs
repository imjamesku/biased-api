using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topic;

namespace WebApi.Services
{
    public interface ITopicService
    {
        IEnumerable<Topic> GetAll();
        Topic GetById(int id);
        Topic Create(CreateTopicModel topicData, int userId);
        // void Update(Topic topic);
        // void Delete(Topic topic);
    }

    public class TopicService : ITopicService
    {
        private DataContext _context;

        public TopicService(DataContext context)
        {
            _context = context;
        }


        public IEnumerable<Topic> GetAll()
        {
            var topics = _context.Topics.Include(t => t.Options);
            return topics;
        }

        public Topic GetById(int id)
        {
            return _context.Topics.Find(id);
        }
        //Todo: Create Data
        public Topic Create(CreateTopicModel topicData, int userId)
        {
            User user = _context.Users.Find(userId);
            Option left = new Option {
                Name = topicData.Left,
                Type = EOptionTypes.Left
            };
            Option right = new Option {
                Name = topicData.Right,
                Type = EOptionTypes.Right
            };
            _context.Options.Add(left);
            _context.Options.Add(right);

            Topic newTopic = new Topic {
                User = user,
                Question = topicData.Question,
                Options = new List<Option> {left, right}
            };
            _context.Topics.Add(newTopic);

            _context.SaveChanges();
            return newTopic;
        }
        // TODO: delete all relevant data
        public void Delete(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                _context.SaveChanges();
            }
        }

    }
}