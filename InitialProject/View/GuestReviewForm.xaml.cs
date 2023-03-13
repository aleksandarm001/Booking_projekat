using InitialProject.Model;
using InitialProject.Repository;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestReviewForm.xaml
    /// </summary>
    public partial class GuestReviewForm : Window
    {
        private readonly GuestReviewRepository _guestReviewRepository;

        private int _hygieneGrade;

        private int _ruleFollowingGrade;

        private string _ownerComment;


        public string OwnerComment
        {
            get => _ownerComment;
            set
            {
                if (value != _ownerComment)
                {
                    _ownerComment = value;
                    OnPropertyChanged();
                }
            }
        }

        public int HygieneGrade
        {
            get => _hygieneGrade;
            set
            {
                if(value != _hygieneGrade)
                {
                    _hygieneGrade = value;
                    OnPropertyChanged();
                }
            }
        }

        public int RuleFollowingGrade
        {
            get => _ruleFollowingGrade;
            set
            {
                if(value!= _ruleFollowingGrade)
                {
                    _ruleFollowingGrade = value;
                    OnPropertyChanged();
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public GuestReviewForm()
        {
            InitializeComponent();
            DataContext= this;

            _guestReviewRepository= new GuestReviewRepository();
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {
            GuestReview newGuestReview = new GuestReview();
            newGuestReview.Hygiene = HygieneGrade;
            newGuestReview.RuleFollowing= RuleFollowingGrade;
            newGuestReview.Comment = OwnerComment;

            _guestReviewRepository.Save(newGuestReview);
            Close();

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
