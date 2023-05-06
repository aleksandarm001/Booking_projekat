using InitialProject.Domen.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class TourRequestsViewModel : INotifyPropertyChanged
    {
        public ICommand ApproveCommand { get; set; }
        public ICommand DeclineCommand { get; set; }

        public TourRequestsViewModel()
        {
            ApproveCommand = new RelayCommand(ApproveTourRequest);
            DeclineCommand = new RelayCommand(DeclineTourRequest);

            _tourRequests = new ObservableCollection<TourRequest>
            {
                new TourRequest { Id = 1, Description = "opis1"},
                new TourRequest { Id = 2, Description = "opis2" },
                new TourRequest { Id = 3, Description = "opis3" }
            };
        }

        private ObservableCollection<TourRequest> _tourRequests { get; set; }
        public ObservableCollection<TourRequest> TourRequests
        {
            get { return _tourRequests; }
            set
            {
                _tourRequests = value;
                OnPropertyChanged();
            }
        }

        private TourRequest _selectedTourRequest;
        public TourRequest SelectedTourRequest
        {
            get { return _selectedTourRequest; }
            set
            {
                _selectedTourRequest = value;
                OnPropertyChanged(nameof(SelectedTourRequest));
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ApproveTourRequest(object obj)
        {
            TourRequest selectedTourRequest = (TourRequest)obj;
            IsSelected = true;
            SelectedTourRequest = selectedTourRequest;
            // Do something with the selected tour request
        }

        private void DeclineTourRequest(object obj)
        {
            TourRequest selectedTourRequest = (TourRequest)obj;
            IsSelected = true;
            SelectedTourRequest = selectedTourRequest;
        }

    }
}
