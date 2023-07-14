using LicentaApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LicentaApp.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IMongoCollection<AlbumModel> _albumCollection;

        public AlbumRepository(IMongoCollection<AlbumModel> albumCollection)
        {
            _albumCollection = albumCollection;
        }

        public async Task<List<AlbumModel>> GetAsync() =>
            await _albumCollection.Find(_ => true).ToListAsync();

        public async Task<AlbumModel> GetAlbumByName(string name) =>
            await _albumCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<List<AlbumModel>> GetListByArtist(string artistName) =>
            await _albumCollection.Find(x => x.Artist == artistName).ToListAsync();

        public async Task<List<AlbumModel>> GetListByGenre(string genre) =>
            await _albumCollection.Find(x => x.Genre == genre).ToListAsync();

        public async Task<Dictionary<string, int>> GetNumOfAlbumsByNames(List<ArtistModel> artistList)
        {
            Dictionary<string, int> albumsToArtist = new Dictionary<string, int>();
            foreach (ArtistModel artist in artistList)
            {
                var artistAlbums = await GetListByArtist(artist.Name);
                var numOfAlbums = artistAlbums.Count();
                albumsToArtist.Add(artist.Name, numOfAlbums);
            }
            return albumsToArtist;
        }

        public async Task CreateAsync(AlbumModel newAlbumModel) =>
            await _albumCollection.InsertOneAsync(newAlbumModel);
        public async Task<List<AlbumModel>> GetFilteredListByArtist(string artistName, string sortOrder)
        {
            var filter = Builders<AlbumModel>.Filter.Eq(x => x.Artist, artistName);
            var sortDefinition = sortOrder == "asc"
                ? Builders<AlbumModel>.Sort.Ascending(x => x.Year)
                : Builders<AlbumModel>.Sort.Descending(x => x.Year);

            return await _albumCollection.Find(filter).Sort(sortDefinition).ToListAsync();
        }

        public async Task<List<AlbumModel>> GetFilteredListByName(string sortOrder)
        {
            sortOrder = sortOrder?.ToLower() ?? "asc";
            var sortDefinition = sortOrder == "asc"
                ? Builders<AlbumModel>.Sort.Ascending(x => x.Name)
                : Builders<AlbumModel>.Sort.Descending(x => x.Name);

            return await _albumCollection.Find(_ => true).Sort(sortDefinition).ToListAsync();
        }

        public async Task<List<AlbumModel>> GetRandomAlbums(int count)
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$sample", new BsonDocument("size", count))
            };

            var albums = await _albumCollection.Aggregate<AlbumModel>(pipeline).ToListAsync();
            return albums;
        }

        public async Task<List<AlbumModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10)
        {
            var sortDefinition = sortOrder.Equals("asc")
                ? Builders<AlbumModel>.Sort.Ascending(x => x.Name)
                : Builders<AlbumModel>.Sort.Descending(x => x.Name);

            return await _albumCollection.Find(_ => true)
                .Sort(sortDefinition)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync() =>
            (int)await _albumCollection.CountDocumentsAsync(FilterDefinition<AlbumModel>.Empty);
        public async Task UpdateAlbumAsync(string albumName, double compoundScore)
        {
            var filter = Builders<AlbumModel>.Filter.Eq(x => x.Name, albumName);

            var album = await _albumCollection.Find(filter).FirstOrDefaultAsync();

            // Calculate the new mean compound score based on the current review count and the new compound score
            var reviewCount = album?.ReviewCount ?? 0;
            var currentCompoundScore = album?.CompoundScore ?? 0.0;

            var newCompoundScore = (currentCompoundScore * reviewCount + compoundScore) / (reviewCount + 1);

            // Determine the sentiment based on the new compound score
            string newSentiment = GetSentiment(newCompoundScore);

            var update = Builders<AlbumModel>.Update
                .Inc(x => x.ReviewCount, 1)
                .Set(x => x.CompoundScore, newCompoundScore)
                .Set(x => x.Sentiment, newSentiment);

            await _albumCollection.UpdateOneAsync(filter, update);
        }

        private string GetSentiment(double compoundScore)
        {
            if (compoundScore >= 0.5)
            {
                return "Positive";
            }
            else if (compoundScore > -0.5 && compoundScore < 0.5)
            {
                return "Neutral";
            }
            else
            {
                return "Negative";
            }
        }
    }
}