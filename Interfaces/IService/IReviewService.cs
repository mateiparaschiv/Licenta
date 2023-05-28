using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IReviewService
    {
        Task<List<ReviewModel>> GetAsync();
        Task<ReviewModel?> GetAsync(string id);
        Task CreateAsync(ReviewModel newReviewModel);
        Task UpdateAsync(string id, ReviewModel updatedReviewModel);
        Task RemoveAsync(string id);
        Task<List<ReviewModel>> GetAsyncListByAlbum(string albumName);
    }
}
