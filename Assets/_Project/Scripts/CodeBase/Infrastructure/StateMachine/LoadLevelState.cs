using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistantProgress;
using System;
using UnityEngine;

namespace CodeBase.Infrastructer.StateMachine
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string InitialPointTag = "InitialPoint"; 
        private const string HudPath = "Controls/HUD";
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
        {
            this._stateMachine = stateMachine;
            this._sceneLoader = sceneLoader;
            this._curtain = curtain;
            this._gameFactory = gameFactory;
            _progressService = persistentProgressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }
        private void InitGameWorld()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            CameraFollow(hero);
            _gameFactory.InstatiateHUD();
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
