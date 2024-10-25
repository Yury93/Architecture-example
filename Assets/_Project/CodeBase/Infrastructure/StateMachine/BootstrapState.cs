using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.InputService;
using CodeBase.Services.PersistantProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructer.StateMachine
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly AllServices _services;
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader; 
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        { 
            _sceneLoader.Load(Initial,onLoaded: EnterLoadLevel);
        }
        public void Exit()
        {

        }
        private void EnterLoadLevel() => _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(RegisterInputService());
            _services.RegisterSingle<IAsset>(new AssetProvider()); 
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAsset>()));
            _services.RegisterSingle<ISavedLoadService>(new SavedLoadService(_services.Single<IPersistentProgressService>(),_services.Single<IGameFactory>()));
            
        }
        private static InputService RegisterInputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }
    }
}
