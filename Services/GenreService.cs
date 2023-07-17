using LicentaApp.Models.ViewModels.GenreViewModel;

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

        public async Task<IndexGenreListViewModel> IndexGenreList(string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 5;
            int totalGenres = await _genreRepository.GetTotalCountAsync();
            int maxPages = (totalGenres + pageSize - 1) / pageSize;

            return new IndexGenreListViewModel
            {
                AllGenreList = await _genreRepository.GetAsyncFilteredByName(),
                GenreList = await _genreRepository.GetPaginatedFilteredList(sortOrder, pageNumber, pageSize),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages
            };
        }

        public async Task<IndexGenreNameViewModel> GenreName(string name)
        {
            return new IndexGenreNameViewModel
            {
                Genre = await _genreRepository.GetAsyncByName(name),
                GenreAlbums = await _albumRepository.GetFilteredListByCompoundScore(count: 9, albumGenre: name)
            };
        }
    }
}