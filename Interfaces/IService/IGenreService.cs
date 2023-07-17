using LicentaApp.Models.ViewModels.GenreViewModel;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IGenreService
    {
        Task<IndexGenreListViewModel> IndexGenreList(string sortOrder, int pageNumber);
        Task<IndexGenreNameViewModel> GenreName(string name);
    }
}
