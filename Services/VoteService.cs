using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topic;

namespace WebApi.Services
{
    public interface IVoteService
    {
        IEnumerable<Topic> GetAll();
        Topic GetById(int id);
        Vote Create(int optionId, int userId);
        // void Update(Topic topic);
        // void Delete(Topic topic);
    }

    public class VoteService : IVoteService
    {
        private DataContext _context;

        public VoteService(DataContext context)
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
        //Vote or unvote
        public Vote Create(int optionId, int userId)
        {
            Vote vote = _context.Votes.Where(v => v.OptionId == optionId && v.UserId == userId).FirstOrDefault();
            if (vote != null) {
                // already voted
                // Do unvote
                _context.Votes.Remove(vote);
                _context.SaveChanges();
                return null;
            } else {
                // Not yet voted
                // Do vote
                Vote newVote = new Vote {
                    UserId = userId,
                    OptionId = optionId
                };
                _context.Votes.Add(newVote);
                _context.SaveChanges();
                return newVote;
            }
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