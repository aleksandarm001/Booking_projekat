using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;
using System.Linq;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class ComplexTourRequestsViewModel
    {
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        private readonly IComplexTourRequestService _complexTourRequestService;
        public string TourName { get; set; }
        public ComplexTourRequestsViewModel(int tourId)
        {
            _complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestService.GetTourRequest(tourId));
            TourName = ComplexTourRequests.ElementAt(0).TourName;
        }
    }
}
