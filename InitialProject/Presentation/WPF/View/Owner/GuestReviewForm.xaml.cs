﻿using InitialProject.Domen.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestReviewForm.xaml
    /// </summary>
    public partial class GuestReviewForm : Window
    {
        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly UserToReviewRepository _userToReviewRepository;
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }

        private int _hygieneGrade;

        private int _ruleFollowingGrade;

        private string _ownerComment;

        private bool _isReviewd;

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

        public bool IsReviewd { get => _isReviewd; set => _isReviewd = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public GuestReviewForm(int guestId,int accommodationId)
        {
            InitializeComponent();
            DataContext = this;
            _userToReviewRepository = new UserToReviewRepository();
            IsReviewd = false;
            _guestReviewRepository = new GuestReviewRepository();
            GuestId = guestId;
            AccommodationId= accommodationId;
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {
            GuestReview newGuestReview = CreateReview();
            _guestReviewRepository.Save(newGuestReview);
            IsReviewd = true;
            Close();

        }

        private GuestReview CreateReview()
        {
            return new GuestReview
            {
                GuestId= GuestId,
                AccommodationId= AccommodationId,
                Hygiene= HygieneGrade,
                RuleFollowing= RuleFollowingGrade,
                Comment= OwnerComment
            };
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}