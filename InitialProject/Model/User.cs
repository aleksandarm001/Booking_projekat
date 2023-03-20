using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    //public enum UserType { Owner = 0, Guest1 = 1, Guest2 = 2, Guide = 3 }
    public class User : ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int role { get; set; }
        public User() { }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Username, Email, Password, role.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Username = values[2];
            Email = values[3];
            Password = values[4];
            role = Convert.ToInt32(values[5]);
        }
    }
}
