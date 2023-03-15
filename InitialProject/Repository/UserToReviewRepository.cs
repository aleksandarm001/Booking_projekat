using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class UserToReviewRepository
    {

        private const string FilePath = "../../../Resources/Data/userstoreview.txt";

        private readonly Serializer<UserToReview> _serializer;

        private List<UserToReview> UsersToReview;

        public UserToReviewRepository()
        {
            _serializer = new Serializer<UserToReview>();
            UsersToReview = _serializer.FromCSV(FilePath);
        }

        public UserToReview GetByOwnerId(int ownerId)
        {
            UsersToReview = _serializer.FromCSV(FilePath);
            return UsersToReview.FirstOrDefault(u => u.OwnerId == ownerId);
        }

        public List<UserToReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public UserToReview Save(UserToReview user)
        {
            UsersToReview.Add(user);
            _serializer.ToCSV(FilePath, UsersToReview);
            return user;
        }
        public void DeleteById(int Guest1Id)
        {
            UsersToReview = _serializer.FromCSV(FilePath);
            UserToReview userToRemove = UsersToReview.Find(u => u.Guest1Id == Guest1Id);
            UsersToReview.Remove(userToRemove);
            _serializer.ToCSV(FilePath, UsersToReview);
        }
    }
}
