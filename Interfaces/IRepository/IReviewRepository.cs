using LicentaApp.Models;
namespace LicentaApp.Interfaces.IRepository
{
    public interface IReviewRepository
    {
        Task AddReview(ReviewModel newReview);
        Task<List<ReviewModel>> IndexReviewList();
    }
}
