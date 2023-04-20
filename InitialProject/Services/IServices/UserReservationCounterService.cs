using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace InitialProject.Services.IServices
{
    public class UserReservationCounterService : IUserReservationCounterService
    {
        private readonly IUserReservationCounterRepository _userReservationCounterRepository;
        private readonly IUserService _userService;
        public UserReservationCounterService()
        {
            _userReservationCounterRepository = new UserReservationCounterRepository();
            _userService = new UserService();
        }
        public void UpdateReservationCounter(int userId)
        {
            List<UserReservationCounter> users = _userReservationCounterRepository.GetAll();
            UserReservationCounter user = users.Find(u => u.UserId == userId);
            user.ReservationCount++;
            _userReservationCounterRepository.Update(user);
        }
        public void InitializeReservationCounter(int userId)
        {
            List<UserReservationCounter> users = _userReservationCounterRepository.GetAll();
            UserReservationCounter user = users.Find(u => u.UserId == userId);
            if(user == null)
            {
                UserReservationCounter newUser = new UserReservationCounter(userId, 0, new DateTime(DateTime.Now.Year, 1, 1));
                _userReservationCounterRepository.Save(newUser);
            }
            else
            {
                if(user.InitialDate.Year == DateTime.Now.Year - 1)
                {
                    CheckUserForSuperGuest(userId, user.ReservationCount);
                    ResetData(userId, users);
                }
            }
        }
        public UserReservationCounter GetReservationCounterByUserId(int userId)
        {
            List<UserReservationCounter> users = _userReservationCounterRepository.GetAll();
            return users.Find(user => user.UserId == userId);
        }
        private void CheckUserForSuperGuest(int userId, int reservationCount)
        {
            User user = _userService.GetById(userId);
            if (reservationCount >= 10)
            {
                user.Points = 5;
                user.IsSuperGuest = true;
                _userService.Update(user);
            }
            else
            {
                user.Points = 0;
                user.IsSuperGuest = false;
                _userService.Update(user);
            }
        }
        private void ResetData(int userId, List<UserReservationCounter> users)
        {
            UserReservationCounter user = users.Find(u => u.UserId == userId);
            user.InitialDate = new DateTime(DateTime.Now.Year, 1, 1);
            user.ReservationCount = 0;
            _userReservationCounterRepository.Update(user);
        }
    }

}
