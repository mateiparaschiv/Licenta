using LicentaApp.Models;
namespace LicentaApp.Interfaces.IRepository
{
    public interface IReviewService
    {
        Task AddReview(ReviewModel newReview);
        Task<List<ReviewModel>> IndexReviewList();
    }
}
