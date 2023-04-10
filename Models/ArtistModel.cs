namespace LicentaApp.Models
{
    public class ArtistModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("rating")]
        [BsonRepresentation(BsonType.Decimal128)]
        public float Rating { get; set; }
    }
}
