namespace LicentaApp.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<FeedbackModel>> GetAsync();
        Task<FeedbackModel?> GetAsync(string id);
        Task CreateAsync(FeedbackModel newFeedbackModel);
        Task UpdateAsync(string id, FeedbackModel updatedFeedbackModel);
        Task RemoveAsync(string id);
    }
}
