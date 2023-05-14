using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class ComplexTourRequestsViewModel : INotifyPropertyChanged
    {
        private readonly IComplexTourRequestService complexTourRequestService;
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        public ObservableCollection<ComplexTourRequest> TourOfComplexRequests { get; set; }

        public ICommand InspectCommand { get; set; }

        public ICommand BackToComplexToursCommand { get; set; }
        public ComplexTourRequestsViewModel()
        {
            complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(complexTourRequestService.GetAllUniqueTourRequests());
            TourOfComplexRequests = new ObservableCollection<ComplexTourRequest>();
            InspectCommand = new RelayCommand(ShowTours);
            BackToComplexToursCommand = new RelayCommand(HideTours);
            TourOfComplexRequestsVisibility = Visibility.Collapsed;



        }

        public void HideTours(object parametar)
        {
            TourOfComplexRequestsVisibility = Visibility.Collapsed;
            ComplexTourRequestsVisibility = Visibility.Visible;
            TourOfComplexRequests.Clear();
            List<ComplexTourRequest> tours = new List<ComplexTourRequest>(complexTourRequestService.GetAllUniqueTourRequests());
            foreach (var tour in tours)
            {
                ComplexTourRequests.Add(tour);
            }
        }
        public void ShowTours(object parameter)
        {
            ComplexTourRequestsVisibility = Visibility.Collapsed;
            TourOfComplexRequestsVisibility = Visibility.Visible;
            ComplexTourRequest complexTourRequest = parameter as ComplexTourRequest;
            ComplexTourRequests.Clear();
            List<ComplexTourRequest> tours = new List<ComplexTourRequest>(complexTourRequestService.GetTourRequest(complexTourRequest.TourId));
            foreach (var tour in tours)
            {
                TourOfComplexRequests.Add(tour);
            }
            
        }

        private Visibility _ComplexTourRequestsVisibility = Visibility.Visible;
        public Visibility ComplexTourRequestsVisibility
        {
            get => _ComplexTourRequestsVisibility;
            set
            {
                _ComplexTourRequestsVisibility = value;
                OnPropertyChanged(nameof(ComplexTourRequestsVisibility));
            }
        }

        private Visibility _TourOfComplexRequestsVisibility = Visibility.Visible;
        public Visibility TourOfComplexRequestsVisibility
        {
            get => _TourOfComplexRequestsVisibility;
            set
            {
                _TourOfComplexRequestsVisibility = value;
                OnPropertyChanged(nameof(TourOfComplexRequestsVisibility));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
