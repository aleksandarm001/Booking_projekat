namespace InitialProject.View.Guest2
{
    using InitialProject.Model;
    using InitialProject.Repository;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// Interaction logic for TourReview.xaml
    /// </summary>
    public partial class TourReview : Window
    {
        public static ObservableCollection<string> Rating { get; set; }
        public TourRate TourRate { get; set; }
        private TourRateRepository _tourRateRepository { get; set; }

        public TourReview(int tourId, int userId)
        {
            TourRate = new TourRate();
            _tourRateRepository = new TourRateRepository();
            TourRate.TourId = tourId;
            TourRate.GuestId = userId;
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
            Rating.Add("");
            for (int i = 1; i <= 5; i++)
            {
                Rating.Add(i.ToString());
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            _tourRateRepository.Save(TourRate);
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
