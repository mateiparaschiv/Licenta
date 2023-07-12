using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IAlbumService
    {
        Task<IndexAlbumListViewModel> IndexAlbumList(string sortOrder, int pageNumber);
        Task<IndexAlbumNameViewModel> AlbumName(string name);
        Task CreateAsync(AlbumModel newAlbumModel);
    }
}
