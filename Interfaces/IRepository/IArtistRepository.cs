using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IArtistRepository
    {
        Task<IndexArtistListViewModel> IndexArtistList(string? name, string? sortOrder, int pageNumber);
        Task<IndexArtistNameViewModel> IndexArtistName(string? name, string? sortOrder);
    }
}
