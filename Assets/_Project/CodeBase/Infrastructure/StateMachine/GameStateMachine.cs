using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.Services.SaveLoad;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructer.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services) 
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, 
                services.Single<IGameFactory>(),
                services.Single<IPersistentProgressService>(),
                services.Single<IStaticDataService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISavedLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader) 
            };
        }
        public void Enter<TState>() where TState : class,IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState ChangeState<TState>() where TState: class,IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        } 
        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
