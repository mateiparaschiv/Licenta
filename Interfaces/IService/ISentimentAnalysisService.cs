using LicentaApp.Models;

namespace LicentaApp.Interfaces.IService
{
    public interface ISentimentAnalysisService
    {
        Task AnalyzeSentimentAsync(ReviewModel reviewModel);
    }
}
