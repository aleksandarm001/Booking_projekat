using InitialProject.Aplication.Factory;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class CreatingForumViewModel : INotifyPropertyChanged
    {
        private string _selectedCountry;
        private string _selectedCity;
        private string _text;
        private ObservableCollection<string> _countries;
        private ObservableCollection<string> _cities;
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ILocationService _locationService;
        private readonly IUserService _userService;
        public RelayCommand CreateForumCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public ObservableCollection<string> Countries
        {
            get => _countries;
            set
            {
                if(value != _countries)
                {
                    _countries = value;
                    OnPropertyChanged("Countries");
                }
            }
        }
        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
                }
            }
        }
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (value != _selectedCountry)
                {
                    _selectedCountry = value;
                    Cities = new ObservableCollection<string>(_locationService.GetCitiesByCountry(_selectedCountry));
                    OnPropertyChanged("Cities");
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (value != _selectedCity)
                {
                    _selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                }
            }
        }
        public string Text
        {
            get => _text;
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CreatingForumViewModel()
        {
            _locationService = Injector.CreateInstance<ILocationService>();
            _userService = Injector.CreateInstance<IUserService>();
            CreateForumCommand = new RelayCommand(CreateForum);
            CancelCommand = new RelayCommand(Close);
            Countries = new ObservableCollection<string>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(_locationService.GetAllCities());
        }
        private void CreateForum(object parameter)
        {

        }
        private void Close(object parameter)
        {

        }
    }
}
