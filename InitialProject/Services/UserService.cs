using InitialProject.Domen.RepositoryInterfaces;
using InitialProject.Factory;
using InitialProject.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService()
        {
            _userRepository = Injector.userRepository();
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
        public int NextId()
        {
            return _userRepository.NextId();
        }
        public User Save(User user)
        {
            return _userRepository.Save(user);
        }
    }
}
