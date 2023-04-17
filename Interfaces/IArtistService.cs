namespace LicentaApp.Interfaces
{
    public interface IArtistService
    {
        Task<List<ArtistModel>> GetAsync();
        Task<ArtistModel?> GetAsyncById(string id);
        Task<ArtistModel> GetAsyncByName(string name);
        Task CreateAsync(ArtistModel newAlbumModel);
        Task UpdateAsync(string id, ArtistModel updatedAlbumModel);
        Task RemoveAsync(string id);
    }
}
