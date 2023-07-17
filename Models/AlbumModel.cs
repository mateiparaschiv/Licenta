using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LicentaApp.Models
{
    [BsonIgnoreExtraElements]
    public class AlbumModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("artist")]
        public string Artist { get; set; }

        [BsonElement("genre")]
        public string Genre { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("rating")]
        public double Rating { get; set; }

        [BsonElement("songs")]
        public List<string> Songs { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("shortDescription")]
        public string ShortDescription { get; set; }

        [BsonElement("compoundScore")]
        public double CompoundScore { get; set; }

        [BsonElement("sentiment")]
        public string Sentiment { get; set; }

        [BsonElement("reviewCount")]
        public int ReviewCount { get; set; }
    }
}
