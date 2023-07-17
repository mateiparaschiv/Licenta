using LicentaApp.Models;
using LicentaApp.Models.ViewModels.AlbumViewModel;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IAlbumService
    {
        Task<IndexAlbumListViewModel> IndexAlbumList(string sortOrder, int pageNumber);
        Task<IndexAlbumNameViewModel> AlbumName(string name);
        Task CreateAsync(AlbumModel newAlbumModel);
        Task<IndexAlbumYearListViewModel> AlbumsYear(int year, string sortOrder, int pageNumber);
        Task<IndexAlbumGenreListViewModel> AlbumsGenre(string genre, string sortOrder, int pageNumber);
        Task<IndexAlbumSentimentListViewModel> AlbumsSentiment(string sentiment, string sortOrder, int pageNumber);
    }
}