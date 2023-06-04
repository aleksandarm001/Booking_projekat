using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class GuestsToReviewViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IGuestReviewService _guestReviewService;

       //public static List<GuestReview> GuestReviews { get; set; }
        public static ObservableCollection<UserToReview> UsersToReview { get; set; }

        int UserId;

        public UserToReview SelectedUserToReview { get; set; }

        public RelayCommand ReviewGuest { get; set; }

        public GuestsToReviewViewModel(int userId)
        {
            UserId = userId;
            _guestReviewService = Injector.CreateInstance<IGuestReviewService>();
            UsersToReview = new ObservableCollection<UserToReview>(_guestReviewService.GetUsersByID(UserId));

            ReviewGuest = new RelayCommand(Review_ButtonClick);
        }

        private void Review_ButtonClick(object parameter)
        {
            if (SelectedUserToReview == null)
            {
                MessageBox.Show("User to review must be selected");
            }
            else
            {
                GuestReviewForm guestReviewForm = new GuestReviewForm(SelectedUserToReview.Guest1Id, SelectedUserToReview.AccommodationId);
                guestReviewForm.ShowDialog();
                if (guestReviewForm.IsReviewd)
                {
                    _guestReviewService.DeleteByIdAndDate(SelectedUserToReview.Guest1Id, SelectedUserToReview.LeavingDay);
                    UsersToReview.Remove(SelectedUserToReview);
                }
            }
        }

    }
}
