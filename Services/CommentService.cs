using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Comment;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface ICommentService
    {
        IList<Comment> GetCommentsByTopicId(int topicId);
        Comment Create(CreateCommentModel formData, int userId);
        Comment Edit(EditCommentModel formData, int userId);
        Comment Delete(int commentId, int userId);
        // Topic CreateSubcomment(CreateCommentModel formData);
        // Topic EditSubcomment(EditCommentModel formData);

    }

    public class CommentService : ICommentService
    {
        private DataContext _context;

        public CommentService(DataContext context)
        {
            _context = context;
        }


        public IList<Comment> GetCommentsByTopicId(int topicId)
        {
            var comments = _context.Comments.Include(c => c.User).Where(c => c.TopicId == topicId && c.DeletedAt == DateTime.MinValue).OrderByDescending(c => c.CreatedAt).ToList();
            return comments;
        }

        public Comment Create(CreateCommentModel formData, int userId)
        {
            Comment newComment = new Comment
            {
                UserId = userId,
                TopicId = formData.TopicId,
                Content = formData.Content
            };
            _context.Comments.Add(newComment);
            Topic topic = _context.Topics.Find(formData.TopicId);
            topic.CommentCount = topic.CommentCount + 1;
            _context.SaveChanges();
            return newComment;
        }

        public Comment Edit(EditCommentModel model, int userId)
        {
            Comment comment = _context.Comments.Find(model.Id);
            if (comment != null)
            {
                if (comment.UserId == userId)
                {
                    comment.Content = model.Content;
                    comment.EditedAt = DateTime.UtcNow;
                    _context.SaveChanges();
                    return comment;
                }
                else
                {
                    throw new AppException("You don't have permission to edit this comment");
                }
            }
            else
            {
                throw new AppException("Comment not found");
            }
        }

        public Comment Delete(int commentId, int userId)
        {
            Comment comment = _context.Comments.Find(commentId);
            if (comment != null && comment.DeletedAt == DateTime.MinValue)
            {
                if (comment.UserId == userId)
                {
                    Topic topic = _context.Topics.Find(comment.TopicId);
                    topic.CommentCount--;
                    comment.DeletedAt = DateTime.UtcNow;
                    _context.SaveChanges();
                    return comment;
                }
                else
                {
                    throw new AppException("You don't have permission to delete this comment");
                }
            }
            else
            {
                throw new AppException("Comment not found");
            }

        }


    }
}