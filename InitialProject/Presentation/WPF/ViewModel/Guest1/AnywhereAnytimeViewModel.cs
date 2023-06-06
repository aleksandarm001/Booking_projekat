using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using MDriven.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Page = ceTe.DynamicPDF.Page;
using System.Windows.Input;
using ceTe.DynamicPDF;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class AnywhereAnytimeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AccommodationReservationDTO> _accommodations;
        private readonly IAccommodationService _accommodationService;
        private readonly IAccommodationReservationService _accommodationReservationService;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        public ceTe.DynamicPDF.Document Report;
        public AccommodationReservationDTO SelectedAccommodation { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand ApplyFiltersCommand { get; set; }
        public RelayCommand ResetFiltersCommand { get; set; }
        public RelayCommand ReserveCommand { get; set; }
        public RelayCommand FocusFilters_Command { get; set; }
        public RelayCommand FocusTable_Command { get; set; }
        private RelayCommand _reserveOnEnter_Command;
        public RelayCommand ReserveOnEnter_Command
        {
            get
            {
                return _reserveOnEnter_Command ?? ( _reserveOnEnter_Command = new RelayCommand(x => { Reserve(); }));
            }
        }
        public ObservableCollection<AccommodationReservationDTO> Accommodations
        {
            get 
            { 
                return _accommodations; 
            }
            set
            {
                _accommodations = value;
                OnPropertyChanged(nameof(Accommodations));
            }
        }
        private int _accommodationsNumber;
        public int AccommodationsNumber
        {
            get => _accommodationsNumber;
            set
            {
                if (value != _accommodationsNumber)
                {
                    _accommodationsNumber = value;
                    OnPropertyChanged(nameof(AccommodationsNumber));
                }
            }
        }
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
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationDays { get; set; }
        private string _strReservationDays;
        public string StrReservationDays
        {
            get => _strReservationDays;
            set
            {
                if (value != _strReservationDays)
                {
                    try
                    {
                        int _reservationDays;
                        int.TryParse(value, out _reservationDays);
                        ReservationDays = _reservationDays;
                    }
                    catch (Exception) { }
                    _strReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime? _startDay;
        public DateTime? StartDay
        {
            get
            {
                return _startDay;
            }
            set
            {
                if (value != _startDay)
                {
                    _startDay = value;
                    OnPropertyChanged("StartDay");
                }
            }
        }
        private string _sStartDay;
        public string SStartDay
        {
            get
            {
                return _sStartDay;
            }
            set
            {
                if (value != _sStartDay)
                {
                    _sStartDay = value;
                    string[] values = _sStartDay.Split('/');
                    if (!string.IsNullOrEmpty(value))
                    {
                        StartDay = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        OnPropertyChanged("SStartDay");
                        OnPropertyChanged("StartDay");
                    }
                }
            }
        }

        private DateTime? _endDay;
        public DateTime? EndDay
        {
            get
            {
                return _endDay;
            }
            set
            {
                if (value != _endDay)
                {
                    _endDay = value;
                    OnPropertyChanged("EndDay");
                }
            }
        }

        private string _sEndDay;
        public string SEndDay
        {
            get
            {
                return _sEndDay;
            }
            set
            {
                if (value != _sEndDay)
                {
                    _sEndDay = value;
                    string[] values = _sEndDay.Split('/');
                    if (!string.IsNullOrEmpty(value))
                    {
                        EndDay = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        OnPropertyChanged("SEndDay");
                        OnPropertyChanged("EndDay");
                    }
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AnywhereAnytimeViewModel()
        {
            _accommodations = new ObservableCollection<AccommodationReservationDTO>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _reservationService = Injector.CreateInstance<IReservationService>();
            _userService = Injector.CreateInstance<IUserService>();
            InitializeCommands();
            AccommodationsNumber = 0;
        }

        private void InitializeCommands()
        {
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
            ReserveCommand = new RelayCommand(MakeReservation);
            FocusFilters_Command = new RelayCommand(FocusFilters);
            FocusTable_Command = new RelayCommand(FocusTable);
        }
        private void FocusFilters(object parameter)
        {
            var textBox = parameter as TextBox;
            textBox.Focus();
        }
        private void FocusTable(object parameter)
        {
            var dataGrid = parameter as DataGrid;
            dataGrid.Focus();
            if(dataGrid.Items.Count != 0)
            {
                dataGrid.SelectedItem = dataGrid.Items[0];
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
            }
        }
        private void ApplyFilters(object parameter)
        {
            if (CanApplyFilters(parameter) && IsFieldsEmpty())
            {
                UpdateAccommodations();
            }
        }
        private bool IsFieldsEmpty()
        {
            return !string.IsNullOrEmpty(StrReservationDays) && !string.IsNullOrEmpty(StrNumberOfGuests);
        }
        private bool CanApplyFilters(object parameter)
        {
            var values = (object[])parameter;
            if (parameter != null)
            {
                return !(bool)values[0] && !(bool)values[1] && !(bool)values[2] && !(bool)values[3];
            }
            return false;
        }
        private void UpdateAccommodations()
        {
            Accommodations.Clear();
            CheckDatesIfNull();
            List<Accommodation> accommodations = _accommodationService.GetAccommodationsByGuestsAndDaysReserved(NumberOfGuests, ReservationDays);
            foreach (Accommodation accommodation in accommodations)
            {
                List<DateRange> days = _accommodationReservationService.GetAvailableDays(accommodation.AccommodationID, ReservationDays, (DateTime)StartDay, (DateTime)EndDay);
                CreateAccommodationReservationDTO(accommodation, days);
            }
            AccommodationsNumber = Accommodations.Count();
        }
        private void CheckDatesIfNull()
        {
            if(StartDay == null && EndDay == null)
            {
                StartDay = DateTime.Now;
                EndDay = DateTime.Now.AddDays(60);
            }
        }
        private void CreateAccommodationReservationDTO(Accommodation accommodation,List<DateRange> days)
        {
            foreach(DateRange day in days)
            {
                AccommodationReservationDTO a = new AccommodationReservationDTO(accommodation.AccommodationID,accommodation.Name, accommodation.Location, accommodation.TypeOfAccommodation, day);
                Accommodations.Add(a);
            }
        }
        public void ResetFilters(object parameter)
        {
            SStartDay = string.Empty;
            SEndDay = string.Empty;
            StartDay = null;
            EndDay = null;
            StrReservationDays = string.Empty;
            StrNumberOfGuests = string.Empty;
            Accommodations.Clear();
            AccommodationsNumber = 0;
        }
        public void MakeReservation(object parameter)
        {
            Reserve();
        }

        private void Reserve()
        {
            if (MessageBox.Show("Confirm reservation", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                Reservation reservation = new Reservation(SelectedAccommodation.CheckInOutDates, NumberOfGuests, _userService.GetUserId());
                _reservationService.Save(reservation);
                AccommodationReservation accommodationReservation = new AccommodationReservation(SelectedAccommodation.AccommodationId, reservation.ReservationId);
                _accommodationService.Save(accommodationReservation);
                MessageBox.Show("You successfuly reserved " + StrReservationDays + " day(s) at " + SelectedAccommodation.AccommodationName + " for " + StrNumberOfGuests + " people.(" + SelectedAccommodation.CheckInOutDates.ToStringForPrint() + ")");
                Accommodation accommodation = _accommodationService.GetAccommodationById(SelectedAccommodation.AccommodationId);
                CreateReport(accommodation, SelectedAccommodation.CheckInOutDates, NumberOfGuests);
                UpdateAccommodations();
            }
        }

        private void GenerateReport(Accommodation accommodation, DateRange dateRange, int numberOfGuests)
        {
            Page page = new Page(PageSize.A4, PageOrientation.Portrait, 44.0f);
            Report.Pages.Add(page);
            string today = string.Format("{0:dd.MM.yyyy.}", DateTime.Now);
            string fullName = _userService.GetFullName();
            ceTe.DynamicPDF.PageElements.Label header = new ceTe.DynamicPDF.PageElements.Label("Reservation report", 0, 0, 504, 100, Font.TimesRoman, 20, TextAlign.Center);
            ceTe.DynamicPDF.PageElements.Label user = new ceTe.DynamicPDF.PageElements.Label("User: " + fullName, 0, 50, 200, 20, Font.TimesRoman, 14, TextAlign.Left);
            ceTe.DynamicPDF.PageElements.Label datefrom = new ceTe.DynamicPDF.PageElements.Label("Today date: " + today, 0, 70, 200, 20, Font.TimesRoman, 14, TextAlign.Left);
            //ceTe.DynamicPDF.PageElements.Label dateto = new ceTe.DynamicPDF.PageElements.Label("Datum do: " + dateRange.EndDate.Date.ToShortDateString(), 0, 90, 200, 20, Font.TimesRoman, 14, TextAlign.Left);

            page.Elements.Add(datefrom);
            page.Elements.Add(header);
            page.Elements.Add(user);

            ReservationReportDTO reservationReportDTO = new ReservationReportDTO(accommodation.Name, accommodation.Location, dateRange, numberOfGuests, accommodation.DaysBeforeCancelling);

            if (reservationReportDTO != null)
            {

                ceTe.DynamicPDF.PageElements.Label accommodationName = new ceTe.DynamicPDF.PageElements.Label("Accommodation", 0, 100, 200, 40, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label accommodationLocation = new ceTe.DynamicPDF.PageElements.Label("Location", 130, 100, 504, 100, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label checkInDate = new ceTe.DynamicPDF.PageElements.Label("Check in", 240, 100, 504, 100, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label checkOutDate = new ceTe.DynamicPDF.PageElements.Label("Check out", 320, 100, 504, 100, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label numOfGuests = new ceTe.DynamicPDF.PageElements.Label("Number of guests", 400, 100, 504, 100, Font.TimesRoman, 14, TextAlign.Left);


                page.Elements.Add(accommodationName);
                page.Elements.Add(accommodationLocation);
                page.Elements.Add(checkInDate);
                page.Elements.Add(checkOutDate);
                page.Elements.Add(numOfGuests);

                float labelWidth = 100f; // Adjust the width of each label as needed
                float labelHeight = 10f; // Adjust the height of each label as needed
                float horizontalSpacing = 15f; // Adjust the horizontal spacing between labels as needed
                float verticalSpacing = 2f; // Adjust the vertical spacing between rows as needed
                float initialX = 0; // Initial starting X-coordinate
                float initialY = 160; // Initial starting Y-coordinate

                float currentX = initialX;
                float currentY = initialY;


                currentX = initialX;


                ceTe.DynamicPDF.PageElements.Label name = new ceTe.DynamicPDF.PageElements.Label(reservationReportDTO.AccommodationName, currentX, currentY, 130, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label location = new ceTe.DynamicPDF.PageElements.Label(reservationReportDTO.AccommdoationLocation.ToString(), currentX + 130f, currentY, 140, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label date1 = new ceTe.DynamicPDF.PageElements.Label(reservationReportDTO.ReservationDateRange.SStartDate, currentX + 240f, currentY, 140, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label date2 = new ceTe.DynamicPDF.PageElements.Label(reservationReportDTO.ReservationDateRange.SEndDate, currentX + 320f, currentY, 140, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label guestNumber = new ceTe.DynamicPDF.PageElements.Label(reservationReportDTO.NumberOfGuests.ToString(), currentX + 400f, currentY, 160, labelHeight, Font.TimesRoman, 11, TextAlign.Left);

                // Add each label to the page
                page.Elements.Add(name);
                page.Elements.Add(location);
                page.Elements.Add(date1);
                page.Elements.Add(date2);
                page.Elements.Add(guestNumber);

                // Increment the X-coordinate for the next row
                currentY += labelHeight + verticalSpacing;

                float labelEndX = page.Dimensions.Width - labelWidth - 120f; // Adjust the X-coordinate and spacing as needed
                float labelEndY = page.Dimensions.Height - labelHeight - 120f; // Adjust the Y-coordinate and spacing as needed

                ceTe.DynamicPDF.PageElements.Label endLabel = new ceTe.DynamicPDF.PageElements.Label("BookBuddy LLC", labelEndX, labelEndY, labelWidth, labelHeight, Font.TimesRoman, 18, TextAlign.Right);
                page.Elements.Add(endLabel);

            }
            // Note: You may need to adjust the page dimensions or position of the labels based on your specific requirements.
        }
        private void CreateReport(Accommodation accommodation, DateRange dateRange, int numberOfGuests)
        {
            Report = new ceTe.DynamicPDF.Document();
            GenerateReport(accommodation, dateRange, numberOfGuests);
            Report.Draw("C:\\Users\\Aleksandar\\Desktop\\Report.pdf");
            MessageBox.Show("Report has been successfuly created and it is located in ../Users/Aleksandar/Desktop", "Report Creating", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }
    }
}
