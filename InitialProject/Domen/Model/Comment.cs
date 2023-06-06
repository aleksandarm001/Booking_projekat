using System;

namespace InitialProject.Domen.Model
{
    public class Comment : ISerializable
    {
        public int CommentId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public Boolean IsOwnerComment { get; set; }
        public int Reports { get; set; }

        public Comment()
        {
            IsOwnerComment = false;
            Reports= 0;
        }

        public Comment(DateTime creationTime, string text, int userId, Boolean isOwnerComment,int reports)
        {
            CreationTime = creationTime;
            Text = text;
            UserId = userId;
            IsOwnerComment= isOwnerComment;
            Reports = reports;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { CommentId.ToString(), CreationTime.ToString(), Text, UserId.ToString() , IsOwnerComment.ToString(),Reports.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            CommentId = Convert.ToInt32(values[0]);
            CreationTime = Convert.ToDateTime(values[1]);
            Text = values[2];
            UserId = Convert.ToInt32(values[3]);
            IsOwnerComment= Convert.ToBoolean(values[4]);
            Reports = Convert.ToInt32(values[5]);
            
        }
    }
}
