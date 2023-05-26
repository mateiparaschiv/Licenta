namespace LicentaApp.Models
{
    public class ReviewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string? Id { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("username")]
        public string? Username { get; set; }

        [BsonElement("subject")]
        public string? Subject { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("message")]
        public string? Message { get; set; }

        //public ReviewModel(string email, string username, string subject, string title, string message)
        //{
        //    Email = email ?? throw new ArgumentNullException(nameof(email));
        //    Username = username ?? throw new ArgumentNullException(nameof(username));
        //    Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        //    Title = title ?? throw new ArgumentNullException(nameof(title));
        //    Message = message ?? throw new ArgumentNullException(nameof(message));
        //}
    }
}
