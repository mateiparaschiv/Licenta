using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IArtistRepository
    {
        Task<IndexArtistListViewModel> IndexArtistList(string? sortOrder, int pageNumber);
        Task<IndexArtistNameViewModel> ArtistName(string name, string? sortOrder);
    }
}
