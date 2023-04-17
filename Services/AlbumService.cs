namespace LicentaApp.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IMongoCollection<AlbumModel> _albumCollection;

        public AlbumService(IMongoCollection<AlbumModel> albumCollection)
        {
            _albumCollection = albumCollection;
        }

        public async Task<List<AlbumModel>> GetAsync() =>
            await _albumCollection.Find(_ => true).ToListAsync();

        public async Task<AlbumModel?> GetAsyncById(string id) =>
            await _albumCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<int?> GetNumOfAlbumsByName(string artistName)
        {
            var albums = await GetAsyncListByName(artistName);
            return albums.Count();
        }
        public async Task<AlbumModel?> GetAsyncByName(string name) =>
            await _albumCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        public async Task<List<AlbumModel>> GetAsyncListByName(string artistName) =>
            await _albumCollection.Find(x => x.Artist == artistName).ToListAsync();
        public async Task<List<AlbumModel>> GetAsyncListByGenre(string genre) =>
            await _albumCollection.Find(x => x.Genre == genre).ToListAsync();

        public async Task<Dictionary<string, int>> GetNumOfAlbumsByNames(List<ArtistModel> artistList)
        {
            Dictionary<string, int> albumsToArtist = new Dictionary<string, int>();
            foreach (ArtistModel artist in artistList)
            {
                var artistAlbums = await GetAsyncListByName(artist.Name);
                var numOfAlbums = artistAlbums.Count();
                albumsToArtist.Add(artist.Name, numOfAlbums);
            }
            return albumsToArtist;
        }

        public async Task CreateAsync(AlbumModel newAlbumModel) =>
            await _albumCollection.InsertOneAsync(newAlbumModel);

        public async Task UpdateAsync(string id, AlbumModel updatedAlbumModel) =>
            await _albumCollection.ReplaceOneAsync(x => x.Id == id, updatedAlbumModel);

        public async Task RemoveAsync(string id) =>
            await _albumCollection.DeleteOneAsync(x => x.Id == id);
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
