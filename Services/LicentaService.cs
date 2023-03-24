using LicentaApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LicentaApp.Services
{
    public class LicentaService
    //MusicService ?
    {
        private readonly IMongoCollection<UserModel> _userCollection;

        public LicentaService(
            IOptions<LicentaDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<UserModel>(
                bookStoreDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<UserModel>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<UserModel?> GetAsync(string id) =>
            await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserModel newUserModel) =>
            await _userCollection.InsertOneAsync(newUserModel);

        public async Task UpdateAsync(string id, UserModel updatedUserModel) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUserModel);

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.Id == id);
    }
}
