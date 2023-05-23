using System;

namespace InitialProject.Domen.Model
{

    public class ComplexTourRequest : ValidationBase, ISerializable
    {
        public enum Status { OnHold, Accepted, Rejected }

        private int tourId;
        private int id;
        private int userId;
        private string tourName;
        private Location location;
        private Language language;
        private string description;
        private int guestNumber = 1;
        private DateTime startingDate;
        private DateTime endingDate;
        private Status requestStatus = Status.OnHold;
        private int guideId = -1;

        public int TourId
        {
            get { return tourId; }

            set
            {
                if (tourId != value)
                {
                    tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

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

        public string TourName
        {
            get { return tourName; }

            set
            {
                if (tourName != value)
                {
                    tourName = value;
                    OnPropertyChanged(nameof(TourName));
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


        public ComplexTourRequest()
        {
            Location = new Location();
            Language = new Language();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                TourId.ToString(),
                Id.ToString(),
                UserId.ToString(),
                TourName,
                Location.ToString(),
                Language.ToString(),
                Description,
                GuestNumber.ToString(),
                StartingDate.ToString(),
                EndingDate.ToString(),
                RequestStatus.ToString(),
                GuideId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            Id = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            TourName = values[3];
            Location = Location.fromStringToLocation(values[4]);
            Language = Language.fromStringToLanguage(values[5]);
            Description = values[6];
            GuestNumber = Convert.ToInt32(values[7]);
            StartingDate = DateTime.Parse(values[8]);
            EndingDate = DateTime.Parse(values[9]);
            RequestStatus = (Status)Enum.Parse(typeof(Status), values[10]);
            GuideId = Convert.ToInt32(values[11]);
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

            if (string.IsNullOrWhiteSpace(this.tourName))
            {
                this.ValidationErrors["TourName"] = "Tour name should not be empty.";
            }

            if (this.GuestNumber <= 0)
            {
                this.ValidationErrors["GuestNumber"] = "Guest number should be greater than 0.";
            }
        }
    }
}
