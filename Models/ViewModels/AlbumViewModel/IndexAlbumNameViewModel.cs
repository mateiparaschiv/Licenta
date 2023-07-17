namespace LicentaApp.Models.ViewModels.AlbumViewModel
{
    public class IndexAlbumNameViewModel
    {
        public AlbumModel Album { get; set; }
        public List<ReviewModel> ReviewList { get; set; }
        public ReviewModel? NewReview { get; set; }
    }
}