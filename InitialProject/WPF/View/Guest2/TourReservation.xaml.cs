using InitialProject.Model;
using InitialProject.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.View
{
    public partial class TourReservation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Tour Tour { get; set; }
        public int UserId { get; set; }
        private TourService _tourService;
        private ReservationService _reservationService;
        private VoucherService _voucherService;
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public ObservableCollection<string> VouchersString { get; set; }
        public string SelectedVoucher { get; set; }

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
            _tourService = new TourService();
            _reservationService = new ReservationService();
            _voucherService = new VoucherService();
            VouchersString = new ObservableCollection<string>();
            Vouchers = new ObservableCollection<Voucher>(_voucherService.GetAllForUser(userId));
            InitializeVouchers();

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

            if (Vaucer.SelectedIndex != -1)
            {
                Voucher voucher = Vouchers.ElementAt(Vaucer.SelectedIndex);
                _reservationService.MakeReservation(UserId, Tour.TourId, Tour.StartingDateTime, NumberOfGuests, voucher.Id);
                _tourService.ReduceMaxGuestNumber(Tour.TourId, NumberOfGuests);
                _voucherService.Delete(voucher);
            }
            else
            {
                _reservationService.MakeReservation(UserId, Tour.TourId, Tour.StartingDateTime, NumberOfGuests, 0);
                _tourService.ReduceMaxGuestNumber(Tour.TourId, NumberOfGuests);
            }

            MessageBox.Show("Rezervacija uspjesna");
            this.Close();
        }

        private void InitializeVouchers()
        {
            foreach (Voucher v in Vouchers)
            {
                VouchersString.Add(v.Name + " vrijedi do " + v.ValidUntil.ToShortDateString());
            }
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
