namespace LicentaApp.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMongoCollection<GenreModel> _genreCollection;

        public GenreService(IMongoCollection<GenreModel> genreCollection)
        {
            _genreCollection = genreCollection;
        }
        public async Task<List<GenreModel>> GetAsync() =>
             await _genreCollection.Find(_ => true).ToListAsync();

        public async Task<GenreModel?> GetAsync(string id) =>
            await _genreCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<GenreModel?> GetAsyncByName(string name) =>
            await _genreCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task CreateAsync(GenreModel newGenreModel) =>
            await _genreCollection.InsertOneAsync(newGenreModel);

        public async Task UpdateAsync(string id, GenreModel updatedGenreModel) =>
            await _genreCollection.ReplaceOneAsync(x => x.Id == id, updatedGenreModel);

        public async Task RemoveAsync(string id) =>
            await _genreCollection.DeleteOneAsync(x => x.Id == id);
    }
}
