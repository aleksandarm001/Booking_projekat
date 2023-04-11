using InitialProject.Factory;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class ReviewCommentsViewModel
    {
        private readonly ITourService _tourService;

        public ObservableCollection<string> Tours { get; set; }
        public ReviewCommentsViewModel()
        {
            _tourService = Injector.tourService();
            LoadTours();
        }

        private void LoadTours()
        {
            Tours = new ObservableCollection<string>(_tourService.GetAllFinishedTours().Select(c => c.TourId + " " + c.Name));
        }
    }
}
