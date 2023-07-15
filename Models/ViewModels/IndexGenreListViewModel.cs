namespace LicentaApp.Models.ViewModels
{
    public class IndexGenreListViewModel
    {
        public List<GenreModel> AllGenreList { get; set; }
        public List<GenreModel> GenreList { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int MaxPages { get; set; }

    }
}
