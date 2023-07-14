using LicentaApp.Models;

namespace LicentaApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public ReviewService(IReviewRepository reviewRepository,
            IArtistRepository artistRepository,
            IAlbumRepository albumRepository,
            ISentimentAnalysisService sentimentAnalysisService)
        {
            _reviewRepository = reviewRepository;
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        public Task<List<ReviewModel>> IndexReviewList()
        {
            return _reviewRepository.GetAsync();
        }

        public async Task AddReview(ReviewModel newReview)
        {
            await _sentimentAnalysisService.AnalyzeSentimentAsync(newReview);
            await _reviewRepository.CreateAsync(newReview);
            if (newReview.SubjectType.Equals("artist"))
            {
                await _artistRepository.UpdateArtistAsync(newReview.Subject, newReview.CompoundScore);
            }
            if (newReview.SubjectType.Equals("album"))
            {
                await _albumRepository.UpdateAlbumAsync(newReview.Subject, newReview.CompoundScore);
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddReviewAndRedirect(ReviewModel newReview)
        {
            try
            {
                await AddReview(newReview);
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception as needed.
                return (false, ex.Message);
            }
        }
    }
}
