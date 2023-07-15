namespace LicentaApp.Models.ViewModels
{
    public class IndexArtistNameViewModel
    {
        public ArtistModel Artist { get; set; }
        public List<AlbumModel> ArtistAlbums { get; set; }
        public List<AlbumModel> ArtistBestAlbums { get; set; }
        public string SortOrder { get; set; }
        public List<ReviewModel> ReviewList { get; set; }
        public ReviewModel? NewReview { get; set; }

    }
}
