namespace LicentaApp.Models
{
    public class FeedbackModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }
    }
}
