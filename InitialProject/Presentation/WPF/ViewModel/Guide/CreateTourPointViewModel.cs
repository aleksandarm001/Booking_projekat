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

        public ICommand DeleteCommand { get; set; }
        public ICommand AddTourPointCommand { get; set; }

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
            DeleteCommand = new RelayCommand(delete);
            AddTourPointCommand = new RelayCommand(addTourPoint);
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

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(_Description));
            }
        }

        public void edit(object obj)
        {
            TourPoint selectedTourPoint = (TourPoint)obj;

        }

        public void delete(object obj)
        {
            TourPoint selectedTourPoint = (TourPoint)obj;
            TourPoints.Remove(selectedTourPoint);
        }

        public void addTourPoint(object obj) 
        {
            TourPoint tourPoint = new TourPoint();
            tourPoint.Name = Name;
            tourPoint.Description = Description;
            TourPoints.Add(tourPoint);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
