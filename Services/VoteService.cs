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

        //Vote or unvote
        public Vote Create(int optionId, int userId)
        {
            Option option = _context.Options.Find(optionId);
            var bothSides = _context.Options.Where(o => o.TopicId == option.TopicId).ToList();
            var votesByUser = _context.Votes.Where(v => v.UserId == userId && (v.OptionId == bothSides[0].Id || v.OptionId == bothSides[1].Id)).ToList();

            if (votesByUser.Count != 0)
            {
                // already voted
                // Do unvote
                // _context.Votes.Remove(vote);
                // _context.SaveChanges();
                return null;
            }
            else
            {
                // Not yet voted
                // Do vote
                Vote newVote = new Vote
                {
                    UserId = userId,
                    OptionId = optionId
                };
                _context.Votes.Add(newVote);
                _context.SaveChanges();
                return newVote;
            }
        }

    }
}