﻿using LicentaApp.Models;

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

        private async Task AddReview(ReviewModel newReview)
        {
            await _sentimentAnalysisService.AnalyzeSentimentAsync(newReview);
            await _reviewRepository.CreateAsync(newReview);
            if (newReview.SubjectType.Equals("artist"))
            {
                await _artistRepository.UpdateArtistAsync(newReview.Subject, newReview.CompoundScore, newReview.Sentiment);
            }
            if (newReview.SubjectType.Equals("album"))
            {
                await _albumRepository.UpdateAlbumAsync(newReview.Subject, newReview.CompoundScore, newReview.Sentiment);
            }
        }
        private async Task DeleteReview(string reviewId)
        {
            var review = await _reviewRepository.GetAsyncById(reviewId);
            if (review == null)
            {
                throw new Exception($"No review found with the id: {reviewId}");
            }

            if (review.SubjectType.Equals("artist"))
            {
                var artist = await _artistRepository.GetArtistByName(review.Subject);
                if (artist != null)
                {
                    artist.ReviewCount--;
                    if (artist.ReviewCount > 0)
                    {
                        // Adjust the average compound score after deleting a review
                        artist.CompoundScore = ((artist.CompoundScore * (artist.ReviewCount + 1)) - review.CompoundScore) / artist.ReviewCount;
                    }
                    else
                    {
                        artist.Sentiment = "NoSentiment";
                        artist.CompoundScore = 0;
                    }

                    await _artistRepository.UpdateAsync(artist.Id, artist);
                }
            }
            else if (review.SubjectType.Equals("album"))
            {
                var album = await _albumRepository.GetAlbumByName(review.Subject);
                if (album != null)
                {
                    album.ReviewCount--;
                    if (album.ReviewCount > 0)
                    {
                        // Adjust the average compound score after deleting a review
                        album.CompoundScore = ((album.CompoundScore * (album.ReviewCount + 1)) - review.CompoundScore) / album.ReviewCount;
                    }
                    else
                    {
                        //album.Sentiment = string.Empty;
                        album.Sentiment = "NoSentiment";
                        album.CompoundScore = 0;
                    }

                    await _albumRepository.UpdateAsync(album.Id, album);
                }
            }
            await _reviewRepository.DeleteAsync(reviewId);
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
        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteReviewAndRedirect(string reviewId)
        {
            try
            {
                await DeleteReview(reviewId);
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
