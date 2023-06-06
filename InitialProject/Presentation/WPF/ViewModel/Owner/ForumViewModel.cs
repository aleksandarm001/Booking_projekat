using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class ForumViewModel :INotifyPropertyChanged
    {
        private readonly IForumService _forumService;

        public ObservableCollection<Forum> _forums;
        public ObservableCollection<Forum> Forums
        {
            get { return _forums; }
            set
            {
                _forums = value;
                OnPropertyChanged(nameof(Forums));
            }
        }

        int UserId;
        public Forum SelectedForum { get; set; }

        public RelayCommand ForumComments { get; set; }

        public ForumViewModel(int userId) 
        {
            _forumService = Injector.CreateInstance<IForumService>();
            UserId = userId;

            Forums = new ObservableCollection<Forum>(_forumService.OpenedForums(UserId));

            ForumComments = new RelayCommand(ForumComments_ButtonClick);
            
        }


        public void ForumComments_ButtonClick(object obj)
        {
            ForumCommentsWindow comments = new ForumCommentsWindow(UserId,SelectedForum);
            comments.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
