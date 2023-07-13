using LicentaApp.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace LicentaApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewService;
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        public ReviewService(IReviewRepository reviewService,
            IArtistRepository artistService,
            IAlbumRepository albumService)
        {
            _reviewService = reviewService;
            _artistRepository = artistService;
            _albumRepository = albumService;

        }

        public Task<List<ReviewModel>> IndexReviewList()
        {
            return _reviewService.GetAsync();
        }

        public async Task AddReview(ReviewModel newReview)
        {
            ReviewModel sentimentReview = await SentimentAnalysis(newReview.Message);
            newReview.NegativeScore = sentimentReview.NegativeScore;
            newReview.NeutralScore = sentimentReview.NeutralScore;
            newReview.PositiveScore = sentimentReview.PositiveScore;
            newReview.CompoundScore = sentimentReview.CompoundScore;
            newReview.Sentiment = sentimentReview.Sentiment;

            if (newReview.SubjectType.Equals("album"))
            {
                var album = await _albumRepository.GetAlbumByName(newReview.Subject);
                await _albumRepository.UpdateAlbumAsync(album.Id, newReview.CompoundScore);
            }
            if (newReview.SubjectType.Equals("artist"))
            {
                var artist = await _artistRepository.GetArtistByName(newReview.Subject);
                await _artistRepository.UpdateArtistAsync(artist.Id, newReview.CompoundScore);
            }

            await _reviewService.CreateAsync(newReview);
        }
        private async Task<ReviewModel> SentimentAnalysis(string text)
        {
            ReviewModel model = new ReviewModel();
            ProcessStartInfo start = new ProcessStartInfo();

            start.FileName = "python";
            start.Arguments = $"\"{Path.GetFullPath("sentiment_analysis.py")}\" \"{text}\"";
            start.UseShellExecute = false;
            start.CreateNoWindow = true;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = await process.StandardError.ReadToEndAsync();
                    string result = await reader.ReadToEndAsync();

                    JObject json = JObject.Parse(result);

                    model.NegativeScore = (double)json["negative"];
                    model.NeutralScore = (double)json["neutral"];
                    model.PositiveScore = (double)json["positive"];
                    model.CompoundScore = (double)json["compound"];
                    model.Sentiment = (string)json["overall_sentiment"];
                }
            }
            return model;
        }
    }
}
