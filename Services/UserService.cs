using LicentaApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LicentaApp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserModel> _userCollection;

        public UserService(
            IOptions<DatabaseSettingsModel> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<UserModel>(
                databaseSettings.Value.);
            //clasa pentru fiecare obiect ?
            //cum fac sa folosesc aceeasi clasa si sa trag collection diferit ?
        }

        public async Task<List<UserModel>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<UserModel?> GetAsync(string id) =>
            await _userCollection.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserModel newUserModel) =>
            await _userCollection.InsertOneAsync(newUserModel);

        public async Task UpdateAsync(string id, UserModel updatedUserModel) =>
            await _userCollection.ReplaceOneAsync(x => x.id == id, updatedUserModel);

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.id == id);
    }
}
