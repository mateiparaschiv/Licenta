using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IArtistRepository
    {
        Task<ArtistModel> GetArtistByName(string name);
        Task<int> GetTotalCountAsync();
        Task<List<ArtistModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10);
        Task UpdateArtistAsync(string artistName, double compoundScore);
    }
}