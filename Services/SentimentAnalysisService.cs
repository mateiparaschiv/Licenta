using LicentaApp.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace LicentaApp.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        public async Task AnalyzeSentimentAsync(ReviewModel reviewModel)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{Path.GetFullPath("sentiment_analysis.py")}\" \"{reviewModel.Message}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = Process.Start(startInfo);
            using var reader = process.StandardOutput;
            string stderr = await process.StandardError.ReadToEndAsync();
            string result = await reader.ReadToEndAsync();

            JObject json = JObject.Parse(result);

            reviewModel.NegativeScore = (double)json["negative"];
            reviewModel.NeutralScore = (double)json["neutral"];
            reviewModel.PositiveScore = (double)json["positive"];
            reviewModel.CompoundScore = (double)json["compound"];
            reviewModel.Sentiment = (string)json["overall_sentiment"];
        }
    }
}
