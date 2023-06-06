using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using Microsoft.TeamFoundation.Build.WebApi;
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
        private IAccommodationService _accommodationService;
        public ForumService()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
        }
        public void Delete(Forum forum)
        {
            _forumRepository.Delete(forum);
        }

        public List<Forum> GetActiveForumsByCreatorId(int userId)
        {
            List<Forum> forums = GetAll();
            return forums.Where(forum => forum.CreatorId == userId && forum.Status == ForumStatus.Open).ToList();
        }

        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }

        public Forum GetForumById(int forumId)
        {
            return GetAll().Find(forum => forum.ForumId == forumId);
        }

        public List<Forum> GetForumsByCreatorId(int userId)
        {
            List<Forum> forums = GetAll();
            return forums.Where(forum => forum.CreatorId == userId).ToList();
        }

        public Dictionary<int, string> GetForumsByUserKeyValue(int userId)
        {
            List<Forum> forums = GetActiveForumsByCreatorId(userId);
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (Forum forum in forums)
            {
                string value = forum.Location.ToString() + " - " + forum.ForumTopic;
                result.Add(forum.ForumId, value);
            }
            return result;
        }

        public string GetTopic(int forumId)
        {
            Forum forum = GetForumById(forumId);
            return forum.ForumTopic;
        }

        public Forum Save(Forum forum)
        {
            return _forumRepository.Save(forum);
        }

        public void Update(Forum forum)
        {
            _forumRepository.Update(forum);
        }
        public Location GetLocation(int forumId)
        {
            Forum forum = GetForumById(forumId);
            return forum.Location;
        }

        public List<Forum> ForumsForOwners(int userId)
        {
            List<Accommodation> accommodations = _accommodationService.GetAccommodationsByOwnerId(userId);
            List<Forum> allForums = GetAll();
            List<Forum> matchingForums = new List<Forum>();

            //List<Forum> matchingForums = allForums.Where(forum => accommodations.Any(accommodation => accommodation.Location == forum.Location)).ToList();
            foreach (Forum forum in allForums)
            {
                bool hasMatchingLocation = false;

                foreach (var accommodation in accommodations)
                {
                    if (accommodation.Location.Country == forum.Location.Country && accommodation.Location.City == forum.Location.City)
                    {
                        hasMatchingLocation = true;
                        break;
                    }
                }

                if (hasMatchingLocation)
                {
                    matchingForums.Add(forum);
                }

            }

             return matchingForums;
        }

        public List<Forum> OpenedForums(int userId)
        {
            List<Forum> openedForums = new List<Forum>();
            List<Forum> forums = ForumsForOwners(userId);
            foreach(Forum forum in forums)
            {
                if(forum.Status == ForumStatus.Open)
                {
                    openedForums.Add(forum);
                }
            }
            return openedForums;
        }


    }
}
