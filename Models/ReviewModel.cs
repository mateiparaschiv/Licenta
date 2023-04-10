namespace LicentaApp.Models
{
    public class ReviewModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }
    }
}
