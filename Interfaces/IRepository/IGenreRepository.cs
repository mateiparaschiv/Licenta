using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IGenreRepository
    {
        Task<IndexGenreListViewModel> IndexGenreList(string? sortOrder);
        Task<IndexGenreNameViewModel> GenreName(string name);
    }
}
