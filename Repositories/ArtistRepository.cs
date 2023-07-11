using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IMongoCollection<ArtistModel> _artistCollection;

        public ArtistRepository(IMongoCollection<ArtistModel> artistCollection)
        {
            _artistCollection = artistCollection;
        }

        public async Task<ArtistModel> GetAsyncByName(string name) =>
            await _artistCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<List<ArtistModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10)
        {
            var sortDefinition = sortOrder.Equals("asc")
            ? Builders<ArtistModel>.Sort.Ascending(x => x.Name)
            : Builders<ArtistModel>.Sort.Descending(x => x.Name);

            return await _artistCollection.Find(_ => true)
            .Sort(sortDefinition)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync() =>
            (int)await _artistCollection.CountDocumentsAsync(FilterDefinition<ArtistModel>.Empty);
    }
}