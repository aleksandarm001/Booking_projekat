using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class ForumCommentsViewMode : INotifyPropertyChanged
    {

        private readonly IForumCommentService _forumCommentService;
        private readonly ICommentService _commentService;
        private readonly IForumUtilityService _forumUtility;
        private readonly IUserService _userService;


        public ObservableCollection<Comment> _comments;
        public ObservableCollection<Comment> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));

            }
        }

        public string _commentText;
        public string CommentText
        {
            get => _commentText;
            set
            {
                if (value != _commentText)
                {
                    _commentText = value;
                    OnPropertyChanged("CommentText");
                }
            }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }


        private Visibility _imageVisibility;
        public Visibility ImageVisibility
        {
            get { return _imageVisibility; }
            set
            {
                _imageVisibility = value;
                OnPropertyChanged(nameof(ImageVisibility));
            }
        }


        public Forum selectedForum { get; set; }
        public Comment SelectedComment { get; set; }
        int UserId;
        string isUsefull;

       

        public RelayCommand AddComment { get; set; }
        public RelayCommand ReportCommand { get; set; }

        public ForumCommentsViewMode(int userId , Forum forum) 
        {
            selectedForum = forum;    
            UserId= userId;
            ImageSource = new BitmapImage(new Uri("/Infrastructure/Resources/Images/Zvjezda.png", UriKind.Relative));
            ImageVisibility = Visibility.Collapsed;
            


            _forumUtility = Injector.CreateInstance<IForumUtilityService>();
            _forumCommentService = Injector.CreateInstance<IForumCommentService>();
            _commentService = Injector.CreateInstance<ICommentService>();
            _userService = Injector.CreateInstance<IUserService>();

            isUsefull = _forumUtility.CheckUseful(selectedForum);
            CheckUsefulness(isUsefull);


            Comments = new ObservableCollection<Comment>(_commentService.CommentsByForumId(forum.ForumId));
            


            AddComment = new RelayCommand(AddComment_ButtonClick);
            ReportCommand = new RelayCommand(ReportComment_ButtonClick);

        }



        public void CheckUsefulness(string usefull)
        {
            if (usefull == "Yes")
            {
                ImageVisibility = Visibility.Visible;
            }
            
        }
        
        
        public void AddComment_ButtonClick(object obj)
        {
           Comment newComment = _commentService.CreateOwnerComment(CommentText, UserId);
           _commentService.Save(newComment);
            Comments.Add(newComment);
           ForumComment newForumComment = new ForumComment(selectedForum.ForumId, newComment.CommentId);
           _forumCommentService.Save(newForumComment);
            isUsefull = _forumUtility.CheckUseful(selectedForum);
            CheckUsefulness(isUsefull);

        }

        public void ReportComment_ButtonClick(object obj)
        {
            if (SelectedComment == null)
            {
                MessageBox.Show("You need to select a comment first", "Message");
            }
            else
            {
                SelectedComment.Reports++;
                _commentService.Update(SelectedComment);
            }
        }
        
        public Boolean wasOnLocation(Comment comment,Forum forum)
        {
            int userId = comment.UserId;
            User user = _userService.GetById(userId);
            if (_forumUtility.WasOnLocation(user, forum))
            {
                return true;
            }
            return false;
        }
        



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
