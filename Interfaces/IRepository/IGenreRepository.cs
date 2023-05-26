using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IGenreRepository
    {
        Task<IndexGenreListViewModel> IndexGenreList(string? name, string? sortOrder);
        Task<IndexGenreNameViewModel> IndexGenreName(string? name);
    }
}
