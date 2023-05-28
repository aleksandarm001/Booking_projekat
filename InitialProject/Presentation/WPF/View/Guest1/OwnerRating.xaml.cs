using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel;
using InitialProject.Presentation.WPF.ViewModel.Guest1;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for OwnerRating.xaml
    /// </summary>
    public partial class OwnerRating : Window, INotifyPropertyChanged
    {
        public event EventHandler FieldsUpdated;
        private RelayCommand _submitReview_command;
        public RelayCommand SubmitReview_Command
        {
            get
            {
                if (_submitReview_command == null)
                {
                    _submitReview_command = new RelayCommand(SubmitReview, CanSubmitReview);
                }
                return _submitReview_command;
            }
        }
        private bool _isEnabled;
        public bool CanExecute
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = !string.IsNullOrEmpty(StrCorrectness) && !string.IsNullOrEmpty(StrCleanliness);
                OnPropertyChanged(nameof(CanExecute));
            }
        }
        public RelayCommand Cancel_Command { get; set; }
        public List<OwnerToRate> ownersToRate;
        public ObservableCollection<KeyValuePair<int, string>> AccommodationsName { get; set; }
        private  OwnerToRateService ownerToRateService;
        private  OwnerRateService ownerRateService;
        private  AccommodationService accommodationService;
        public List<string> NekaLista;
        public List<string> Grades { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private int _selectedAccommodationId;
        public int SelectedAccommodationId
        {
            get => _selectedAccommodationId;
            set
            {
                if(value != _selectedAccommodationId)
                {
                    _selectedAccommodationId = value;
                    OnPropertyChanged();
                    NotifyFieldsUpdated();
                }
            }
        }

        public List<string> Images { get; set; }
        public int Cleanliness { get; set; }
        private string _strCleanliness;
        public string StrCleanliness
        {
            get => _strCleanliness;
            set
            {
                if (value != _strCleanliness)
                {
                    try
                    {
                        int _cleanliness;
                        int.TryParse(value, out _cleanliness);
                        Cleanliness = _cleanliness;
                    }
                    catch (Exception) { }
                    _strCleanliness = value;
                    OnPropertyChanged();
                    NotifyFieldsUpdated();
                }
            }
        }
        public int Correctness { get; set; }
        private string _strCorrectness;
        public string StrCorrectness
        {
            get => _strCorrectness;
            set
            {
                if (value != _strCorrectness)
                {
                    try
                    {
                        int _correctness;
                        int.TryParse(value, out _correctness);
                        Correctness = _correctness;
                    }
                    catch (Exception) { }
                    _strCorrectness = value;
                    OnPropertyChanged();
                    NotifyFieldsUpdated();
                }
            }
        }
        public string AdditionalComment { get; set; }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;         
            }
        }
        private readonly int _userId;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public OwnerRating(int userId)
        {
            InitializeComponent();
            _userId = userId;
            DataContext = this;
            cmbx.Focus();
            InitializeGrades();
            InitializeServices();
            InitializeCollections(userId);
            InitializeCommands();
        }
        private void NotifyFieldsUpdated()
        {
            FieldsUpdated?.Invoke(this, EventArgs.Empty);
        }
        private void InitializeCommands()
        {
            Cancel_Command = new RelayCommand(Cancel);
        }

        private void InitializeCollections(int userId)
        {
            Images = new List<string>();
            AccommodationsName = new ObservableCollection<KeyValuePair<int, string>>(ownerToRateService.GetAccommodationNamesByUser(userId));
        }

        private void InitializeServices()
        {
            ownerToRateService = new OwnerToRateService();
            ownerRateService = new OwnerRateService();
            accommodationService = new AccommodationService();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            if (!Images.Contains(Image))
            {
                Images.Add(Image);
            }
        }
        private void SubmitReview(object parameter)
        {
            ownerToRateService.DeleteOwnerToRate(SelectedAccommodationId);
            int ownerId = accommodationService.GetOwnerIdByAccommodationId(SelectedAccommodationId);
            OwnerRate ownerRate = new OwnerRate(_userId, ownerId, SelectedAccommodationId, Cleanliness, Correctness, AdditionalComment, Images);
            ownerRateService.SaveRate(ownerRate);
            AskRenovationRecommendation();
        }
        private bool CanSubmitReview(object parameter)
        {
            return CanExecute;
        }
        private void Cancel(object parameter)
        {
            this.Close();
        }
        private void InitializeGrades()
        {
            Grades = new List<string>();
            for(int i = 1; i < 6; i++)
            {
                Grades.Add(i.ToString());
            }
        }
        private void AskRenovationRecommendation()
        {
            if (MessageBox.Show("Do you want to left reccommoendation?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
                RenovationRecommendationForm renovationRecommendation = new RenovationRecommendationForm(SelectedAccommodationId);
                renovationRecommendation.Owner = App.Current.MainWindow;
                renovationRecommendation.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                renovationRecommendation.ShowDialog();
            }
            else
            {
                this.Close();
            }
        }
    }
}
