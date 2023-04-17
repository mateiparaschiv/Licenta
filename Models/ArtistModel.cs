namespace LicentaApp.Models
{
    public class ArtistModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("band")]
        public Boolean? Band { get; set; }

        [BsonElement("dob")]
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("dod")]
        public DateTime? DateOfDeath { get; set; }

        [BsonElement("formed")]
        public string Formed { get; set; }

        [BsonElement("disbanded")]
        public string Disbanded { get; set; }

        [BsonElement("image")]
        public string? Image { get; set; }

        //public ArtistModel(string name, string description, bool? band, DateTime? dateOfBirth, DateTime? dateOfDeath, int? formed, int? disbanded)
        //{
        //    searchName = name ?? throw new ArgumentNullException(nameof(name));
        //    Description = description ?? throw new ArgumentNullException(nameof(description));
        //    Band = band;
        //    DateOfBirth = dateOfBirth;
        //    DateOfDeath = dateOfDeath;
        //    Formed = formed;
        //    Disbanded = disbanded;
        //}
        //Genres
    }
}
