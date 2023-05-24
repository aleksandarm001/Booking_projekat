using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface IUserService
    {
        int GetUserId();
        void UpdateUserId(int newUserId);
        List<User> GetAllUsers();
        User GetById(int id);
        User GetByUsername(string username);
        int NextId();
        User Save(User user);
        void Update(User user);
        void UsePoints(int userId);
        string GetUsername();
        string GetFullName(int userId);
        bool GetSuperGuest();
        int GetUserPoints();
    }
}