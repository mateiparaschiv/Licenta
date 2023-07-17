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

        public async Task<List<AlbumModel>> GetAsyncFilteredByName()
        {
            var sortDefinition = Builders<AlbumModel>.Sort.Ascending(album => album.Name);
            return await _albumCollection.Find(album => true).Sort(sortDefinition).ToListAsync();
        }
        public async Task UpdateAsync(string id, AlbumModel updatedAlbumModel) =>
            await _albumCollection.ReplaceOneAsync(x => x.Id == id, updatedAlbumModel);
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

        public async Task<List<AlbumModel>> GetPaginatedFilteredList(string sortOrder, int? year = null, string? genre = null, string? sentiment = null, int pageNumber = 0, int pageSize = 10)
        {
            var sortDefinition = sortOrder.Equals("asc")
                ? Builders<AlbumModel>.Sort.Ascending(x => x.Name)
                : Builders<AlbumModel>.Sort.Descending(x => x.Name);

            var filterDefinitionBuilder = Builders<AlbumModel>.Filter;
            var filterDefinition = filterDefinitionBuilder.Empty;

            if (year.HasValue && year.Value != 0)
            {
                var yearFilter = filterDefinitionBuilder.Eq(a => a.Year, year.Value);
                filterDefinition = filterDefinition & yearFilter;
            }

            if (!string.IsNullOrEmpty(genre))
            {
                var genreFilter = filterDefinitionBuilder.Eq(a => a.Genre, genre);
                filterDefinition = filterDefinition & genreFilter;
            }

            if (!string.IsNullOrEmpty(sentiment))
            {
                var sentimentFilter = filterDefinitionBuilder.Eq(a => a.Sentiment, sentiment);
                filterDefinition = filterDefinition & sentimentFilter;
            }

            return await _albumCollection.Find(filterDefinition)
                .Sort(sortDefinition)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(int? year = null, string? genre = null, string? sentiment = null)
        {
            var filters = new List<FilterDefinition<AlbumModel>>();

            if (year.HasValue)
            {
                filters.Add(Builders<AlbumModel>.Filter.Eq(x => x.Year, year));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                filters.Add(Builders<AlbumModel>.Filter.Eq(x => x.Genre, genre));
            }

            if (!string.IsNullOrEmpty(sentiment))
            {
                filters.Add(Builders<AlbumModel>.Filter.Eq(x => x.Sentiment, sentiment));
            }

            var filter = filters.Any()
                ? Builders<AlbumModel>.Filter.And(filters)
                : FilterDefinition<AlbumModel>.Empty;

            return (int)await _albumCollection.CountDocumentsAsync(filter);
        }

        public async Task UpdateAlbumAsync(string albumName, double compoundScore, string reviewSentiment)
        {
            var filter = Builders<AlbumModel>.Filter.Eq(x => x.Name, albumName);

            var album = await _albumCollection.Find(filter).FirstOrDefaultAsync();

            // Calculate the new mean compound score based on the current review count and the new compound score
            var reviewCount = album?.ReviewCount ?? 0;
            var currentCompoundScore = album?.CompoundScore ?? 0.0;

            var newCompoundScore = (currentCompoundScore * reviewCount + compoundScore) / (reviewCount + 1);

            // Determine the sentiment based on the new compound score
            string newSentiment = GetSentiment(newCompoundScore);

            if (reviewCount == 0)
            {
                newSentiment = reviewSentiment;
            }

            var update = Builders<AlbumModel>.Update
                .Inc(x => x.ReviewCount, 1)
                .Set(x => x.CompoundScore, newCompoundScore)
                .Set(x => x.Sentiment, newSentiment);

            await _albumCollection.UpdateOneAsync(filter, update);
        }

        public async Task<List<AlbumModel>> GetFilteredListByCompoundScore(int? count = 0, string? albumGenre = null, string? albumArtist = null)
        {
            var sortDefinition = Builders<AlbumModel>.Sort.Descending(x => x.CompoundScore);

            FilterDefinition<AlbumModel> filter = Builders<AlbumModel>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(albumGenre))
            {
                filter = filter & Builders<AlbumModel>.Filter.Eq(x => x.Genre, albumGenre);
            }

            if (!string.IsNullOrWhiteSpace(albumArtist))
            {
                filter = filter & Builders<AlbumModel>.Filter.Eq(x => x.Artist, albumArtist);
            }

            var query = _albumCollection.Find(filter).Sort(sortDefinition);

            if (count.HasValue)
            {
                query = query.Limit(count);
            }

            return await query.ToListAsync();
        }
        public async Task<List<int>> GetDistinctYearsAsync()
        {
            var filter = Builders<AlbumModel>.Filter.Empty;
            var projection = Builders<AlbumModel>.Projection.Include(a => a.Year);
            var years = await _albumCollection.Find(filter).Project<AlbumModel>(projection).ToListAsync();

            return years.Select(a => a.Year).Distinct().OrderByDescending(y => y).ToList();
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