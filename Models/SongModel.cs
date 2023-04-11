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

        [BsonRepresentation(BsonType.Double)]
        public double Rating { get; set; }
    }
}
