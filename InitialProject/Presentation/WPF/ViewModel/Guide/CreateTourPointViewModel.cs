using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class CreateTourPointViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourPoint> TourPoints { get; set; }
        public ICommand EditCommand { get; set; }

        TourPoint tourPoint = new TourPoint()
        {
            Name = "antonije1",
            Description = "opis"
        };

        TourPoint tourPoint1 = new TourPoint()
        {
            Name = "antonije2",
            Description = "opis"
        };

        TourPoint tourPoint2 = new TourPoint()
        {
            Name = "antonije3",
            Description = "opis"
        };

        TourPoint tourPoint3 = new TourPoint()
        {
            Name = "antonije4",
            Description = "opis"
        };

        public CreateTourPointViewModel()
        {
            EditCommand = new RelayCommand(edit);
            TourPoints = new ObservableCollection<TourPoint>();
            TourPoints.Add(tourPoint);
            TourPoints.Add(tourPoint1);
            TourPoints.Add(tourPoint2);
            TourPoints.Add(tourPoint3);

        }

        private TourPoint _selectedTourPoint;
        public TourPoint SelectedTourPoint
        {
            get { return _selectedTourPoint; }
            set
            {
                _selectedTourPoint = value;
                OnPropertyChanged(nameof(_selectedTourPoint));
            }
        }

        public void edit(object obj)
        {
            TourPoint selectedTourPoint = (TourPoint)obj;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
