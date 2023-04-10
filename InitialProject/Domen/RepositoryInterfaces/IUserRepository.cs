using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Domen.RepositoryInterfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetById(int id);
        User GetByUsername(string username);
        int NextId();
        User Save(User user);
    }
}