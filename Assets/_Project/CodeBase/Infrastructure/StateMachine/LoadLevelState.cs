using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructer.StateMachine
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string InitialPointTag = "InitialPoint"; 
        private const string HudPath = "Controls/HUD";
        private const string ENEMY_SPAWNER_TAG = "EnemySpawner";
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        public LoadLevelState(GameStateMachine stateMachine
            , SceneLoader sceneLoader, 
            LoadingCurtain curtain, 
            IGameFactory gameFactory, 
            IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService)
        {
            this._stateMachine = stateMachine;
            this._sceneLoader = sceneLoader;
            this._curtain = curtain;
            this._gameFactory = gameFactory;
            _progressService = persistentProgressService;
            _staticDataService = staticDataService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }
        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnSceneLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }
        private void InitGameWorld()
        {
            InitSpawners();

            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            CameraFollow(hero);
            InitHud(hero);
        } 
        private void InitSpawners()
        {
            //foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(ENEMY_SPAWNER_TAG))
            //{
            //   var spawner = spawnerObject.GetComponent<EnemySpawner>();
            //    _gameFactory.Register(spawner);
            //    var lootSpawner = spawnerObject.GetComponent<Logic.LootSpawner>();
            //    _gameFactory.Register(lootSpawner);
            //}

            string sceneKey = SceneManager.GetActiveScene().name;
           LevelStaticData levelData = _staticDataService.ForLevel(sceneKey);
            foreach (var enemySpawner in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(enemySpawner.Position, enemySpawner.Id, enemySpawner.MonsterTypeId);
            }
        }

        private void InitHud(GameObject hero)
        {
           GameObject hud = _gameFactory.InstatiateHUD();
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
