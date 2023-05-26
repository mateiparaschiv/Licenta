using LicentaApp.Models.ViewModels;

namespace LicentaApp.Interfaces.IRepository
{
    public interface IAlbumRepository
    {
        Task<IndexAlbumListViewModel> IndexAlbumList(string? sortOrder);
        Task<IndexAlbumNameViewModel> IndexAlbumName(string name);
    }
}
