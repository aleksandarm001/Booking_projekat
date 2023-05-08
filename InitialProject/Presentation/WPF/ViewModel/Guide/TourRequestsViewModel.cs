using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services;
using InitialProject.Services.IServices;
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

        public ICommand FillterCommand { get; set; }

        private readonly ITourRequestService tourRequestService;
        private readonly ITourService tourService;

        public TourRequestsViewModel()
        {
            ApproveCommand = new RelayCommand(ApproveTourRequest);
            DeclineCommand = new RelayCommand(DeclineTourRequest);
            FillterCommand = new RelayCommand(FillterTourRequest);
            tourRequestService = Injector.CreateInstance<ITourRequestService>();
            tourService = Injector.CreateInstance<ITourService>();

            _tourRequests = new ObservableCollection<TourRequest>();
            var tourRequestsList = tourRequestService.GetAllRequests();
            foreach (var tourRequest in tourRequestsList)
            {
                _tourRequests.Add(tourRequest);
            }
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
            //should delete and to Tours
            DeleteTourRequest(SelectedTourRequest);
            //add to Tours txt
            tourService.Save((Tour)SelectedTourRequest);
            _tourRequests.Remove(SelectedTourRequest);
            tourRequestService.Delete(SelectedTourRequest);


        }

        private void DeleteTourRequest(TourRequest tourReq)
        {
            tourRequestService.Delete(tourReq);
        }

        private void FillterTourRequest(object obj)
        {
           AcceptedTourRequestView T = new AcceptedTourRequestView();
            T.Show();
        }

        private void DeclineTourRequest(object obj)
        {
            TourRequest selectedTourRequest = (TourRequest)obj;
            IsSelected = true;
            SelectedTourRequest = selectedTourRequest;
            DeleteTourRequest(selectedTourRequest);
            _tourRequests.Remove(SelectedTourRequest);
            tourRequestService.Delete(SelectedTourRequest);
        }

    }
}
