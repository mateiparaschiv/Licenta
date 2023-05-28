using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IArtistService
    {
        Task<List<ArtistModel>> GetAsync();
        //Task<IMongoQueryable<ArtistModel>> GetAsync();
        Task<ArtistModel?> GetAsyncById(string id);
        Task<ArtistModel> GetAsyncByName(string name);
        Task CreateAsync(ArtistModel newAlbumModel);
        Task UpdateAsync(string id, ArtistModel updatedAlbumModel);
        Task RemoveAsync(string id);
        void Shuffle<T>(IList<T> list);
        Task<List<ArtistModel>> GetPaginatedListAsync(int perPage, int page);
    }

}
