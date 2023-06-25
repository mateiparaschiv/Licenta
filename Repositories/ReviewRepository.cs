using LicentaApp.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace LicentaApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public ReviewRepository(IReviewService reviewService)
        {
            _reviewService = reviewService;

        }
        private readonly IReviewService _reviewService;
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

            await _reviewService.CreateAsync(newReview);
        }

        public async Task<ReviewModel> SentimentAnalysis(string text)
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
