using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class GuestReview: ISerializable
    {
        public int Id { get; set; }
        public int Hygiene { get; set; }
        public int RuleFollowing { get; set; }
        public string Comment { get; set; }

        public GuestReview()
        {
            Id = 0;
            Hygiene= 0;
            RuleFollowing= 0;
            Comment = "";

        }

        public GuestReview(int id, int hygieneGrade, int ruleFollowingGrade, string comment)
        {
            Id = id;
            Hygiene = hygieneGrade;
            RuleFollowing = ruleFollowingGrade;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Hygiene.ToString(),
                RuleFollowing.ToString(),
                Comment
            };

            return csvValues; 
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Hygiene= Convert.ToInt32(values[1]);
            RuleFollowing= Convert.ToInt32(values[2]);
            Comment = values[3];
        }
    }
}
