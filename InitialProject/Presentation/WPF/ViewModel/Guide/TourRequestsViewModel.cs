using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class TourRequestsViewModel : INotifyPropertyChanged
    {
        private readonly ITourRequestService tourRequestService;
        private readonly int guideId;

        public ICommand ApproveCommand { get; set; }
        public ICommand DeclineCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ComplexRequestCommand { get; set; }

        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public TourRequest SelectedTourRequest { get; set; }
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TourRequestsViewModel(int? guideId)
        {
            ApproveCommand = new RelayCommand(ApproveTourRequest);
            DeclineCommand = new RelayCommand(DeclineTourRequest);
            FilterCommand = new RelayCommand(FilterTourRequest);
            ComplexRequestCommand = new RelayCommand(ComplexRequest);

            tourRequestService = Injector.CreateInstance<ITourRequestService>();

            TourRequests = new ObservableCollection<TourRequest>();

            var tourRequestsList = tourRequestService.GetAllRequests();

            this.guideId = guideId ?? 0;

            foreach (var tourRequest in tourRequestsList.Where(tr => tr.RequestStatus == ComplexTourRequest.Status.OnHold))
            {
                TourRequests.Add(tourRequest);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ApproveTourRequest(object obj)
        {
            TourRequest selectedTourRequest = (TourRequest)obj;
            CreationType creationType = new CreationType
            {
                Type = CreationType.CreationTourType.CreatedByRequest
            };

            CreatingTourView creatingTourView = new CreatingTourView(selectedTourRequest, creationType, null, guideId);
            TourRequests.Remove(selectedTourRequest);
            creatingTourView.ShowDialog();
        }

        private void FilterTourRequest(object obj)
        {
            FilterTourRequestView T = new FilterTourRequestView(TourRequests);
            T.Show();
        }

        private void DeclineTourRequest(object obj)
        {
            TourRequest selectedTourRequest = (TourRequest)obj;
            selectedTourRequest.RequestStatus = ComplexTourRequest.Status.Rejected;
            tourRequestService.Update(selectedTourRequest);
            TourRequests.Remove(selectedTourRequest);
        }

        private void ComplexRequest(object obj)
        {
            ComplexTourRequestsView T = new ComplexTourRequestsView(guideId);
            T.Show();
        }
    }
}
