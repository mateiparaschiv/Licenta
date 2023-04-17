namespace LicentaApp.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
    }
}
