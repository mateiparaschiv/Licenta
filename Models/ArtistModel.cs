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

        [BsonElement("band")]
        [BsonRepresentation(BsonType.Boolean)]
        public Boolean? Band { get; set; }

        [BsonElement("dob")]
        [BsonRepresentation(BsonType.DateTime)]

        // TODO: [BsonRepresentation(BsonType.Object)] 
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("dod")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? DateOfDeath { get; set; }

        [BsonElement("formed")]
        [BsonRepresentation(BsonType.Int32)]
        public int? Formed { get; set; }

        [BsonElement("disbanded")]
        [BsonRepresentation(BsonType.Int32)]
        public int? Disbanded { get; set; }

        [BsonElement("image")]
        [BsonRepresentation(BsonType.String)]
        public string? Image { get; set; }

        public ArtistModel(string name, string description, bool? band, DateTime? dateOfBirth, DateTime? dateOfDeath, int? formed, int? disbanded)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Band = band;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
            Formed = formed;
            Disbanded = disbanded;
        }
        //Genres
    }
}
