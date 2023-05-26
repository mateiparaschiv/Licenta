﻿using LicentaApp.Interfaces.IService;

namespace LicentaApp.Services
{
    public class SongService : ISongService
    {
        private readonly IMongoCollection<SongModel> _songCollection;

        public SongService(IMongoCollection<SongModel> songCollection)
        {
            _songCollection = songCollection;
        }

        public async Task<List<SongModel>> GetAsync() =>
            await _songCollection.Find(_ => true).ToListAsync();

        public async Task<SongModel?> GetAsync(string id) =>
            await _songCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(SongModel newSongModel) =>
            await _songCollection.InsertOneAsync(newSongModel);

        public async Task UpdateAsync(string id, SongModel updatedSongModel) =>
            await _songCollection.ReplaceOneAsync(x => x.Id == id, updatedSongModel);

        public async Task RemoveAsync(string id) =>
            await _songCollection.DeleteOneAsync(x => x.Id == id);
    }
}
