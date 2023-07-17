using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IAlbumRepository
    {
        Task<List<AlbumModel>> GetAsyncFilteredByName();
        Task UpdateAsync(string id, AlbumModel updatedAlbumModel);
        Task<List<AlbumModel>> GetListByArtist(string artistName);
        Task<List<AlbumModel>> GetListByGenre(string genre);
        Task<Dictionary<string, int>> GetNumOfAlbumsByNames(List<ArtistModel> artistList);
        Task CreateAsync(AlbumModel newAlbumModel);
        Task<AlbumModel> GetAlbumByName(string name);
        Task<List<AlbumModel>> GetFilteredListByArtist(string artistName, string sortOrder);
        Task<List<AlbumModel>> GetFilteredListByName(string sortOrder);
        Task<List<AlbumModel>> GetRandomAlbums(int count);
        Task<List<AlbumModel>> GetPaginatedFilteredList(string sortOrder, int? year = null, string? genre = null, string? sentiment = null, int pageNumber = 0, int pageSize = 10);
        Task<int> GetTotalCountAsync(int? year = null, string? genre = null, string? sentiment = null);
        Task UpdateAlbumAsync(string albumId, double compoundScore);
        Task<List<AlbumModel>> GetFilteredListByCompoundScore(int? count = 0, string? albumGenre = null, string? albumArtist = null);
        Task<List<int>> GetDistinctYearsAsync();
    }
}
