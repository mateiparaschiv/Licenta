using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IArtistRepository
    {
        Task<ArtistModel> GetArtistByName(string name);
        Task UpdateAsync(string id, ArtistModel updatedArtistModel);
        Task<List<ArtistModel>> GetAsyncFilteredByName();
        Task<int> GetTotalCountAsync(string? sentiment = null, bool? band = null);
        Task<List<ArtistModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10, string? sentiment = null, bool? band = null);
        Task UpdateArtistAsync(string artistName, double compoundScore);
        Task<List<ArtistModel>> GetFilteredListByCompoundScore(int? count);
    }
}