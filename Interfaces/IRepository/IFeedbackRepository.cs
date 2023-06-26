using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IFeedbackRepository
    {
        Task<List<FeedbackModel>> GetAsync();
    }
}
