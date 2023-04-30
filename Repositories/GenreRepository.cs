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

        public async Task<Tuple<List<GenreModel>, string>> IndexGenreList(string? name, string? sortOrder)
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
            var tuple = new Tuple<List<GenreModel>, string>(genreList, sortOrder);
            return tuple;
        }

        public async Task<Tuple<GenreModel, List<AlbumModel>>> IndexGenreName(string? name)
        {
            var genre = await _genreService.GetAsyncByName(name);
            var genreAlbums = await _albumService.GetAsyncListByGenre(name);
            var tuple = new Tuple<GenreModel, List<AlbumModel>>(genre, genreAlbums);
            return tuple;
        }
    }
}
