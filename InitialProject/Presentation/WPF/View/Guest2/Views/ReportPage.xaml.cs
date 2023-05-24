using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Guest2;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage(int userId)
        {
            ReportPageViewModel viewModel = new ReportPageViewModel(userId);
            InitializeComponent();
            DataContext = viewModel;

            DatePickerStart.DisplayDateEnd = DateTime.Today;
            DatePickerEnd.DisplayDateEnd = DateTime.Today;
        }

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePickerEnd.DisplayDateStart = (DateTime)DatePickerStart.SelectedDate;
            
        }

    }
}
