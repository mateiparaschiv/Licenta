namespace LicentaApp.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMongoCollection<FeedbackModel> _feedbackCollection;

        public FeedbackService(IMongoCollection<FeedbackModel> feedbackCollection)
        {
            _feedbackCollection = feedbackCollection;
        }

        public async Task<List<FeedbackModel>> GetAsync() =>
            await _feedbackCollection.Find(_ => true).ToListAsync();

        public async Task<FeedbackModel?> GetAsync(string id) =>
            await _feedbackCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(FeedbackModel newFeedbackModel) =>
            await _feedbackCollection.InsertOneAsync(newFeedbackModel);

        public async Task UpdateAsync(string id, FeedbackModel updatedFeedbackModel) =>
            await _feedbackCollection.ReplaceOneAsync(x => x.Id == id, updatedFeedbackModel);

        public async Task RemoveAsync(string id) =>
            await _feedbackCollection.DeleteOneAsync(x => x.Id == id);
    }
}
