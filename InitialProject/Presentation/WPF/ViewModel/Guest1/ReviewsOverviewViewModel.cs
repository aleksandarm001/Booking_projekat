using InitialProject.Domen.CustomClasses;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class ReviewsOverviewViewModel : INotifyPropertyChanged
    {
        private readonly ReviewInfoService reviewInfoService;
        private ObservableCollection<ReviewInfoDTO> reviews;
        private double averageRate;
        private int reviewsNumber;

        public ObservableCollection<ReviewInfoDTO> Reviews
        {
            get { return reviews; }
            set
            {
                if (reviews != value)
                {
                    reviews = value;
                    OnPropertyChanged();
                }
            }
        }

        public double AverageRate
        {
            get { return averageRate; }
            set
            {
                if (averageRate != value)
                {
                    averageRate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ReviewsNumber
        {
            get { return reviewsNumber; }
            set
            {
                if (reviewsNumber != value)
                {
                    reviewsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReviewsOverviewViewModel(int userId)
        {
            reviewInfoService = new ReviewInfoService();
            Reviews = new ObservableCollection<ReviewInfoDTO>(reviewInfoService.GetReviewInfo(userId));
            InitializeAverageRate();
            ReviewsNumber = Reviews.Count;
        }

        private void InitializeAverageRate()
        {
            double sum = 0;
            foreach (var review in Reviews)
            {
                sum += review.Hygiene + review.FollowingRules;
            }
            AverageRate = sum / Reviews.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
