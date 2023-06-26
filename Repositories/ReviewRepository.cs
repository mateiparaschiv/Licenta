using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMongoCollection<ReviewModel> _reviewCollection;

        public ReviewRepository(IMongoCollection<ReviewModel> reviewCollection)
        {
            _reviewCollection = reviewCollection;
        }

        public async Task CreateAsync(ReviewModel newReviewModel) =>
            await _reviewCollection.InsertOneAsync(newReviewModel);

        public async Task<List<ReviewModel>> GetAsync() =>
            await _reviewCollection.Find(_ => true).ToListAsync();

        public async Task<List<ReviewModel>> GetAsyncListByAlbum(string albumName) =>
            await _reviewCollection.Find(x => x.Subject == albumName).ToListAsync();
    }
}