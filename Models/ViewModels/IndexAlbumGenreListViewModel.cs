namespace LicentaApp.Models.ViewModels
{
    public class IndexAlbumGenreListViewModel
    {
        public List<AlbumModel> AlbumList { get; set; }
        public List<string> GenreList { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int MaxPages { get; set; }
        public string Genre { get; set; }
    }
}
