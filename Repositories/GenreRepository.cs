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
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
            var genreList = await _genreService.GetAsync();
            switch (sortOrder)
            {
                case "name_asc":
                    genreList.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case "name_desc":
                    genreList.Sort((x, y) => string.Compare(y.Name, x.Name));
                    break;
                case "":
                    break;
            }
            IndexGenreListViewModel indexGenreListViewModel = new IndexGenreListViewModel
            {
                GenreList = genreList,
                SortOrder = sortOrder
            };
            return indexGenreListViewModel;
        }

        public async Task<IndexGenreNameViewModel> IndexGenreName(string name)
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
