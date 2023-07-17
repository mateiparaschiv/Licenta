namespace LicentaApp.Models.ViewModels.AlbumViewModel
{
    public class IndexAlbumListViewModel
    {
        public List<AlbumModel> AllAlbumList { get; set; }
        public List<AlbumModel> AlbumList { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int MaxPages { get; set; }
        public List<int> YearList { get; set; }
        public List<string> GenreList { get; set; }

    }
}
