using LicentaApp.Models.ViewModels.ArtistViewModel;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IArtistService
    {
        Task<IndexArtistListViewModel> IndexArtistList(string sortOrder, int pageNumber);
        Task<IndexArtistNameViewModel> ArtistsName(string name, string sortOrder);
        Task<IndexArtistSentimentListViewModel> ArtistsSentiment(string sentiment, string sortOrder, int pageNumber);
        Task<IndexArtistFormationListViewModel> ArtistsFormation(bool band, string sortOrder, int pageNumber);
    }
}
