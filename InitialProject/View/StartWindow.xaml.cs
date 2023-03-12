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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly LocationRepository _locationRepository;
        private readonly TourPointRepository _tourPointRepository;
        public static ObservableCollection<Location> Locations { get; set; }
        public StartWindow()
        {
            InitializeComponent();
            DataContext = this;
            _locationRepository = new LocationRepository();
            _tourPointRepository = new TourPointRepository();
            Locations = new ObservableCollection<Location>(_locationRepository.getAll());
        }

        private void Guest1_ButtonClick(object sender, RoutedEventArgs e)
        {
            Guest1View guest1View = new Guest1View(Locations);
            guest1View.Show();
        }

        private void Guest2_ButtonClick(object sender, RoutedEventArgs e)
        {
            TourView tourView = new TourView(1); 
            tourView.Show();
        }

        private void Owner_ButtonClick(object sender, RoutedEventArgs e)
        {
            RegisterNewAccommodation newAccommodation = new RegisterNewAccommodation();
            newAccommodation.Show();
        }

        
        private void Guide_ButtonClick(object sender, RoutedEventArgs e)
        {
            _tourPointRepository.ClearTemp();
            TourForm tour = new TourForm();
            tour.Show();
        }
    }
}
