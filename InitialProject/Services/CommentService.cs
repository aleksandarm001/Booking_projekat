using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IForumCommentService _forumCommentService;
        public CommentService()
        {
            _commentRepository = Injector.CreateInstance<ICommentRepository>();
            _forumCommentService = Injector.CreateInstance<IForumCommentService>();
        }
        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetByCommentId(int commentId)
        {
            return GetAll().Find(comment => comment.CommentId == commentId);
        }

        public List<Comment> GetByUser(int userId)
        {
            return _commentRepository.GetByUser(userId);
        }
        public Comment Save(Comment comment)
        {
            return _commentRepository.Save(comment);
        }

        public Comment Update(Comment comment)
        {
            return _commentRepository.Update(comment);
        }
        
        public List<Comment> CommentsByForumId(int forumId)
        {
            List<int> commentsId = _forumCommentService.GetCommentsIdByForumId(forumId);
            List<Comment> comments = new List<Comment>();
            foreach (int commentId in commentsId)
            {
                comments.Add((GetByCommentId(commentId)));
            }
            return comments;
        }
        
        public Comment CreateOwnerComment(string text, int userId)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.UserId = userId;
            comment.CreationTime = DateTime.Now;
            comment.IsOwnerComment = true;

            return comment;

        }
        
    }
}
