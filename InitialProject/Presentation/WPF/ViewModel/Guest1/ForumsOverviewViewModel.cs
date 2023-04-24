﻿using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Services;
using InitialProject.Services.IServices;
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
    public class ForumsOverviewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ForumOverviewDTO> _myForums;
        private ObservableCollection<ForumOverviewDTO> _allForums;
        private ForumOverviewDTO _selectedMyForum;
        private ForumOverviewDTO _selectedForum;
        private readonly IForumService _forumService;
        private readonly IForumCommentService _forumCommentService;
        private readonly IUserService _userService;
        public RelayCommand CreateForumCommand { get; set; }
        public RelayCommand CloseForumCommand { get; set; }
        public RelayCommand OpenForumCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<ForumOverviewDTO> MyForums
        {
            get => _myForums;
            set
            {
                if (value != _myForums)
                {
                    _myForums = value;
                    OnPropertyChanged("MyForums");
                }
            }
        }
        public ObservableCollection<ForumOverviewDTO> AllForums
        {
            get => _allForums;
            set
            {
                if (value != _allForums)
                {
                    _allForums = value;
                    OnPropertyChanged("AllForums");
                }
            }
        }
        public ForumOverviewDTO SelectedMyForum
        {
            get => _selectedMyForum;
            set
            {
                if (value != _selectedMyForum)
                {
                    _selectedMyForum = value;
                    OnPropertyChanged("SelectedMyForum");
                }
            }
        }
        public ForumOverviewDTO SelectedForum
        {
            get => _selectedForum;
            set
            {
                if (value != _selectedForum)
                {
                    _selectedForum = value;
                    OnPropertyChanged("SelectedForum");
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ForumsOverviewViewModel()
        {
            _forumService = Injector.CreateInstance<IForumService>();
            _forumCommentService = Injector.CreateInstance<IForumCommentService>();
            _userService = Injector.CreateInstance<IUserService>();
            CreateForumCommand = new RelayCommand(OpenCreateForumForm);
            CloseForumCommand = new RelayCommand(CloseForum);
            OpenForumCommand = new RelayCommand(OpenForum);
            MyForums = new ObservableCollection<ForumOverviewDTO>();
            AllForums = new ObservableCollection<ForumOverviewDTO>();
            InitializeMyForums();
            InitializeAllForums();
        }
        private void OpenCreateForumForm(object parameter)
        {
            CreatingForumForm creatingForumForm = new CreatingForumForm();
            creatingForumForm.Owner = App.Current.MainWindow;
            creatingForumForm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            creatingForumForm.ShowDialog();
        }
        private void InitializeMyForums()
        {
            List<Forum> myForums = _forumService.GetForumsByCreatorId(_userService.GetUserId());
            string useful;
            MyForums.Clear();
            foreach(Forum myForum in myForums)
            {
                useful = CheckUseful(myForum);
                int commentsNumber = _forumCommentService.GetCommentsNumberByForum(myForum.ForumId);
                ForumOverviewDTO forum = new ForumOverviewDTO(myForum.ForumId,myForum.ForumTopic, myForum.Location, myForum.DateCreated, commentsNumber, myForum.Status, useful);
                MyForums.Add(forum);
            }
        }
        private void InitializeAllForums()
        {
            List<Forum> forums = _forumService.GetAll();
            string useful;
            AllForums.Clear();
            foreach (Forum f in forums)
            {
                useful = CheckUseful(f);
                int commentsNumber = _forumCommentService.GetCommentsNumberByForum(f.ForumId);
                ForumOverviewDTO forum = new ForumOverviewDTO(f.ForumId, f.ForumTopic, f.Location, f.DateCreated, commentsNumber, f.Status, useful);
                AllForums.Add(forum);
            }
        }
        private string CheckUseful(Forum myForum)
        {
            if (myForum.VeryUseful)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
        private void CloseForum(object parameter)
        {

        }
        private void OpenForum(object parameter)
        {

        }
    }
}
