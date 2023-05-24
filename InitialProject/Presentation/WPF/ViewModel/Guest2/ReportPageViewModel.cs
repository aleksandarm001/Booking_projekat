using ceTe.DynamicPDF;
using InitialProject.Aplication.Factory;
using InitialProject.Services.IServices;
using System;
using System.Linq;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{

    class ReportPageViewModel
    {

        private readonly ITourService _tourService;
        private readonly IUserService _userService;
        public ceTe.DynamicPDF.Document Report;
        public int UserId { get; set; }
        public RelayCommand CreateReportCommand { get; set; }
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public ReportPageViewModel(int userId)
        {
            UserId = userId;
            _tourService = Injector.CreateInstance<ITourService>();
            _userService = Injector.CreateInstance<IUserService>();
            CreateReportCommand = new RelayCommand(Execute_CreateReportCommand);

        }

        private void GenerateReport()
        {
            Page page = new Page(PageSize.A4, PageOrientation.Portrait, 44.0f);
            Report.Pages.Add(page);

            string fullName = _userService.GetFullName(UserId);
            ceTe.DynamicPDF.PageElements.Label header = new ceTe.DynamicPDF.PageElements.Label("Izvjestaj o rezervacijama", 0, 0, 504, 100, Font.TimesRoman, 18, TextAlign.Center);
            ceTe.DynamicPDF.PageElements.Label user = new ceTe.DynamicPDF.PageElements.Label("Korisnik: " + fullName , 0,50,200,20, Font.TimesRoman, 14,TextAlign.Left);

            ceTe.DynamicPDF.PageElements.Label datefrom = new ceTe.DynamicPDF.PageElements.Label("Datum od: " + StartingDate.Date.ToShortDateString(), 0,70,200,20, Font.TimesRoman, 14,TextAlign.Left);
            ceTe.DynamicPDF.PageElements.Label dateto = new ceTe.DynamicPDF.PageElements.Label("Datum do: " + EndingDate.Date.ToShortDateString(), 0,90,200,20, Font.TimesRoman, 14,TextAlign.Left);

            page.Elements.Add(datefrom);
            page.Elements.Add(dateto);
            page.Elements.Add(header);
            page.Elements.Add(user);

            if (_tourService.GetAllForReport(UserId, StartingDate, EndingDate).Count != 0)
            {

                ceTe.DynamicPDF.PageElements.Label nameTour = new ceTe.DynamicPDF.PageElements.Label("Naziv ture", 0, 130, 200, 40, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label locationTour = new ceTe.DynamicPDF.PageElements.Label("Lokacija", 120, 130, 504, 100, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label dateTour = new ceTe.DynamicPDF.PageElements.Label("Datum izvodjenja", 270, 130, 504, 100, Font.TimesRoman, 14, TextAlign.Left);
                ceTe.DynamicPDF.PageElements.Label attendanceTour = new ceTe.DynamicPDF.PageElements.Label("Prisustvo gosta", 430, 130, 504, 100, Font.TimesRoman, 14, TextAlign.Left);



             
                page.Elements.Add(nameTour);
                page.Elements.Add(locationTour);
                page.Elements.Add(dateTour);
                page.Elements.Add(attendanceTour);

                float labelWidth = 150f; // Adjust the width of each label as needed
                float labelHeight = 30f; // Adjust the height of each label as needed
                float horizontalSpacing = 20f; // Adjust the horizontal spacing between labels as needed
                float verticalSpacing = 2f; // Adjust the vertical spacing between rows as needed
                float initialX = 0; // Initial starting X-coordinate
                float initialY = 160; // Initial starting Y-coordinate

                float currentX = initialX;
                float currentY = initialY;

                foreach (var element in _tourService.GetAllForReport(UserId, StartingDate, EndingDate))
                {
                    currentX = initialX;


                    ceTe.DynamicPDF.PageElements.Label name = new ceTe.DynamicPDF.PageElements.Label(element.Name, currentX, currentY, 130, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    ceTe.DynamicPDF.PageElements.Label location = new ceTe.DynamicPDF.PageElements.Label(element.Location.ToString3(), currentX + 120f, currentY, 140, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
                    ceTe.DynamicPDF.PageElements.Label date = new ceTe.DynamicPDF.PageElements.Label(element.StartingDateTime.Date.ToLongDateString(), currentX + 270f, currentY, 160, labelHeight, Font.TimesRoman, 11, TextAlign.Left);
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

                    

                    // Add each label to the page
                    page.Elements.Add(name);
                    page.Elements.Add(location);
                    page.Elements.Add(date);
                    page.Elements.Add(attendance);

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
            Report.Draw("C:\\Users\\I\\Desktop\\Projekat\\Booking_projekat\\Reports\\Report.pdf");

            MessageBox.Show("Izvještaj je uspješno kreiran i možete ga pronaći unutar report foldera", "Kreiranje izvještaja", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }
    }
}
