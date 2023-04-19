namespace InitialProject.Domen.Model
{
    using System;

    public class TourRequest : ISerializable
    {
        public enum Status { OnHold, Accepted, Rejected }
        public int Id { get; set; }
        public int UserId { get; set; }
        public Location Location { get; set; }
        public Language Language { get; set; }
        public string Description { get; set; }
        public int GuestNumber { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Status TourStatus { get; set; } = Status.OnHold;


        public TourRequest() { }

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
                TourStatus.ToString() };
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
            StartingDate = DateTime.Parse(values[7]);
            TourStatus = (Status)Enum.Parse(typeof(Status), values[8]);
        }
    }
}
