using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IAlbumService
    {
        Task<IndexAlbumListViewModel> IndexAlbumList(string sortOrder);
        Task<IndexAlbumNameViewModel> AlbumName(string name);
        Task CreateAsync(AlbumModel newAlbumModel);
    }
}
