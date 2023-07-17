namespace LicentaApp.Models.ViewModels
{
    public class IndexAlbumNameViewModel
    {
        public AlbumModel Album { get; set; }
        public List<ReviewModel> ReviewList { get; set; }
        public ReviewModel? NewReview { get; set; }
        public string ReturnUrl { get; set; }
    }
}
