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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Guest1
{
    /// <summary>
    /// Interaction logic for ReviewsOverview.xaml
    /// </summary>
    public partial class ReviewsOverview : Window, INotifyPropertyChanged
    {
        private readonly ReviewInfoService reviewInfoService;
        public ObservableCollection<ReviewInfoDTO> Reviews { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public double AverageRate { get; set; }
        public int ReviewsNumber { get; set; }
        public ReviewsOverview(int userId)
        {
            InitializeComponent();
            DataContext = this;
            reviewInfoService = new ReviewInfoService();
            Reviews = new ObservableCollection<ReviewInfoDTO>(reviewInfoService.GetReviewInfo(userId));
            InitializeAverageRate();
            ReviewsNumber = Reviews.Count();

        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void InitializeAverageRate()
        {
            double sum = 0;
            foreach(var review in Reviews)
            {
                sum += review.Hygiene + review.FollowingRules;
            }
            AverageRate = sum / Reviews.Count();
        }
    }
}
