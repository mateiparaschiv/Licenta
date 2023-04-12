namespace LicentaApp.Interfaces
{
    public interface IAlbumService
    {
        Task<List<AlbumModel>> GetAsync();
        Task<AlbumModel?> GetAsyncById(string id);
        Task<List<AlbumModel>> GetAsyncListByName(string albumName);
        Task<int?> GetNumOfAlbumsByName(string albumName);
        Task CreateAsync(AlbumModel newAlbumModel);
        Task UpdateAsync(string id, AlbumModel updatedAlbumModel);
        Task RemoveAsync(string id);
        Task<AlbumModel?> GetAsyncByName(string name);
    }
}
