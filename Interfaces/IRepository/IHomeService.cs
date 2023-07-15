using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IHomeService
    {
        Task<IndexHomeViewModel> IndexHome();
    }
}
