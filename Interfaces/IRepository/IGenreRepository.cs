using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IGenreRepository
    {
        Task<List<GenreModel>> GetAsync();
        Task<List<GenreModel>> GetAsyncFilteredByName();
        Task<GenreModel> GetAsyncByName(string name);
        Task<List<GenreModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10);
        Task<int> GetTotalCountAsync();
    }
}
