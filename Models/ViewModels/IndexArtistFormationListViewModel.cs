namespace LicentaApp.Models.ViewModels
{
    public class IndexArtistFormationListViewModel
    {
        public List<ArtistModel> ArtistList { get; set; }
        public Dictionary<string, int> AlbumsToArtist { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int MaxPages { get; set; }
        public bool Band { get; set; }
    }
}