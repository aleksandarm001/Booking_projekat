using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class ReviewInfoDTO
    {
        public string OwnerName { get; set; }
        public string AccommodationName { get; set; }
        public int Hygiene { get; set; }
        public int FollowingRules { get; set; }
        public ReviewInfoDTO(string ownerName, string accommodationName, int hygiene, int followingRules)
        {
            OwnerName = ownerName;
            AccommodationName = accommodationName;
            Hygiene = hygiene;
            FollowingRules = followingRules;
        }
        public ReviewInfoDTO()
        {
            OwnerName = "";
            AccommodationName = "";
            FollowingRules = 0;
            Hygiene = 0;
        }
    }
}
