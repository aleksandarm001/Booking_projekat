using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Owner;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerStartWindow.xaml
    /// </summary>
    public partial class OwnerStartWindow : Window
    {
        private OwnerStartViewModel viewModel { get; set; }

        public OwnerStartWindow(int UserId)
        {
            InitializeComponent();
            this.viewModel = new OwnerStartViewModel(this.frame.NavigationService,this.sideBarFrame.NavigationService, UserId);
            DataContext = this.viewModel;
        }

        


    }
}
