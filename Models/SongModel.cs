using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LicentaApp.Models
{
    public class SongModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("rating")]
        public double Rating { get; set; }
    }
}
