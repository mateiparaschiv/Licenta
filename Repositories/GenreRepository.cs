using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IGenreService _genreService;
        private readonly IAlbumService _albumService;
        public GenreRepository(IGenreService genreService, IAlbumService albumService)
        {
            _genreService = genreService;
            _albumService = albumService;
        }

        public async Task<IndexGenreListViewModel> IndexGenreList(string? sortOrder)
        {
            List<GenreModel> genreList;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;

            switch (sortOrder)
            {
                case "desc":
                    genreList = await _genreService.GetAsyncListDescending();
                    break;
                default: // default is ascending
                    genreList = await _genreService.GetAsyncListAscending();
                    break;
            }

            IndexGenreListViewModel indexGenreListViewModel = new IndexGenreListViewModel
            {
                GenreList = genreList,
                SortOrder = sortOrder
            };
            return indexGenreListViewModel;
        }

        public async Task<IndexGenreNameViewModel> GenreName(string name)
        {
            var genre = await _genreService.GetAsyncByName(name);
            var genreAlbums = await _albumService.GetAsyncListByGenre(name);
            IndexGenreNameViewModel indexGenreNameViewModel = new IndexGenreNameViewModel
            {
                Genre = genre,
                GenreAlbums = genreAlbums
            };
            return indexGenreNameViewModel;
        }
    }
}
