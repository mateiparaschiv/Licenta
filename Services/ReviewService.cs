using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMongoCollection<ReviewModel> _reviewCollection;

        public ReviewService(IMongoCollection<ReviewModel> reviewCollection)
        {
            _reviewCollection = reviewCollection;
        }
        public async Task<List<ReviewModel>> GetAsync() =>
            await _reviewCollection.Find(_ => true).ToListAsync();

        public async Task<ReviewModel?> GetAsync(string id) =>
            await _reviewCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ReviewModel newReviewModel) =>
            await _reviewCollection.InsertOneAsync(newReviewModel);

        public async Task UpdateAsync(string id, ReviewModel updatedReviewModel) =>
            await _reviewCollection.ReplaceOneAsync(x => x.Id == id, updatedReviewModel);

        public async Task RemoveAsync(string id) =>
            await _reviewCollection.DeleteOneAsync(x => x.Id == id);
        public async Task<List<ReviewModel>> GetAsyncListByAlbum(string albumName) =>
            await _reviewCollection.Find(x => x.Subject == albumName).ToListAsync();
    }
}
