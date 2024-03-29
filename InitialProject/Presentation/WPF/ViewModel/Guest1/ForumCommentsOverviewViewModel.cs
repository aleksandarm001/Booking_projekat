﻿using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class ForumCommentsOverviewViewModel : INotifyPropertyChanged
    {
        private bool _canLeaveComment;
        private string _topic;
        private string _location;
        private string _newComment;
        private ObservableCollection<CommentDTO> _comments;
        public event PropertyChangedEventHandler? PropertyChanged;
        private IForumCommentService _forumCommentService;
        private IForumIdService _forumIdService;
        private IForumService _forumService;
        private IUserService _userService;
        private ICommentService _commentService;
        private IAccommodationReservationService _accommodationReservationService;
        public RelayCommand SubmitCommentCommand { get; set; }
        public RelayCommand FocusComments_Command { get; set; }
        public RelayCommand FocusTextBox_Command { get; set; }
        public ObservableCollection<CommentDTO> Comments
        {
            get => _comments;
            set
            {
                if (value != _comments)
                {
                    _comments = value;
                    OnPropertyChanged("Comments");
                }
            }
        }
        public bool CanLeaveComment
        {
            get => _canLeaveComment;
            set
            {
                if (_canLeaveComment != value)
                {
                    _canLeaveComment = value;
                    SubmitCommentCommand = new RelayCommand(SubmitComment);
                    OnPropertyChanged(nameof(CanLeaveComment));
                }
            }
        }
        public string Topic
        {
            get => _topic;
            set
            {
                if (_topic != value)
                {
                    _topic = value;
                    OnPropertyChanged(nameof(Topic));
                }
            }
        }
        public string Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        public string NewComment
        {
            get => _newComment;
            set
            {
                if (_newComment != value)
                {
                    _newComment = value;
                    OnPropertyChanged(nameof(NewComment));
                }
            }
        }
        public ForumCommentsOverviewViewModel()
        {
            Comments = new ObservableCollection<CommentDTO>();
            InitializeServices();
            InitializeForumComments();
            InitializeTopic();
            InitializeLocation();
            InitializeCanLeaveComment();
            InitializeCommands();
        }
        private void InitializeForumComments()
        {
            Comments.Clear();
            List<int> commentIds = _forumCommentService.GetCommentsIdByForumId(_forumIdService.ForumId);
            foreach (int commentId in commentIds)
            {
                Comment com = _commentService.GetByCommentId(commentId);
                User user = _userService.GetById(com.UserId);
                bool highlight = CheckForHighlight(user);
                CommentDTO commentDTO = new CommentDTO(user.Username, com.Text, com.CreationTime, highlight);
                Comments.Add(commentDTO);
            }
            Comments = new ObservableCollection<CommentDTO>(Comments.OrderByDescending(c => c.PostedDate).ToList());
        }
        private void InitializeTopic()
        {
            Topic = _forumService.GetTopic(_forumIdService.ForumId);
        }
        private void InitializeLocation()
        {
            Location location = _forumService.GetLocation(_forumIdService.ForumId);
            Location = location.City + ", " + location.Country;
        }
        private void InitializeServices()
        {
            _forumService = Injector.CreateInstance<IForumService>();
            _forumCommentService = Injector.CreateInstance<IForumCommentService>();
            _userService = Injector.CreateInstance<IUserService>();
            _commentService = Injector.CreateInstance<ICommentService>();
            _forumIdService = Injector.CreateInstance<IForumIdService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
        }

        private void InitializeCommands()
        {
            SubmitCommentCommand = new RelayCommand(SubmitComment);
            FocusComments_Command = new RelayCommand(FocusComments);
            FocusTextBox_Command = new RelayCommand(FocusTextBox);
        }

        private void FocusComments(object parameter)
        {
            var listBox = parameter as ListBox;
            listBox.Focus();
            listBox.SelectedItem = listBox.Items[0];
            listBox.ScrollIntoView(listBox.SelectedItem);
        }
        private void FocusTextBox(object parameter)
        {
            var textBox = parameter as TextBox;
            textBox.Focus();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SubmitComment(object parameter)
        {
            if (!string.IsNullOrEmpty(NewComment) && CanLeaveComment == true)
            {
                Comment newComment = new Comment(DateTime.Now, NewComment, _userService.GetUserId());
                newComment = _commentService.Save(newComment);
                ForumComment forumComment = new ForumComment(_forumIdService.ForumId, newComment.CommentId);
                _forumCommentService.Save(forumComment);
                NewComment = "";
                InitializeForumComments();
            }
        }
        private bool CheckForHighlight(User user)
        {
            Location location = _forumService.GetLocation(_forumIdService.ForumId);
            return _accommodationReservationService.WasOnLocation(user.Id, location);
        }
        private void InitializeCanLeaveComment()
        {
            Forum forum = _forumService.GetForumById(_forumIdService.ForumId);
            CanLeaveComment = forum.Status == ForumStatus.Open;
        }
    }
}
