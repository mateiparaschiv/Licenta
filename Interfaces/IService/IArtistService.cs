using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IArtistService
    {
        Task<List<ArtistModel>> GetAsync();
        Task<ArtistModel?> GetAsyncById(string id);
        Task<ArtistModel> GetAsyncByName(string name);
        Task CreateAsync(ArtistModel newAlbumModel);
        Task UpdateAsync(string id, ArtistModel updatedAlbumModel);
        Task RemoveAsync(string id);
        Task<List<ArtistModel>> GetPaginatedListAsync(int perPage, int page);
        Task<List<ArtistModel>> GetPaginatedListAsyncAscending(int perPage, int page);
        Task<List<ArtistModel>> GetPaginatedListAsyncDescending(int perPage, int page);
        Task<int> GetTotalCountAsync();
    }
}
