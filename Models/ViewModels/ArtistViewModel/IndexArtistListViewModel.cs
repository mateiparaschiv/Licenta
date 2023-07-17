namespace LicentaApp.Models.ViewModels.ArtistViewModel
{
    public class IndexArtistListViewModel
    {
        public List<ArtistModel> AllArtistList { get; set; }
        public List<ArtistModel> ArtistList { get; set; }
        public Dictionary<string, int> AlbumsToArtist { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int MaxPages { get; set; }
    }
}
