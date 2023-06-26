using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<FeedbackModel> _feedbackCollection;

        public FeedbackRepository(IMongoCollection<FeedbackModel> feedbackCollection)
        {
            _feedbackCollection = feedbackCollection;
        }

        public async Task<List<FeedbackModel>> GetAsync() =>
            await _feedbackCollection.Find(_ => true).ToListAsync();
    }
}
