namespace LicentaApp.Interfaces.IRepository
{
    public interface IArtistRepository
    {
        Task<Tuple<ArtistModel, List<AlbumModel>, string, List<ReviewModel>>> IndexArtistName(string? name, string? sortOrder);
        Task<Tuple<PaginatedListModel<ArtistModel>, Dictionary<string, int>, string>> IndexArtistList(string? name, string? sortOrder, int? pageNumber);
    }
}
