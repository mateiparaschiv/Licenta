namespace LicentaApp.Interfaces
{
    public interface IAlbumService
    {
        Task<List<AlbumModel>> GetAsync();
        Task<AlbumModel?> GetAsyncById(string id);
        Task<List<AlbumModel>> GetAsyncListByName(string artistName);
        Task<List<AlbumModel>> GetAsyncListByGenre(string genre);
        Task<Dictionary<string, int>> GetNumOfAlbumsByNames(List<ArtistModel> artistList);
        Task<int?> GetNumOfAlbumsByName(string artistName);
        Task CreateAsync(AlbumModel newAlbumModel);
        Task UpdateAsync(string id, AlbumModel updatedAlbumModel);
        Task RemoveAsync(string id);
        Task<AlbumModel?> GetAsyncByName(string name);
    }
}
