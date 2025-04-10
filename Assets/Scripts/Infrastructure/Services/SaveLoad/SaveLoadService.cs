using Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private const string ProgressKey = "Progress";


        public SaveLoadService(IPersistentProgressService progressService ,IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters) 
                progressWriter.UpdateProgress(_progressService.GameProgress);
            PlayerPrefs.SetString(ProgressKey, _progressService.GameProgress.ToJson());
        }

        public GameProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<GameProgress>();
    }
}