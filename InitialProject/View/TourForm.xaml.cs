﻿using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using static InitialProject.Model.TourPoint;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window
    {
        private readonly LanguageRepository _languageRepository;
        private readonly TourRepository _tourRepository;
        public static ObservableCollection<Language> Languages { get; set; }

        public TourForm()
        {
            InitializeComponent();
            DataContext = this;
            _languageRepository = new LanguageRepository();
            _tourRepository = new TourRepository();
            Languages = new ObservableCollection<Language>(_languageRepository.GetAll());

        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        
            
            
        private void SaveTour(object sender, RoutedEventArgs e)
        {

        }

        private void AddKeyPoint_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourPointForm addTourPoint = new TourPointForm();
            addTourPoint.Show();
        }

        private void AddDatesAndTimes_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddPictures_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            Language language = new Language();
            language.Name = "srpski";
            Location location = new Location();
            location.City = "zrenjanin";
            location.Country = "serbia";
            Tour tour = new Tour();
            tour.TourId = 1;
            tour.Name = "Test";
            tour.Location = location;
            tour.Duration = 5;
            tour.Description= "TestDesc";
            tour.Language = language;
            tour.MaxGuestNumber= 5;
            DateTime dateTime1 = new DateTime();
            dateTime1 = DateTime.Now;

            DateTime dateTime2 = new DateTime();
            dateTime2 = DateTime.Now;

            List<DateTime> list = new List<DateTime>();
            list.Add(dateTime1);
            list.Add(dateTime2);
            tour.StartingDateTimes = list;

            TourPoint pt = new TourPoint();
            pt.TourId = 1;
            pt.Name = "Test";
            pt.Order = 1;
            pt.CurrentActive = 0;
            pt.Description= "Tes2t";
            pt.Id = 1;

            List<TourPoint> points= new List<TourPoint>();
            points.Add(pt);

            tour.KeyPoints = points;

            _tourRepository.Save(tour);










    }

        
    }
}
