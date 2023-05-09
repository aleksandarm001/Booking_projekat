﻿using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class GuideHomeScreenViewModel : INotifyPropertyChanged
    {
        public RelayCommand CheckBoxCommand { get; }
        public RelayCommand FirstQuestionCommand { get; }
        public RelayCommand SecondQuestionCommand { get; }
        public RelayCommand ThirdQuestionCommand { get; }
        public RelayCommand CreateTourCommand { get; set; }

        public RelayCommand TourRequestCommand { get; set; }

        private System.Timers.Timer popupTimer;

        public Window _window { get; set; }
        public GuideHomeScreenViewModel(Window window)
        {
            CheckBoxCommand = new RelayCommand(CheckBoxChanged);
            FirstQuestionCommand = new RelayCommand(FirstAnswer);
            SecondQuestionCommand = new RelayCommand(SecondAnswer);
            ThirdQuestionCommand = new RelayCommand(ThirdAnswer);
            CreateTourCommand = new RelayCommand(CreateTourView);
            TourRequestCommand = new RelayCommand(CreateTourRequestView);
            _window = window;
        }

        private Visibility _buttonsVisibility = Visibility.Collapsed;
        public Visibility ButtonsVisibility
        {
            get => _buttonsVisibility;
            set
            {
                _buttonsVisibility = value;
                OnPropertyChanged(nameof(ButtonsVisibility));
            }
        }

        private bool isPopupVisible;
        public bool IsPopupVisible
        {
            get { return isPopupVisible; }
            set
            {
                isPopupVisible = value;
                OnPropertyChanged(nameof(IsPopupVisible));
            }
        }

        private string popupMessage;
        public string PopupMessage
        {
            get { return popupMessage; }
            set
            {
                popupMessage = value;
                OnPropertyChanged(nameof(PopupMessage));
            }
        }


        private void ShowInfoPopup(string message)
        {
            IsPopupVisible = true;

            // Close the Popup after a certain duration (e.g., 3 seconds)
            var timer = new System.Timers.Timer(3000);
            timer.Elapsed += (sender, e) =>
            {
                IsPopupVisible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }


        private void CheckBoxChanged(object isChecked)
        {
            bool IsChecked = (bool)isChecked;
            // Update the visibility of the buttons based on the CheckBox's state
            ButtonsVisibility = IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }
        private void CreateTourView(object isChecked)
        {
            CreatingTourView tourForm = new CreatingTourView();
            tourForm.Show();
            _window.Close();
        }

        private void CreateTourRequestView(object isChecked)
        {
            TourRequestsView tourRequestForm = new TourRequestsView();
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        
    }
}
