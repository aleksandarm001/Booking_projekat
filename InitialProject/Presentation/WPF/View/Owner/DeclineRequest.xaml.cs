using InitialProject.Domen.Model;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for DeclineRequest.xaml
    /// </summary>
    public partial class DeclineRequest : Window,INotifyPropertyChanged
    {
        private readonly ChangeReservationRequestService _requestService = new ChangeReservationRequestService();

       // public ChangeReservationRequest _request;
/*
        public ChangeReservationRequest Request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged(nameof(Request));
            }
        }

*/
       
        public int RequestId { get; set; }
        public string OwnerComment { get; set; }
        
        public DeclineRequest(int requestId)
        {
            InitializeComponent();
            RequestId= requestId;
            OwnerComment= string.Empty;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerComment= CommentBox.Text;
            _requestService.DeclineRequest(RequestId, OwnerComment);
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
