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

        //public async Task<IMongoQueryable<ArtistModel>> GetAsync() =>
        //    await _artistCollection.Find(_ => true).ToListAsync();
        //public async Task<List<ArtistModel>> GetPaginatedListAsync()
        //{
        //    //var query = _artistCollection.Find(_ => true);
        //    var perPage = 10;
        //    var page = 1;
        //    await _artistCollection.Find(_ => true).Skip(perPage * page).Limit(perPage).ToListAsync();
        //} 
        public async Task<List<ArtistModel>> GetPaginatedListAsync(int perPage, int page) =>
        //await _artistCollection.Find(_ => true).Skip(perPage * page).Limit(perPage).ToListAsync();
        await _artistCollection.Find(_ => true).SortBy(x => x.Name).Skip(perPage * page).Limit(perPage).ToListAsync();


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

        public void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
