using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using InitialProject.View.Guest1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class CreatingForumViewModel : INotifyPropertyChanged
    {
        private string _selectedCountry;
        private string _selectedCity;
        private string _text;
        private bool _canCreate;
        private ObservableCollection<string> _countries;
        private ObservableCollection<string> _cities;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ILocationService _locationService;
        private IUserService _userService;
        private IForumCommentService _forumCommentService;
        private IForumService _forumService;
        private ICommentRepository _commentRepository; 
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
                    OnPropertyChanged(nameof(Countries));
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
                    OnPropertyChanged(nameof(Cities));
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
                    Cities.Clear();
                    Cities = new ObservableCollection<string>(_locationService.GetCitiesByCountry(_selectedCountry));
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                    CheckIfCanSubmit();
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
                    OnPropertyChanged(nameof(SelectedCity));
                    CheckIfCanSubmit();
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
                    OnPropertyChanged(nameof(Text));
                    CheckIfCanSubmit();
                }
            }
        }
        public bool CanCreate
        {
            get
            {
                return _canCreate;
            }
            set
            {
                if (value != _canCreate)
                {
                    _canCreate = value;
                    OnPropertyChanged(nameof(CanCreate));
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CreatingForumViewModel()
        {
            InitializeServices();
            InitializeCollections();
            CanCreate = false;
            InitializeCommands();
        }

        private void InitializeCollections()
        {
            Countries = new ObservableCollection<string>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(_locationService.GetAllCities());
        }

        private void InitializeServices()
        {
            _locationService = Injector.CreateInstance<ILocationService>();
            _userService = Injector.CreateInstance<IUserService>();
            _forumService = Injector.CreateInstance<IForumService>();
            _forumCommentService = Injector.CreateInstance<IForumCommentService>();
            _commentRepository = Injector.CreateInstance<ICommentRepository>();
        }

        private void InitializeCommands()
        {
            CreateForumCommand = new RelayCommand(CreateForum, CanCreateForum);
            CancelCommand = new RelayCommand(Close);
        }

        private void CreateForum(object parameter)
        {
            int userId = _userService.GetUserId();
            Location location = new Location(SelectedCity, SelectedCountry);
            Comment comment = CreateComment(userId);
            Forum forum = CreateForum(userId, location);
            ForumComment forumComment = CreateForumComment(forum.ForumId, comment.CommentId);
            ShowSuccessMessage();
            CloseWindow();
        }
        private Comment CreateComment(int userId)
        {
            Comment comment = new Comment(DateTime.Now, Text, userId);
            return _commentRepository.Save(comment);
        }
        private Forum CreateForum(int userId, Location location)
        {
            Forum forum = new Forum(userId, Text, location, DateTime.Now);
            return _forumService.Save(forum);
        }
        private ForumComment CreateForumComment(int forumId, int commentId)
        {
            ForumComment forumComment = new ForumComment(forumId, commentId);
            return _forumCommentService.Save(forumComment);
        }
        private void ShowSuccessMessage()
        {
            MessageBox.Show("You successfully created forum!");
        }
        private void Close(object parameter)
        {
            if(parameter is Window window){
                window.Close();
            }
        }
        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<CreatingForumForm>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
        private void CheckIfCanSubmit()
        {
            CanCreate = (!string.IsNullOrEmpty(SelectedCountry) && !string.IsNullOrEmpty(SelectedCity) && !string.IsNullOrEmpty(Text));
        }
        private bool CanCreateForum(object parameter)
        {
            return CanCreate;
        }
    }
}
