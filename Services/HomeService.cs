using LicentaApp.Models.ViewModels;

namespace LicentaApp.Services
{
    public class HomeService : IHomeService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IAlbumRepository _albumRepository;

        public HomeService(IGenreRepository genreRepository, IAlbumRepository albumRepository)
        {
            _genreRepository = genreRepository;
            _albumRepository = albumRepository;
        }

        public async Task<IndexHomeViewModel> IndexHome()
        {
            return new IndexHomeViewModel
            {
                AlbumList = await _albumRepository.GetFilteredListByCompoundScore(count: 10),
                GenreList = await _genreRepository.GetAsync()
            };
        }
    }
}