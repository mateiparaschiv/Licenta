using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IAlbumRepository
    {
        Task<List<AlbumModel>> GetAsync();
        Task<List<AlbumModel>> GetListByArtist(string artistName);
        Task<List<AlbumModel>> GetListByGenre(string genre);
        Task<Dictionary<string, int>> GetNumOfAlbumsByNames(List<ArtistModel> artistList);
        Task CreateAsync(AlbumModel newAlbumModel);
        Task<AlbumModel?> GetAlbumByName(string name);
        Task<List<AlbumModel>> GetFilteredListByArtist(string artistName, string sortOrder);
        Task<List<AlbumModel>> GetFilteredListByName(string sortOrder);
        Task<List<AlbumModel>> GetRandomAlbums(int count);
    }
}
