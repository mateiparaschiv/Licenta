using LicentaApp.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
namespace LicentaApp.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IMongoCollection<ArtistModel> _artistCollection;

        public ArtistService(IMongoCollection<ArtistModel> artistCollection)
        {
            _artistCollection = artistCollection;
        }

        public async Task<List<ArtistModel>> GetAsync() =>
            await _artistCollection.Find(_ => true).ToListAsync();

        public async Task<List<ArtistModel>> GetPaginatedListAsync(int pageNumber = 0, int pageSize = 10) =>
            await _artistCollection.Find(_ => true)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();

        public async Task<List<ArtistModel>> GetPaginatedListAsyncAscending(int pageNumber = 0, int pageSize = 10) =>
            await _artistCollection.Find(_ => true)
                .SortBy(x => x.Name)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();

        public async Task<List<ArtistModel>> GetPaginatedListAsyncDescending(int pageNumber = 0, int pageSize = 10) =>
            await _artistCollection.Find(_ => true)
                .SortByDescending(x => x.Name)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();

        public async Task<int> GetTotalCountAsync()
        {
            var totalCount = await _artistCollection.CountDocumentsAsync(FilterDefinition<ArtistModel>.Empty);
            return (int)totalCount;
        }

        public async Task<ArtistModel?> GetAsyncById(string id) =>
            await _artistCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<ArtistModel> GetAsyncByName(string name) =>
            await _artistCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task CreateAsync(ArtistModel newAlbumModel) =>
            await _artistCollection.InsertOneAsync(newAlbumModel);

        public async Task UpdateAsync(string id, ArtistModel updatedAlbumModel) =>
            await _artistCollection.ReplaceOneAsync(x => x.Id == id, updatedAlbumModel);

        public async Task RemoveAsync(string id) =>
            await _artistCollection.DeleteOneAsync(x => x.Id == id);
    }
}
