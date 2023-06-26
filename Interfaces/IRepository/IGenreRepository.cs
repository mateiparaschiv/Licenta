using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IGenreRepository
    {
        Task<GenreModel> GetAsyncByName(string name);
        Task<List<GenreModel>> GetAsyncListAscending();
        Task<List<GenreModel>> GetAsyncListDescending();
        Task<List<GenreModel>> GetFilteredListByName(string sortOrder);
    }
}
