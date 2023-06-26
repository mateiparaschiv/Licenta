using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IReviewRepository
    {
        Task<List<ReviewModel>> GetAsync();
        Task CreateAsync(ReviewModel newReviewModel);
        Task<List<ReviewModel>> GetAsyncListByAlbum(string albumName);
    }
}
