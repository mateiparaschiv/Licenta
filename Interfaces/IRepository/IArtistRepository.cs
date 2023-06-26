using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IArtistRepository
    {
        Task<ArtistModel> GetAsyncByName(string name);
        Task<int> GetTotalCountAsync();
        Task<List<ArtistModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10);
    }
}
