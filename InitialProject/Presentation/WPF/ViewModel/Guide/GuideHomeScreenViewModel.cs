using InitialProject.Aplication.Factory;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class GuideHomeScreenViewModel : INotifyPropertyChanged
    {
        private readonly IGuideStatusService guideStatusService;
        private readonly Window _window;
        private bool _isGuideEmployed;
        private bool _isMenuVisible;
        private Visibility _buttonsVisibility = Visibility.Collapsed;
        private Visibility _gridVisibility = Visibility.Visible;
        private Visibility _burgerMenuVisibility = Visibility.Collapsed;
        private bool _isPopupVisible;
        private string _popupMessage;
        private readonly int GuideId;

        public RelayCommand CheckBoxCommand { get; }
        public RelayCommand FirstQuestionCommand { get; }
        public RelayCommand SecondQuestionCommand { get; }
        public RelayCommand ThirdQuestionCommand { get; }
        public RelayCommand CreateTourCommand { get; }
        public RelayCommand TourRequestCommand { get; }
        public RelayCommand HamburgerCommand { get; }
        public RelayCommand TourStatisticsCommand { get; }
        public RelayCommand ProfileCommand { get; }

        public bool IsGuideEmployed
        {
            get => _isGuideEmployed;
            set
            {
                _isGuideEmployed = value;
                OnPropertyChanged(nameof(IsGuideEmployed));
            }
        }

        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set
            {
                _isMenuVisible = value;
                OnPropertyChanged(nameof(IsMenuVisible));
            }
        }

        public Visibility ButtonsVisibility
        {
            get => _buttonsVisibility;
            set
            {
                _buttonsVisibility = value;
                OnPropertyChanged(nameof(ButtonsVisibility));
            }
        }

        public Visibility GridVisibility
        {
            get => _gridVisibility;
            set
            {
                _gridVisibility = value;
                OnPropertyChanged(nameof(GridVisibility));
            }
        }

        public Visibility BurgerMenuVisibility
        {
            get => _burgerMenuVisibility;
            set
            {
                _burgerMenuVisibility = value;
                OnPropertyChanged(nameof(BurgerMenuVisibility));
            }
        }

        public bool IsPopupVisible
        {
            get => _isPopupVisible;
            set
            {
                _isPopupVisible = value;
                OnPropertyChanged(nameof(IsPopupVisible));
            }
        }

        public string PopupMessage
        {
            get => _popupMessage;
            set
            {
                _popupMessage = value;
                OnPropertyChanged(nameof(PopupMessage));
            }
        }


        public GuideHomeScreenViewModel(Window window, int? guideId)
        {
            _window = window;
            guideStatusService = Injector.CreateInstance<IGuideStatusService>();

            CheckBoxCommand = new RelayCommand(CheckBoxChanged);
            FirstQuestionCommand = new RelayCommand(FirstAnswer);
            SecondQuestionCommand = new RelayCommand(SecondAnswer);
            ThirdQuestionCommand = new RelayCommand(ThirdAnswer);
            CreateTourCommand = new RelayCommand(CreateTourView);
            TourRequestCommand = new RelayCommand(CreateTourRequestView);
            HamburgerCommand = new RelayCommand(ShowHamburgerMenu);
            TourStatisticsCommand = new RelayCommand(ShowStatistics);
            ProfileCommand = new RelayCommand(ShowProfile);

            GuideId = guideId.GetValueOrDefault();

            if (guideStatusService.GetStatusByUserId(GuideId).EmploymentStatus == Domen.Model.GuideStatus.Status.Unemployed)
                IsGuideEmployed = false;
            else
                IsGuideEmployed = true;

            CheckIfGuideIsSuper();
        }

        private void CheckIfGuideIsSuper()
        {
            guideStatusService.EvaluateGuideForSuperStatus(GuideId);
        }

        private void ShowProfile(object ob)
        {
            var profileView = new GuideProfileView(GuideId);
            profileView.Show();
        }

        private void ShowStatistics(object ob)
        {
            var statisticsView = new TourRequestStatisticsView();
            statisticsView.Show();
        }

        private void ShowHamburgerMenu(object ob)
        {
            if (GridVisibility != Visibility.Collapsed)
            {
                GridVisibility = Visibility.Collapsed;
                BurgerMenuVisibility = Visibility.Visible;
            }
            else
            {
                GridVisibility = Visibility.Visible;
                BurgerMenuVisibility = Visibility.Collapsed;
            }
        }

        private void CheckBoxChanged(object isChecked)
        {
            bool IsChecked = (bool)isChecked;
            ButtonsVisibility = IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CreateTourView(object isChecked)
        {
            CreatingTourView tourForm = new CreatingTourView(null, null, null, GuideId);
            tourForm.Show();
            _window.Close();
        }

        private void CreateTourRequestView(object isChecked)
        {
            TourRequestsView tourRequestForm = new TourRequestsView(GuideId);
            tourRequestForm.Show();
            _window.Close();
        }

        private void FirstAnswer(object isChecked)
        {
            PopupMessage = "Temporary information 1";
        }

        private void SecondAnswer(object isChecked)
        {
            ShowInfoPopup("Temporary information 2");
        }

        private void ThirdAnswer(object isChecked)
        {
            ShowInfoPopup("Temporary information 3");
        }

        private void ShowInfoPopup(string message)
        {
            IsPopupVisible = true;

            var timer = new System.Timers.Timer(3000);
            timer.Elapsed += (sender, e) =>
            {
                IsPopupVisible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
