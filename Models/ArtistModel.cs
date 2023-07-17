using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LicentaApp.Models
{
    public class ArtistModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("band")]
        public Boolean? Band { get; set; }

        [BsonElement("dob")]
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("dod")]
        public DateTime? DateOfDeath { get; set; }

        [BsonElement("formed")]
        public string Formed { get; set; }

        [BsonElement("disbanded")]
        public string Disbanded { get; set; }

        [BsonElement("image")]
        public string? Image { get; set; }

        [BsonElement("compoundScore")]
        public double CompoundScore { get; set; }

        [BsonElement("sentiment")]
        public string Sentiment { get; set; }

        [BsonElement("reviewCount")]
        public int ReviewCount { get; set; }
    }
}
