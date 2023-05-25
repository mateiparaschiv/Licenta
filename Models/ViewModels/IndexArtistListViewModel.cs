namespace LicentaApp.Models.ViewModels
{
    public class IndexArtistListViewModel
    {
        public List<ArtistModel> ArtistList { get; set; }
        public Dictionary<string, int> AlbumsToArtist { get; set; }
        public String SortOrder { get; set; }
        public int PageNumber { get; set; }
    }
}
