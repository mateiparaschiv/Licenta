namespace LicentaApp.Models
{
    public class AlbumModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("artist")]
        [BsonRepresentation(BsonType.String)]
        public string Artist { get; set; }

        [BsonElement("genre")]
        [BsonRepresentation(BsonType.String)]
        public string Genre { get; set; }

        [BsonElement("year")]
        [BsonRepresentation(BsonType.Int32)]
        public int Year { get; set; }

        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("rating")]
        [BsonRepresentation(BsonType.Double)]
        public double Rating { get; set; }

        [BsonElement("songs")]
        //[BsonRepresentation(BsonType.Array)]
        public List<string>? Songs { get; set; }

        [BsonElement("image")]
        [BsonRepresentation(BsonType.String)]
        public string Image { get; set; }
    }
}
