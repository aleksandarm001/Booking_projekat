﻿using System;
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

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    /// <summary>
    /// Interaction logic for RenovationRecommendation.xaml
    /// </summary>
    public partial class RenovationRecommendationForm : Window
    {
        private readonly RenovationRecommendationViewModel viewModel;
        public RenovationRecommendationForm()
        {
            InitializeComponent();
            viewModel = new RenovationRecommendationViewModel(1);
            DataContext = viewModel;
        }
    }
}
