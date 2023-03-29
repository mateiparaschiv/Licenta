namespace LicentaApp.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string username { get; set; }

        public string email { get; set; }

        public string? name { get; set; }
        public string? surname { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public double rating { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int age { get; set; }
        public int Age { get; set; }
    }
}
