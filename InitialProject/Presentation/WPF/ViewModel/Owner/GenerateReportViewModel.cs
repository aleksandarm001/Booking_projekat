using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ceTe.DynamicPDF;
using InitialProject.Aplication.Factory;
using System.Windows;
using InitialProject.Services;
using InitialProject.Domen.Model;
using System.Xml.Linq;
using Eco.Logging;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class GenerateReportViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly RenovationService _renovationService;
        private readonly IUserService _userService;
        public ceTe.DynamicPDF.Document Report;
        public int UserId { get; set; }
        public RelayCommand CreateReportCommand { get; set; }
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;

        private string _name;
        public string AccommodationName
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        public GenerateReportViewModel(int userId)
        {
            UserId = userId;

            _renovationService = new RenovationService();
            _userService = Injector.CreateInstance<IUserService>();
            CreateReportCommand = new RelayCommand(Execute_CreateReportCommand);

        }

        private void GenerateReport()
        {
            Page page = new Page(PageSize.A4, PageOrientation.Portrait, 44.0f);
            Report.Pages.Add(page);

            string fullName = _userService.GetFullName(UserId);

            ceTe.DynamicPDF.PageElements.Label header = new ceTe.DynamicPDF.PageElements.Label("Izvjestaj o rezervacijama", 0, 0, 504, 100, Font.TimesRoman, 18, TextAlign.Center);
            ceTe.DynamicPDF.PageElements.Label user = new ceTe.DynamicPDF.PageElements.Label("Korisnik: " + fullName, 0, 50, 200, 20, Font.TimesRoman, 14, TextAlign.Left);
            ceTe.DynamicPDF.PageElements.Label accommodationName = new ceTe.DynamicPDF.PageElements.Label("Smestaj: " + AccommodationName, 0, 70, 200, 20, Font.TimesRoman, 14, TextAlign.Left);
            ceTe.DynamicPDF.PageElements.Label datefrom = new ceTe.DynamicPDF.PageElements.Label("Datum od: " + StartingDate.Date.ToShortDateString(), 0, 90, 200, 20, Font.TimesRoman, 14, TextAlign.Left);
            ceTe.DynamicPDF.PageElements.Label dateto = new ceTe.DynamicPDF.PageElements.Label("Datum do: " + EndingDate.Date.ToShortDateString(), 0, 110, 200, 20, Font.TimesRoman, 14, TextAlign.Left);

            page.Elements.Add(datefrom);
            page.Elements.Add(accommodationName);
            page.Elements.Add(dateto);
            page.Elements.Add(header);
            page.Elements.Add(user);

            if (_renovationService.AllForReport(AccommodationName).Count != 0)
            {
                ceTe.DynamicPDF.PageElements.Label reservationName = new ceTe.DynamicPDF.PageElements.Label("Ime korisnika", 0, 150, 200, 40, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label startDate = new ceTe.DynamicPDF.PageElements.Label("Pocetni datum", 120, 150, 504, 100, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label endDate = new ceTe.DynamicPDF.PageElements.Label("Krajnji datum", 270, 150, 504, 100, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label numberOfGuests = new ceTe.DynamicPDF.PageElements.Label("Broj gostiju", 420, 150, 504, 100, Font.TimesRoman, 14, TextAlign.Left);

                page.Elements.Add(reservationName);
                page.Elements.Add(startDate);
                page.Elements.Add(endDate);
                page.Elements.Add(numberOfGuests);

                float labelWidth = 150f; // Adjust the width of each label as needed
                float labelHeight = 30f; // Adjust the height of each label as needed
                float horizontalSpacing = 20f; // Adjust the horizontal spacing between labels as needed
                float verticalSpacing = 2f; // Adjust the vertical spacing between rows as needed
                float initialX = 0; // Initial starting X-coordinate
                float initialY = 180; // Initial starting Y-coordinate

                float currentX = initialX;
                float currentY = initialY;

                foreach (var element in _renovationService.AllForReport(AccommodationName))
                {
                    currentX = initialX;


                    ceTe.DynamicPDF.PageElements.Label id = new ceTe.DynamicPDF.PageElements.Label(element.UserName.ToString(), currentX, currentY, 130, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    ceTe.DynamicPDF.PageElements.Label StartingDate = new ceTe.DynamicPDF.PageElements.Label(element.ReservationDateRange.SStartDate.ToString(), currentX + 120f, currentY, 140, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    ceTe.DynamicPDF.PageElements.Label EndDate = new ceTe.DynamicPDF.PageElements.Label(element.ReservationDateRange.SEndDate.ToString(), currentX + 270f, currentY, 160, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    /*
                    ceTe.DynamicPDF.PageElements.Label attendance;
                    if (element.Status == Domen.Model.TourAttendance.AttendanceStatus.Present)
                    {
                        attendance = new ceTe.DynamicPDF.PageElements.Label("Prisutan", currentX + 430f, currentY, 100, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    }
                    else if (element.Status == Domen.Model.TourAttendance.AttendanceStatus.NotPresent)
                    {
                        attendance = new ceTe.DynamicPDF.PageElements.Label("Nije prisutan", currentX + 430f, currentY, 100, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    }
                    else
                    {
                        attendance = new ceTe.DynamicPDF.PageElements.Label("Nepoznato", currentX + 430f, currentY, 100, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    }
                    */
                    ceTe.DynamicPDF.PageElements.Label NumberOfGuests = new ceTe.DynamicPDF.PageElements.Label(element.NumberOfGuests.ToString(), currentX + 430f, currentY, 100, labelHeight, Font.TimesRoman, 11, TextAlign.Left);

                    // Add each label to the page
                    page.Elements.Add(id);
                    page.Elements.Add(StartingDate);
                    page.Elements.Add(EndDate);
                    page.Elements.Add(NumberOfGuests);

                    // Increment the X-coordinate for the next row
                    currentY += labelHeight + verticalSpacing;

                }
            }
            else
            {
                ceTe.DynamicPDF.PageElements.Label name = new ceTe.DynamicPDF.PageElements.Label("U izabranom periodu gost nije imao prisustva na turama", 0, 130, 330, 20, Font.TimesRoman, 11, TextAlign.Left);
                page.Elements.Add(name);
            }

                // Note: You may need to adjust the page dimensions or position of the labels based on your specific requirements.

            
        }

        private void Execute_CreateReportCommand(object parameter)
        {
            Report = new ceTe.DynamicPDF.Document();
            GenerateReport();
            Report.Draw("C:\\Users\\ASUS\\OneDrive\\Documents\\GitHub\\Booking_projekat\\OwnerReports\\Report.pdf");

            MessageBox.Show("Izvještaj je uspješno kreiran i možete ga pronaći unutar report foldera", "Kreiranje izvještaja", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }
    }
}
