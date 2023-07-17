using LicentaApp.Models.ViewModels.HomeViewModel;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IHomeService
    {
        Task<IndexHomeViewModel> IndexHome();
    }
}
