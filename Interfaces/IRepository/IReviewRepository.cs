using LicentaApp.Models;
namespace LicentaApp.Interfaces.IRepository
{
    public interface IReviewRepository
    {
        void AddReview(ReviewModel newReview);
        Task<List<ReviewModel>> IndexReviewList();
    }
}
