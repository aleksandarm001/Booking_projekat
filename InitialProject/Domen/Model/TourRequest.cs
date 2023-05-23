namespace InitialProject.Domen.Model
{
    using System;
    using static InitialProject.Domen.Model.ComplexTourRequest;

    public class TourRequest : ValidationBase, ISerializable
    {
        private int id;
        private int userId;
        private Location location;
        private Language language;
        private string description;
        private int guestNumber = 1;
        private DateTime startingDate;
        private DateTime endingDate;
        private Status requestStatus = Status.OnHold;
        private int guideId = -1;


        public int Id
        {
            get { return id; }

            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        public int UserId
        {
            get { return userId; }

            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }
        public Location Location
        {
            get { return location; }

            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        public Language Language
        {
            get { return language; }

            set
            {
                if (language != value)
                {
                    language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }
        public string Description
        {
            get { return description; }

            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public int GuestNumber
        {
            get { return guestNumber; }

            set
            {
                if (guestNumber != value)
                {
                    guestNumber = value;
                    OnPropertyChanged(nameof(GuestNumber));
                }
            }
        }
        public DateTime StartingDate
        {
            get { return startingDate; }

            set
            {
                if (startingDate != value)
                {
                    startingDate = value;
                    OnPropertyChanged(nameof(StartingDate));
                }
            }
        }
        public DateTime EndingDate
        {
            get { return endingDate; }

            set
            {
                if (endingDate != value)
                {
                    endingDate = value;
                    OnPropertyChanged(nameof(EndingDate));
                }
            }
        }
        public Status RequestStatus
        {
            get { return requestStatus; }

            set
            {
                if (requestStatus != value)
                {
                    requestStatus = value;
                    OnPropertyChanged(nameof(RequestStatus));
                }
            }
        }
        public int GuideId
        {
            get { return guideId; }

            set
            {
                if (guideId != value)
                {
                    guideId = value;
                    OnPropertyChanged(nameof(GuideId));
                }
            }
        }


        public TourRequest()
        {
            Location = new Location();
            Language = new Language();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                UserId.ToString(),
                Location.ToString(),
                Language.ToString(),
                Description,
                GuestNumber.ToString(),
                StartingDate.ToString(),
                EndingDate.ToString(),
                RequestStatus.ToString(),
                GuideId.ToString()
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Location = Location.fromStringToLocation(values[2]);
            Language = Language.fromStringToLanguage(values[3]);
            Description = values[4];
            GuestNumber = Convert.ToInt32(values[5]);
            StartingDate = DateTime.Parse(values[6]);
            EndingDate = DateTime.Parse(values[7]);
            RequestStatus = (Status)Enum.Parse(typeof(Status), values[8]);
            GuideId = Convert.ToInt32(values[9]);
        }

        protected override void ValidateSelf()
        {

            this.Location.Validate();
           
            if (!this.Location.IsValid)
            {
                this.ValidationErrors["Location.Country"] = this.Location.ValidationErrors["Country"];
                this.ValidationErrors["Location.City"] = this.Location.ValidationErrors["City"];
            }

            this.Language.Validate();
            if (!this.Language.IsValid)
            {
                this.ValidationErrors["Language"] = this.Language.ValidationErrors["Name"];
            }

            if (this.StartingDate <= DateTime.Now)
            {
                this.ValidationErrors["StartingDate"] = "Start date should not be in past.";
            }


            if (this.EndingDate < this.StartingDate)
            {
                this.ValidationErrors["EndingDate"] = "End date should be after start date.";
            }

            if (this.EndingDate <= DateTime.Now)
            {
                this.ValidationErrors["EndingDate"] = "End date should not be in past.";
            }

            if (string.IsNullOrWhiteSpace(this.Description))
            {
                this.ValidationErrors["Description"] = "Description should not be empty.";
            }

            if (this.GuestNumber <= 0)
            {
                this.ValidationErrors["GuestNumber"] = "Guest number should be greater than 0.";
            }


        }
    }
}
