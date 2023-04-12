namespace LicentaApp.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewModel>> GetAsync();
        Task<ReviewModel?> GetAsync(string id);
        Task CreateAsync(ReviewModel newReviewModel);
        Task UpdateAsync(string id, ReviewModel updatedReviewModel);
        Task RemoveAsync(string id);
    }
}
