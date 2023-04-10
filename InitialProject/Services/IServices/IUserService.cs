using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetById(int id);
        User GetByUsername(string username);
        int NextId();
        User Save(User user);
    }
}