namespace LicentaApp.Interfaces.IRepository
{
    public interface IAlbumRepository
    {
        Task<Tuple<List<AlbumModel>, string>> IndexAlbumList(string? name, string? sortOrder);
        Task<Tuple<AlbumModel, List<ReviewModel>>> IndexAlbumName(string? name);
    }
}
