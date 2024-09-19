using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
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
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            this._stateMachine = stateMachine;
            this._sceneLoader = sceneLoader;
            this._curtain = curtain;
            this._gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        { 
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            CameraFollow(hero);
            _gameFactory.InstatiateHUD();

            _stateMachine.Enter<GameLoopState>();
        }
        private void CameraFollow(GameObject follow) =>
     Camera.main.GetComponent<CameraFollow>().Follow(follow);
    }
}
