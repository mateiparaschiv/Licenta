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

        public async Task<GenreModel?> GetAsyncByName(string name) =>
            await _genreCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<List<GenreModel>> GetAsyncListAscending() =>
            await _genreCollection.Find(_ => true).SortBy(x => x.Name).ToListAsync();

        public async Task<List<GenreModel>> GetAsyncListDescending() =>
             await _genreCollection.Find(_ => true).SortByDescending(x => x.Name).ToListAsync();

        public async Task<List<GenreModel>> GetFilteredListByName(string sortOrder)
        {
            sortOrder = sortOrder?.ToLower() ?? "asc";
            var sortDefinition = sortOrder == "asc"
                ? Builders<GenreModel>.Sort.Ascending(x => x.Name)
                : Builders<GenreModel>.Sort.Descending(x => x.Name);

            return await _genreCollection.Find(_ => true).Sort(sortDefinition).ToListAsync();
        }
    }
}
