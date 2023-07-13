using LicentaApp.Models;
namespace LicentaApp.Interfaces.IService
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAsync();
        Task<UserModel> GetAsync(string UserName);
        Task CreateAsync(UserModel newUserModel);
        Task UpdateAsync(string UserName, UserModel updatedUserModel);
        Task RemoveAsync(string UserName);
    }
}
