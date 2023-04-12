namespace LicentaApp.Interfaces
{
    public interface ISongService
    {
        Task<List<SongModel>> GetAsync();
        Task<SongModel?> GetAsync(string id);
        Task CreateAsync(SongModel newSongModel);
        Task UpdateAsync(string id, SongModel updatedSongModel);
        Task RemoveAsync(string id);
    }
}
