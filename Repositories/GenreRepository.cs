using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IMongoCollection<GenreModel> _genreCollection;

        public GenreRepository(IMongoCollection<GenreModel> genreCollection)
        {
            _genreCollection = genreCollection;
        }

        public async Task<GenreModel> GetAsyncByName(string name) =>
            await _genreCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        public async Task<List<GenreModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10)
        {
            var sortDefinition = sortOrder.Equals("asc")
                ? Builders<GenreModel>.Sort.Ascending(x => x.Name)
                : Builders<GenreModel>.Sort.Descending(x => x.Name);

            return await _genreCollection.Find(_ => true)
                .Sort(sortDefinition)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync() =>
            (int)await _genreCollection.CountDocumentsAsync(FilterDefinition<GenreModel>.Empty);
    }
}
