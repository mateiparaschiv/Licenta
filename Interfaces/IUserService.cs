namespace LicentaApp.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAsync();
        Task<UserModel?> GetAsync(string id);
        Task CreateAsync(UserModel newUserModel);
        Task UpdateAsync(string id, UserModel updatedUserModel);
        Task RemoveAsync(string id);
    }
}
