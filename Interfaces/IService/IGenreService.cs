using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IGenreService
    {
        Task<IndexGenreListViewModel> IndexGenreList(string sortOrder, int pageNumber);
        Task<IndexGenreNameViewModel> GenreName(string name);
    }
}
