using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public enum UserType {Owner = 0, Guest1, Guest2, Guide}
    public class User : ISerializable
    {
        private int userId;
        private string username;
        private string password;
        private string email;
        private string phoneNumber;
        private UserType userType;

        public int UserId { get => userId; set => userId = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        internal UserType UserType { get => userType; set => userType = value; }

        public User(int userId, string username, 
            string password, string email, 
            string phoneNumber, UserType userType)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.userType = userType; 
        }
        public User()
        {

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                userId.ToString(),
                username,
                password,
                email,
                phoneNumber,
                userType.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            userId = Convert.ToInt32(values[0]);
            username = values[1];
            password = values[2];
            email = values[3];
            phoneNumber = values[4];
            userType = (UserType)Enum.Parse(typeof(UserType), values[5]);
        }
    }
}
