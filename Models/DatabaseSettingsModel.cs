namespace LicentaApp.Models
{
    public class DatabaseSettingsModel
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public CollectionName CollectionName { get; set; } = null!;
    }
    public class CollectionName
    {
        public string AlbumCollection { get; set; } = null!;
        public string ArtistCollection { get; set; } = null!;
        public string FeedbackCollection { get; set; } = null!;
        public string ReviewCollection { get; set; } = null!;
        public string SongCollection { get; set; } = null!;
        public string UserCollection { get; set; } = null!;

    }
}
