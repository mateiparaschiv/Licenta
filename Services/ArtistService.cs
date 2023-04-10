using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Services
{
    public class ArtistService
    {
        private readonly IMongoCollection<ArtistModel> _artistCollection;

        public ArtistService(IMongoCollection<ArtistModel> artistCollection)
        {
            _artistCollection = artistCollection;
        }

        public async Task<List<ArtistModel>> GetAsync() =>
            await _artistCollection.Find(_ => true).ToListAsync();

        public async Task<ArtistModel?> GetAsync(string id) =>
            await _artistCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ArtistModel newAlbumModel) =>
            await _artistCollection.InsertOneAsync(newAlbumModel);

        public async Task UpdateAsync(string id, ArtistModel updatedAlbumModel) =>
            await _artistCollection.ReplaceOneAsync(x => x.Id == id, updatedAlbumModel);

        public async Task RemoveAsync(string id) =>
            await _artistCollection.DeleteOneAsync(x => x.Id == id);
    }
}
