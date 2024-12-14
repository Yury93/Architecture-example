using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.InputService;
using CodeBase.Services.PersistantProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
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
            RegisterStaticData();
            _services.RegisterSingle<IRandomService>(new RandomService());
             
            _services.RegisterSingle<IInputService>(RegisterInputService());
            _services.RegisterSingle<IAsset>(new AssetProvider());

            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAsset>(), _services.Single<IStaticDataService>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAsset>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IRandomService>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<IWindowService>()));
            _services.RegisterSingle<ISavedLoadService>(new SavedLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
           
        

        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadMonsters();
            _services.RegisterSingle<IStaticDataService>(staticDataService);
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
