using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IGenreService
    {
        Task<List<GenreModel>> GetAsync();
        Task<GenreModel?> GetAsync(string id);
        Task CreateAsync(GenreModel newGenreModel);
        Task UpdateAsync(string id, GenreModel updatedGenreModel);
        Task RemoveAsync(string id);
        Task<GenreModel?> GetAsyncByName(string name);
        Task<List<GenreModel>> GetAsyncListAscending();
        Task<List<GenreModel>> GetAsyncListDescending();
    }
}
