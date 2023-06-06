using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for ForumCommentsWindow.xaml
    /// </summary>
    public partial class ForumCommentsWindow : Window
    {
        private ForumCommentsViewMode viewModel { get; set; }
        public ForumCommentsWindow(int userId, Forum forum)
        {
            InitializeComponent();
            viewModel = new ForumCommentsViewMode(userId, forum);
            this.DataContext = viewModel;
        }

        
    }
}
