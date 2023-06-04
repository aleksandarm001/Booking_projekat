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
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationReport.xaml
    /// </summary>
    public partial class ReservationReport : Window
    {
        public ReservationReport()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ReservationReport_Loaded);
        }
        private void ReservationReport_Loaded(object sender, RoutedEventArgs e)
        {
            this.ReportViewer.ReportPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Infrastructure/Resources/Reports/report.rdl");
            this.ReportViewer.RefreshReport();
        }
    }
}
