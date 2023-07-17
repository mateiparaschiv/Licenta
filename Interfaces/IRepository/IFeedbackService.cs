using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IFeedbackService
    {
        Task<List<FeedbackModel>> GetAsync();
    }
}
