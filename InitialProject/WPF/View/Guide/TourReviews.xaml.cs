﻿using Eco.ViewModel.Runtime;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace InitialProject.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for TourReviews.xaml
    /// </summary>
    public partial class TourReviews : Window
    {
        private ReviewCommentsViewModel commentsViewModel;
        public TourReviews()
        {
            commentsViewModel = new ReviewCommentsViewModel();
            DataContext = commentsViewModel;
            InitializeComponent();

        }
        private void ShowStatisticButton(object sender, RoutedEventArgs e)
        {
            
        }
        private void DeleteFakeCommentButton(object sender, RoutedEventArgs e)
        {
           
        }
        
    }
}
