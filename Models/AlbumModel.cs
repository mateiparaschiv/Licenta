namespace LicentaApp.Models
{
    public class AlbumModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonRepresentation(BsonType.String)]
        /*[BsonElement("name")]*/

        public string name { get; set; }
        [BsonRepresentation(BsonType.String)]
        /*[BsonElement("artist")]*/

        public string artist { get; set; }
        [BsonRepresentation(BsonType.String)]
        /*[BsonElement("genre")]*/

        public string genre { get; set; }
        [BsonRepresentation(BsonType.Int32)]

        public int year { get; set; }
        [BsonRepresentation(BsonType.String)]

        public string description { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]

        public float rating { get; set; }

    }
}
