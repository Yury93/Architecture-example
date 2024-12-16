using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructer.StateMachine
{
    public class LoadLevelState : IPayLoadedState<string>
    {
 
        private const string ENEMY_SPAWNER_TAG = "EnemySpawner";
        private const string HUD_PATH = "Controls/HUD"; 
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine
            , SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService,
            IUIFactory uIFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = persistentProgressService;
            _staticDataService = staticDataService;
            _uiFactory = uIFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _gameFactory.Warmup();
            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }
        public void Exit()
        {
            _curtain.Hide();
        }

        private async void OnSceneLoaded()
        {
            InitLoadRoot();
            await InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitLoadRoot()
        {
            _uiFactory.CreateUIRoot();
        }

        private async Task InitGameWorld()
        {
            LevelStaticData levelData = GetLevelStaticData();
            await InitSpawners(levelData);

            GameObject hero = await _gameFactory.CreateHeroAsync(levelData);
            CameraFollow(hero);
             await InitHud(hero);
        }

        private LevelStaticData GetLevelStaticData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.ForLevel(sceneKey);
            return levelData;
        }

        private async Task InitSpawners(LevelStaticData levelData)
        {
           
            foreach (var enemySpawner in levelData.EnemySpawners)
            {
               SpawnPoint spawnPoint = await _gameFactory.CreateSpawnerAsync(enemySpawner.Position, enemySpawner.Id, enemySpawner.MonsterTypeId);
               await _gameFactory.CreateLootSpawnerAsync(spawnPoint.transform.position, spawnPoint.Id,enemySpawner.MonsterTypeId,spawnPoint);

            }
        }

        private async Task InitHud(GameObject hero)
        {
           GameObject hud = await _gameFactory.InstatiateHUDAsync();
           hud.GetComponentInChildren<ActorUI>()
                .Construct(hero.GetComponent<HeroHealth>());
        }
        private void InformProgressReaders()
        { 
            foreach (ISavedProgressReader progressReader in _gameFactory.progressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }


        private void CameraFollow(GameObject follow) =>
     Camera.main.GetComponent<CameraFollow>().Follow(follow);
    }
}
