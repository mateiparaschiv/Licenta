using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserModel> _userCollection;

        public UserRepository(IMongoCollection<UserModel> userCollection)
        {
            _userCollection = userCollection;
        }

        public async Task<List<UserModel>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<UserModel?> GetAsync(string UserName) =>
            await _userCollection.Find(x => x.UserName == UserName).FirstOrDefaultAsync();

        public async Task CreateAsync(UserModel newUserModel) =>
            await _userCollection.InsertOneAsync(newUserModel);

        public async Task UpdateAsync(string UserName, UserModel updatedUserModel) =>
            await _userCollection.ReplaceOneAsync(x => x.UserName == UserName, updatedUserModel);

        public async Task RemoveAsync(string UserName) =>
            await _userCollection.DeleteOneAsync(x => x.UserName == UserName);
    }
}
