using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IReviewRepository
    {
        Task<List<ReviewModel>> GetAsync();
        Task CreateAsync(ReviewModel newReviewModel);
        Task DeleteAsync(string id);
        Task<List<ReviewModel>> GetAsyncListBySubject(string albumName);
        Task<ReviewModel> GetAsyncById(string id);
        Task<List<ReviewModel>> GetAsyncFilteredByDate(string? subject = null);
    }
}
