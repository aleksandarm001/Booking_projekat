﻿using InitialProject.CustomClasses;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View
{
    public partial class TourReservation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Tour Tour { get; set; }
        public int UserId { get; set; }
        private ReservationRepository _reservationRepository;
        private TourRepository _tourRepository;

        public int NumberOfGuests { get; set; }
        private string _strNumberOfGuests;

    

        public string StrNumberOfGuests
        {
            get => _strNumberOfGuests;
            set
            {
                if (value != _strNumberOfGuests)
                {
                    try
                    {
                        int _numberOfGuests;
                        int.TryParse(value, out _numberOfGuests);
                        NumberOfGuests = _numberOfGuests;
                    }
                    catch (Exception) { }
                    _strNumberOfGuests = value;
                }
            }
        }

        public TourReservation(int userId, Tour t, int NumberOfGuests)
        {
            InitializeComponent();
            DataContext = this;
            Tour = t;
            UserId = userId;
            this.NumberOfGuests = NumberOfGuests;
            _reservationRepository = new ReservationRepository();
            _tourRepository = new TourRepository();

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfGuests > Tour.MaxGuestNumber)
            { 
                ImpossibleMakeReservation();
            }
            else
            {
                MakeReservation();
            }

        }

        private void ImpossibleMakeReservation()
        {
            string messageBoxText = "Nema dovoljno mjesta. Broj preostalih mjesta je " + Tour.MaxGuestNumber;
            string caption = "Rezervacija ture";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.No)
            {
                this.Close();
            }
        }

        private void MakeReservation()
        {
            Reservation reservation = new Reservation(UserId, Tour.TourId, Tour.StartingDateTime, NumberOfGuests);
            _reservationRepository.Save(reservation);
            _tourRepository.ReduceMaxGuestNumber(Tour.TourId, NumberOfGuests);

            MessageBox.Show("Rezervacija uspjesna");
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}