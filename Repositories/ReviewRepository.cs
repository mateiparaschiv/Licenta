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
            //newReview = new ReviewModel("paraschivmatei20@stud.ase.ro", "mateematy", "ReviewTest", "Testing Review", "I am testing the review model.");
            //_reviewService.CreateAsync(newReview);
        }

        public Task<List<ReviewModel>> IndexReviewList()
        {
            return _reviewService.GetAsync();
        }
    }
}
