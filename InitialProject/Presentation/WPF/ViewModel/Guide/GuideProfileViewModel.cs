using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using InitialProject.Services.IServices;
using InitialProject.Aplication.Factory;
using InitialProject.Services;
using InitialProject.Domen.Model;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class GuideProfileViewModel : INotifyPropertyChanged
    {

        private readonly IUserService userService;
        private readonly IGuideStatusService guideStatusService;
        public RelayCommand QuitJobCommand { get; set; }

        private readonly int GuideId;
        public GuideProfileViewModel(int? guideId)
        {
            userService = Injector.CreateInstance<IUserService>();
            guideStatusService = Injector.CreateInstance<IGuideStatusService>();

            QuitJobCommand = new RelayCommand(QuitJob);

            GuideId = (int)guideId;


            LoadProfile();
        }

        private void LoadProfile()
        {
            User user = userService.GetById(GuideId);
            GuideStatus guideStatus = guideStatusService.GetStatusByUserId(GuideId);


            Name = user.Name;
            Lastname = user.Username;
            Email = user.Email;
            Type = guideStatus.EmploymentStatus.ToString();
        }

        private void QuitJob(object parametar)
        {
            guideStatusService.QuitJob(GuideId);
            Type = GuideStatus.Status.Unemployed.ToString();
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _Lastname;
        public string Lastname
        {
            get { return _Lastname; }
            set
            {
                _Lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                OnPropertyChanged(nameof(Type)); 
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
