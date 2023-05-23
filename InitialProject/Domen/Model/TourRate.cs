namespace InitialProject.Domen.Model
{
    using InitialProject.Domen;
    using System;
    using System.Collections.Generic;
    using System.Printing;

    public class TourRate : ValidationBase,ISerializable
    {
        public TourRate() { }
        public int GuestId { get; set; }
        public int TourId { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int TourInterest { get; set; }
        public string? Comment { get; set; }
        public List<string>? Images { get; set; }

        public DateTime? Date { get; set; }

        public bool? IsValidRate { get; set; } = true;

        public TourRate(int guestId, int tourId, int guideKnowledge, int guideLanguage, int tourInterest, bool isValid, string? comment, List<string>? images)
        {
            GuestId = guestId;
            TourId = tourId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourInterest = tourInterest;
            IsValidRate = isValid;
            Comment = comment;
            Images = images;
        }

        public string[] ToCSV()
        {

            string[] csvValues = {
                GuestId.ToString(),
                TourId.ToString(),
                GuideKnowledge.ToString(),
                GuideLanguage.ToString(),
                TourInterest.ToString(),
                IsValidRate.ToString(),
                Comment,
                ImagesListToString(),
                Date.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            GuideKnowledge = Convert.ToInt32(values[2]);
            GuideLanguage = Convert.ToInt32(values[3]);
            TourInterest = Convert.ToInt32(values[4]);
            IsValidRate = Convert.ToBoolean(values[5]);
            Comment = values[6];
            Images = StringToImagesList(values[7]);
            Date = DateTime.TryParse(values[8],out var result) ? result : null;
        }

        private string ImagesListToString()
        {
            string s = "";

           if (Images == null || Images.Count == 0)
                return s;
            foreach (string image in Images)
            {
                if (Images.IndexOf(image) == Images.Count - 1)
                {
                    s += image;
                }
                else
                {
                    s += image + ";";
                }
            }
            return s;
        }
        private List<string> StringToImagesList(string s)
        {
            return new List<string>(s.Split(";"));
        }

        protected override void ValidateSelf()
        {

            if (this.GuideKnowledge == 0)
            {
                this.ValidationErrors["GuideKnowledge"] = "Guide knowledge must be selected.";
            }

            if (this.TourInterest == 0)
            {
                this.ValidationErrors["TourInterest"] = "Tour interest must be selected.";
            }

            if (this.GuideLanguage == 0)
            {
                this.ValidationErrors["GuideLanguage"] = "Guide language must be selected.";
            }
        }
    }


}
