using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.CustomClasses;
using InitialProject.Services.IServices;
using MDriven.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ForumCommentService : IForumCommentService
    {
        private readonly IForumCommentRepository _forumCommentRepository;
        public ForumCommentService()
        {
            _forumCommentRepository = Injector.CreateInstance<IForumCommentRepository>();
        }
        public void Delete(ForumComment forum)
        {
            _forumCommentRepository.Delete(forum);
        }

        public List<ForumComment> GetAll()
        {
            return _forumCommentRepository.GetAll();
        }

        public int GetCommentsNumberByForum(int forumId)
        {
            List<ForumComment> forums = _forumCommentRepository.GetAll();
            return forums.Where(forum => forum.ForumId == forumId).Count();
        }

        public ForumComment Save(ForumComment forum)
        {
            return _forumCommentRepository.Save(forum);
        }

        public void Update(ForumComment forum)
        {
            _forumCommentRepository.Update(forum);
        }
    }
}
