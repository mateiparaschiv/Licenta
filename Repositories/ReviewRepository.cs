using LicentaApp.Models;
namespace LicentaApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public ReviewRepository(IReviewService reviewService)
        {
            _reviewService = reviewService;

        }
        private readonly IReviewService _reviewService;
        public async void AddReview(ReviewModel newReview)
        {
            await _reviewService.CreateAsync(newReview);
        }

        public Task<List<ReviewModel>> IndexReviewList()
        {
            return _reviewService.GetAsync();
        }
    }
}
