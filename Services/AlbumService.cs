using MongoDB.Driver;

namespace LicentaApp.Services
{
    public class AlbumService
    {
        private readonly IMongoCollection<AlbumModel> _albumCollection;

        public AlbumService(IMongoCollection<AlbumModel> albumCollection)
        {
            _albumCollection = albumCollection;
        }

        public async Task<List<AlbumModel>> GetAsync() =>
            await _albumCollection.Find(_ => true).ToListAsync();

        public async Task<AlbumModel?> GetAsync(string id) =>
            await _albumCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(AlbumModel newAlbumModel) =>
            await _albumCollection.InsertOneAsync(newAlbumModel);

        public async Task UpdateAsync(string id, AlbumModel updatedAlbumModel) =>
            await _albumCollection.ReplaceOneAsync(x => x.Id == id, updatedAlbumModel);

        public async Task RemoveAsync(string id) =>
            await _albumCollection.DeleteOneAsync(x => x.Id == id);
    }
}
