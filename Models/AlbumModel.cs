namespace LicentaApp.Models
{
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
        [BsonRepresentation(BsonType.Double)]
        public double Rating { get; set; }

        [BsonElement("songs")]
        public List<string>? Songs { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }


        //public AlbumModel(string name, string artist, string genre, int year, string description, double rating, List<string> songs)
        //{
        //    Name = name ?? throw new ArgumentNullException(nameof(name));
        //    Artist = artist ?? throw new ArgumentNullException(nameof(artist));
        //    Genre = genre ?? throw new ArgumentNullException(nameof(genre));
        //    Year = year;
        //    Description = description ?? throw new ArgumentNullException(nameof(description));
        //    Rating = rating;
        //    Songs = songs ?? throw new ArgumentNullException(nameof(songs));
        //}
    }
}
