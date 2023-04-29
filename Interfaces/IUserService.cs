namespace LicentaApp.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAsync();
        Task<UserModel?> GetAsync(string UserName);
        Task CreateAsync(UserModel newUserModel);
        Task UpdateAsync(string UserName, UserModel updatedUserModel);
        Task RemoveAsync(string UserName);
    }
}
