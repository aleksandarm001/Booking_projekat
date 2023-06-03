using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
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

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class CloseForumWindowViewModel : INotifyPropertyChanged
    {
        private IForumService _forumService;
        private IUserService _userService;
        private int _selectedForumId;
        private bool _canCancelForum;
        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand CloseForumCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public int SelectedForumId
        {
            get => _selectedForumId;
            set
            {
                if (_selectedForumId != value)
                {
                    _selectedForumId = value;
                    CanCancelForum = true;
                    OnPropertyChanged(nameof(SelectedForumId));
                    OnPropertyChanged(nameof(CanCancelForum));
                }
            }
        }
        public bool CanCancelForum
        {
            get => _canCancelForum;
            set
            {
                if (_canCancelForum != value)
                {
                    _canCancelForum = value;
                    OnPropertyChanged(nameof(CanCancelForum));
                }
            }
        }
        public ObservableCollection<KeyValuePair<int, string>> Forums { get; set; }
        public CloseForumWindowViewModel()
        {
            InitializeServices();
            InitializeUserForums();
            InitializeCommands();
            CanCancelForum = false;

        }

        private void InitializeCommands()
        {
            CloseForumCommand = new RelayCommand(CloseForum, CanExecuteCommand);
            CancelCommand = new RelayCommand(Cancel);
        }
        private bool CanExecuteCommand(object parameter)
        {
            return CanCancelForum;
        }
        private void InitializeServices()
        {
            _forumService = Injector.CreateInstance<IForumService>();
            _userService = Injector.CreateInstance<IUserService>();
        }

        private void InitializeUserForums()
        {
            int userId = _userService.GetUserId();
            Forums = new ObservableCollection<KeyValuePair<int, string>>(_forumService.GetForumsByUserKeyValue(userId));
        }
        private void CloseForum(object parameter)
        {
            Forum forum = _forumService.GetForumById(SelectedForumId);
            forum.Status = ForumStatus.Closed;
            _forumService.Update(forum);
            CloseWindow();
        }
        private void Cancel(object parameter)
        {
            if(parameter is Window window)
            {
                window.Close();
            }
        }
        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<CloseForumWindow>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}