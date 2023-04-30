namespace LicentaApp.Interfaces.IRepository
{
    public interface IGenreRepository
    {
        Task<Tuple<List<GenreModel>, string>> IndexGenreList(string? name, string? sortOrder);
        Task<Tuple<GenreModel, List<AlbumModel>>> IndexGenreName(string? name);
    }
}
