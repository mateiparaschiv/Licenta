namespace LicentaApp.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("surname")]
        public string? Surname { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        [BsonElement("rating")]
        public double Rating { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("age")]
        public int Age { get; set; }
    }
}
