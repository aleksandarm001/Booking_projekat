using InitialProject.Factory;
using InitialProject.IRepository;
using InitialProject.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class TourStatisticsViewModel : INotifyPropertyChanged
    {
        private readonly ITourStatisticsService _tourStatisticsService;
        public ObservableCollection<string> Years { get; set; }
        //public Label MostVisitedTour { get; set; }
        public TourStatisticsViewModel()
        {
            _tourStatisticsService = Injector.tourStatisticsService();
            Years = new ObservableCollection<string>(_tourStatisticsService.GetAllYears());
            //MostVisitedTour = new Label();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _Year;
        public string Year
        {
            get => _Year;
            set
            {
                if (_Year != value)
                {
                    _Year = value; 
                }
                
            }
            
        }

        public Tour GetMostVisitedTour(string year)
        {
            return _tourStatisticsService.GetMostVisitedTour(Year);
        }


    }

    

    
}
