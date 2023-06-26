using LicentaApp.Models.ViewModels;

namespace LicentaApp.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IAlbumRepository _albumRepository;
        public GenreService(IGenreRepository genreRepository, IAlbumRepository albumRepository)
        {
            _genreRepository = genreRepository;
            _albumRepository = albumRepository;
        }

        public async Task<IndexGenreListViewModel> IndexGenreList(string? sortOrder)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            return new IndexGenreListViewModel
            {
                GenreList = await _genreRepository.GetFilteredListByName(sortOrder),
                SortOrder = sortOrder
            };
        }

        public async Task<IndexGenreNameViewModel> GenreName(string name)
        {
            return new IndexGenreNameViewModel
            {
                Genre = await _genreRepository.GetAsyncByName(name),
                GenreAlbums = await _albumRepository.GetListByGenre(name)
            };
        }
    }
}
