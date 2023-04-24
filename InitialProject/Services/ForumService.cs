using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ForumService : IForumService
    {
        private IForumRepository _forumRepository;
        public ForumService()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
        }
        public void Delete(Forum forum)
        {
            _forumRepository.Delete(forum);
        }

        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }
        public Forum Save(Forum forum)
        {
            return _forumRepository.Save(forum);
        }

        public void Update(Forum forum)
        {
            _forumRepository.Save(forum);
        }
    }
}
