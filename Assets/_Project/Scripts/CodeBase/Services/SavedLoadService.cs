using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistantProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SavedLoadService : ISavedLoadService
    {
        public const string PROGRESS_KEY = "Progress";

        private readonly IPersistentProgressService _progressService;
        public readonly IGameFactory _gameFactory;

        public SavedLoadService(IPersistentProgressService persistentProgressService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _progressService = persistentProgressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            PlayerPrefs.SetString(PROGRESS_KEY, _progressService.Progress.ToJson());
        }
        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserialized<PlayerProgress>();
        }
    }
}