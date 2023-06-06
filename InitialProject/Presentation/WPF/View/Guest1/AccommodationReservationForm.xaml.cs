using ceTe.DynamicPDF;
using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Presentation.WPF.ViewModel;
using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using LiveCharts;
using LiveCharts.Wpf;
using Page = ceTe.DynamicPDF.Page;
using ColumnSeries = LiveCharts.Wpf.ColumnSeries;
using System.Windows.Media;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationForm.xaml
    /// </summary>
    public partial class AccommodationReservationForm : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<DateRange> _dateRanges;
        private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private IAccommodationReservationService _accommodationReservationService;
        private IUserService _userService;
        private int _userId;
        public ceTe.DynamicPDF.Document Report;
        public KeyValuePair<string ,int>[] Statistics { get; set; }
        public RelayCommand ApplyCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand FocusInformations_Command { get; private set; }
        public RelayCommand FocusTable_Command { get; private set; }
        public RelayCommand SelectDates_Command { get; private set; }
        public RelayCommand SelectStatistics_Command { get; private set; }
        public DateRange SelectedDateRange { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public string AccommodationName { get; set; }
        public List<Reservation> Reservations { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public ObservableCollection<DateRange> DateRanges

        {
            get { return _dateRanges; }
            set
            {
                _dateRanges = value;
                OnPropertyChanged(nameof(DateRanges));
            }
        }
        private DateTime _startDay;
        public DateTime StartDay
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
                    //EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, StartDay));
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
                    if (!string.IsNullOrEmpty(value)) { 
                         StartDay = new DateTime(Convert.ToInt32(values[2]), Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                        OnPropertyChanged("SStartDay");
                        OnPropertyChanged("StartDay");
                    }
                }
            }
        }
        private DateTime _endDay;
        public DateTime EndDay
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
                        IsEnabled = int.TryParse(value, out _reservationDays);
                        ReservationDays = _reservationDays;
                    }
                    catch (Exception) { }
                    _strReservationDays = value;
                    OnPropertyChanged(nameof(StrReservationDays));
                }
            }
        }
        private int _numberOfGuests;
        public int NumberOfGuests
        {
            get { return _numberOfGuests; }
            set
            {
                _numberOfGuests = value;
                OnPropertyChanged("NumberOfGuests");
            }
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get 
            { 
                return _isEnabled; 
            }
            set 
            { 
                _isEnabled = value; 
                OnPropertyChanged(nameof(IsEnabled)); 
            }
        }
        private bool _isStartDateValid;
        public bool IsStartDateValid
        {
            get { return _isStartDateValid; }
            set
            {
                _isStartDateValid = value;
                OnPropertyChanged(nameof(IsStartDateValid));
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public NumericValidation validation { get; set; }
        public AccommodationReservationForm(Accommodation accommodation, int userId)
        {
            InitializeComponent();
            _userId = userId;
            this.DataContext = this;
            SelectedAccommodation = accommodation;
            AccommodationName = accommodation.Name;
            Reservations = accommodation.Reservations;
            _reservationRepository = new ReservationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            InitializeDatePickers();
            DateRanges = new ObservableCollection<DateRange>();
            IsEnabled = false;
            InitializeCommands();
            validation = new NumericValidation();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _userService = Injector.CreateInstance<IUserService>();
            Statistics = _accommodationReservationService.GetAccommodationStatistics(SelectedAccommodation.AccommodationID);
            LoadColumnChartData();
        }

        private void InitializeCommands()
        {
            ApplyCommand = new RelayCommand(ApplyFilters_Command, CanApplyFilters);
            CloseCommand = new RelayCommand(CloseWindow_Command);
            FocusInformations_Command = new RelayCommand(FocusInformations);
            FocusTable_Command = new RelayCommand(FocusTable);
            SelectDates_Command = new RelayCommand(SelectDates);
            SelectStatistics_Command = new RelayCommand(SelectStatistics);
        }
        private void FocusInformations(object parameter)
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
        private void SelectDates(object parameter)
        {
            var tab = (parameter) as TabControl;
            tab.SelectedIndex = 0;

        }
        private void SelectStatistics(object parameter)
        {
            var tab = (parameter) as TabControl;
            tab.SelectedIndex = 1;

        }
        private void LoadColumnChartData()
        {
            //((ColumnSeries)mcChart.Series[0]).ItemsSource = Statistics;
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Fill= (Brush)(new BrushConverter().ConvertFrom("#00734B")),
                    Values = GetValues()
                }
            };
        }
        private ChartValues<int> GetValues()
        {
            ChartValues<int> result = new ChartValues<int>();
            foreach (KeyValuePair<string, int> pair in Statistics)
            {
                result.Add(pair.Value);
            }
            return result;
        }

        private void InitializeDatePickers()
        {
            //StartDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            //EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }
        private bool CanApplyFilters(object parameter)
        {
            var values = (object[])parameter;
            if (parameter != null)
            {
                return (!(bool)values[0] && !(bool)values[1] && !(bool)values[2]);
            }
            return false;
        }
        private void ApplyFilters_Command(object parameter)
        {

            if (CanApplyFilters(parameter) && IsFieldsEmpty())
            {
                ApplyFilters();
            }
            FocusDataGrid(parameter);
        }

        private static void FocusDataGrid(object parameter)
        {
            var values = (object[])parameter;
            var dataGrid = values[3] as DataGrid;
            if (dataGrid != null && dataGrid.Items.Count > 0)
            {
                dataGrid.Focus();
                dataGrid.SelectedItem = dataGrid.Items[0];
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
            }
        }

        private bool IsFieldsEmpty()
        {
            return !string.IsNullOrEmpty(SEndDay) && !string.IsNullOrEmpty(SStartDay) && !string.IsNullOrEmpty(StrReservationDays);
        }

        private void CloseWindow_Command(object parameter)
        {
            this.Close();
        }
        private List<DateRange> ExtractFreeDates(DateTime StartDay, DateTime EndDay)
        {
            List<DateRange> allDates = GetAllPossibleDates(StartDay, EndDay);
            List<DateRange> datesToRemove = new List<DateRange>();
            foreach (Reservation reservation in Reservations)
            {
                foreach (DateRange range in allDates)
                {
                    if (reservation.ReservationDateRange.WithinRange(range) && !datesToRemove.Contains(range))
                    {
                        datesToRemove.Add(range);
                    }
                }
            }
            RemoveUnavailableDates(allDates, datesToRemove);
            return allDates;
        }
        private void RemoveUnavailableDates(List<DateRange> allDates, List<DateRange> datesToRemove)
        {
            foreach (DateRange range in datesToRemove)
            {
                DateRange dateRange = allDates.Find(r => r.StartDate == range.StartDate && r.EndDate == range.EndDate);
                allDates.Remove(dateRange);
            }
        }
        private List<DateRange> GetAllPossibleDates(DateTime StartDay, DateTime EndDay)
        {
            List<DateRange> result = new List<DateRange>();
            for (var day = StartDay; day.Date <= EndDay; day = day.AddDays(1))
            {
                if(day.AddDays(ReservationDays).Date <= EndDay)
                {
                    DateRange range = new DateRange(day.Date, day.AddDays(ReservationDays).Date);
                    result.Add(range);
                }
            }
            return result;
        }
        private void ApplyFiltersButton(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            NoFreeReservation.Visibility = Visibility.Hidden;
            DateRanges.Clear();
            ShowFreeDatesForReservation();
        }

        private void ShowFreeDatesForReservation()
        {
            List<DateRange> freeDates = ExtractFreeDates(StartDay, EndDay);
            if (freeDates.Count == 0)
            {
                NoFreeReservation.Visibility = Visibility.Visible;
                DateRanges = new ObservableCollection<DateRange>(ExtractFreeDates(DateTime.Now, DateTime.Now.AddDays(90)));
            }
            else
            {
                DateRanges = new ObservableCollection<DateRange>(freeDates);
            }
        }
        private void DataGritMenuItemClick(object sender, RoutedEventArgs e)
        {
            EnterGuestNumberDialog guestNumberInputDialog = new EnterGuestNumberDialog(SelectedAccommodation.MaxGuestNumber);
            guestNumberInputDialog.Owner = this;
            guestNumberInputDialog.ShowDialog();
            if(guestNumberInputDialog.NumberOfGuests != 0)
            {
                NumberOfGuests = guestNumberInputDialog.NumberOfGuests;
                ReserveAccommodation(SelectedAccommodation.AccommodationID, _userId, SelectedDateRange, NumberOfGuests);
                MessageBox.Show("You successfuly reserved " + ReservationDays.ToString() + " day(s) at " + AccommodationName);
                guestNumberInputDialog.Close();
            }
        }
        private void ReserveAccommodation(int accommodationID, int userID, DateRange dateRange, int numberOfGuests)
        {
            Reservation reservation = new Reservation(dateRange, numberOfGuests, userID);
            AccommodationReservation accommodationReservation = new AccommodationReservation(accommodationID, _reservationRepository.NextId());
            SelectedAccommodation.Reservations.Add(reservation);
            Reservations.Add(reservation);
            DateRanges.Clear();
            ShowFreeDatesForReservation();
            _reservationRepository.Save(reservation);
            _accommodationReservationRepository.Save(accommodationReservation);
            CreateReport(SelectedAccommodation, dateRange, numberOfGuests);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            GenerateReport(accommodation,dateRange,numberOfGuests);
            Report.Draw("C:\\Users\\Aleksandar\\Desktop\\Report.pdf");
            MessageBox.Show("Report has been successfuly created and it is located in ../Users/Aleksandar/Desktop", "Report Creating", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }

        private void rezervacije_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (SelectedAccommodation == null)
                {
                    MessageBox.Show("Please select date.");
                }
                else
                {
                    EnterGuestNumberDialog guestNumberInputDialog = new EnterGuestNumberDialog(SelectedAccommodation.MaxGuestNumber);
                    guestNumberInputDialog.Owner = this;
                    guestNumberInputDialog.ShowDialog();
                    if (guestNumberInputDialog.NumberOfGuests != 0)
                    {
                        NumberOfGuests = guestNumberInputDialog.NumberOfGuests;
                        ReserveAccommodation(SelectedAccommodation.AccommodationID, _userId, SelectedDateRange, NumberOfGuests);
                        MessageBox.Show("You successfuly reserved " + ReservationDays.ToString() + " day(s) at " + AccommodationName);
                        guestNumberInputDialog.Close();
                    }
                }
            }
        }
    }
}