using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LicentaApp.Models
{
    public class ReviewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("sentiment")]
        public string Sentiment { get; set; }

        [BsonElement("negativeScore")]
        public double NegativeScore { get; set; }

        [BsonElement("neutralScore")]
        public double NeutralScore { get; set; }

        [BsonElement("positiveScore")]
        public double PositiveScore { get; set; }

        [BsonElement("compoundScore")]
        public double CompoundScore { get; set; }

        [BsonElement("subjectType")]
        public string SubjectType { get; set; }
    }
}
