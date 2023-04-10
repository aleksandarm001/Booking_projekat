using InitialProject.Model;
using InitialProject.View;
using System;
using System.Collections.Generic;
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
using InitialProject.CustomClasses;
using InitialProject.Repository;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window, INotifyPropertyChanged
    {
        private UserRepository _userRepository;
        public string NameOfUser { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private string _strTypeOfUser;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string StrTypeOfUser
        {
            get => _strTypeOfUser;
            set
            {
                if (value != _strTypeOfUser)
                {
                    try
                    {
                        TypeOfUser = (UserType)Enum.Parse(typeof(UserType), StrTypeOfUser);
                    }
                    catch (Exception) { }
                    _strTypeOfUser = value;
                    OnPropertyChanged();
                }
            }
        }
        public UserType TypeOfUser { get; set; }

        public UserRegistration()
        {
            InitializeComponent();
            DataContext = this;
            _userRepository = new UserRepository();

        }
        private void OpenLogInForm(object sender, RoutedEventArgs e)
        {

        }
        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(NameOfUser, Username, Email, Password, TypeOfUser);
            _userRepository.Save(user);

            
        }
        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
