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

        public async Task<ArtistModel> GetArtistByName(string name) =>
            await _artistCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, ArtistModel updatedArtistModel) =>
            await _artistCollection.ReplaceOneAsync(x => x.Id == id, updatedArtistModel);

        public async Task<List<ArtistModel>> GetAsyncFilteredByName()
        {
            var sortDefinition = Builders<ArtistModel>.Sort.Ascending(x => x.Name);
            return await _artistCollection.Find(x => true).Sort(sortDefinition).ToListAsync();
        }

        public async Task<List<ArtistModel>> GetPaginatedFilteredList(string sortOrder, int pageNumber = 0, int pageSize = 10, string? sentiment = null, bool? band = null)
        {
            var sortDefinition = sortOrder.Equals("asc")
                ? Builders<ArtistModel>.Sort.Ascending(x => x.Name)
                : Builders<ArtistModel>.Sort.Descending(x => x.Name);

            var filterDefinitionBuilder = Builders<ArtistModel>.Filter;
            var filterDefinition = filterDefinitionBuilder.Empty;

            if (!string.IsNullOrEmpty(sentiment))
            {
                var sentimentFilter = filterDefinitionBuilder.Eq(a => a.Sentiment, sentiment);
                filterDefinition = filterDefinition & sentimentFilter;
            }

            if (band.HasValue)
            {
                var bandFilter = filterDefinitionBuilder.Eq(a => a.Band, band.Value);
                filterDefinition = filterDefinition & bandFilter;
            }

            return await _artistCollection.Find(filterDefinition)
                .Sort(sortDefinition)
                .Skip(pageNumber * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? sentiment = null, bool? band = null)
        {
            var filterDefinitionBuilder = Builders<ArtistModel>.Filter;
            var filterDefinition = filterDefinitionBuilder.Empty;

            if (!string.IsNullOrEmpty(sentiment))
            {
                var sentimentFilter = filterDefinitionBuilder.Eq(a => a.Sentiment, sentiment);
                filterDefinition = filterDefinition & sentimentFilter;
            }

            if (band.HasValue)
            {
                var bandFilter = filterDefinitionBuilder.Eq(a => a.Band, band.Value);
                filterDefinition = filterDefinition & bandFilter;
            }

            return (int)await _artistCollection.CountDocumentsAsync(filterDefinition);
        }

        public async Task UpdateArtistAsync(string artistName, double compoundScore)
        {
            var filter = Builders<ArtistModel>.Filter.Eq(x => x.Name, artistName);

            var artist = await _artistCollection.Find(filter).FirstOrDefaultAsync();

            // Calculate the new mean compound score based on the current review count and the new compound score
            var reviewCount = artist?.ReviewCount ?? 0;
            var currentCompoundScore = artist?.CompoundScore ?? 0.0;

            var newCompoundScore = (currentCompoundScore * reviewCount + compoundScore) / (reviewCount + 1);

            // Determine the sentiment based on the new compound score
            string newSentiment = GetSentiment(newCompoundScore);

            var update = Builders<ArtistModel>.Update
                .Inc(x => x.ReviewCount, 1)
                .Set(x => x.CompoundScore, newCompoundScore)
                .Set(x => x.Sentiment, newSentiment);

            await _artistCollection.UpdateOneAsync(filter, update);
        }
        public async Task<List<ArtistModel>> GetFilteredListByCompoundScore(int? count)
        {
            var sortDefinition = Builders<ArtistModel>.Sort.Descending(x => x.CompoundScore);
            var query = _artistCollection.Find(_ => true).Sort(sortDefinition);

            if (count.HasValue)
            {
                query = query.Limit(count);
            }

            return await query.ToListAsync();
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