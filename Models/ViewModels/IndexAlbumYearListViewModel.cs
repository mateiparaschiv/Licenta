namespace LicentaApp.Models.ViewModels
{
    public class IndexAlbumYearListViewModel
    {
        public List<AlbumModel> AlbumList { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int MaxPages { get; set; }
        public int Year { get; set; }
    }
}