namespace LicentaApp.Models.ViewModels
{
    public class IndexArtistNameViewModel
    {
        public ArtistModel Artist { get; set; }
        public List<AlbumModel> ArtistAlbums { get; set; }
        public string SortOrder { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}
