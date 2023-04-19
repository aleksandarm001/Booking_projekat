namespace InitialProject.View.Guest2
{
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    public partial class TourReview : Window
    {
        public static ObservableCollection<string> Rating { get; set; }
        public TourRate TourRate { get; set; }
        private TourReviewViewModel _tourReviewViewModel;

        public TourReview(int tourId, int userId)
        {
            _tourReviewViewModel = new TourReviewViewModel();
            TourRate = new TourRate();
            TourRate.TourId = tourId;
            TourRate.GuestId = userId;
            TourRate.IsValid = true;
            InitializeComponent();
            DataContext = this;
            InitializeRating();
            TourRate.Images = new List<string>();

        }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
            }
        }

        private void InitializeRating()
        {
            Rating = new ObservableCollection<string>();
            for (int i = 1; i <= 5; i++)
            {
                Rating.Add(i.ToString());
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            _tourReviewViewModel.RateTour(TourRate);
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (!TourRate.Images.Contains(Image))
            {
                TourRate.Images.Add(Image);
            }
        }
    }
}
