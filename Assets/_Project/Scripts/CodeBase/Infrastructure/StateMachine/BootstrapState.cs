using CodeBase.Services.InputService;
using System;
using UnityEngine;

namespace CodeBase.Infrastructer.StateMachine
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial,onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
           
        }

        private void RegisterServices()
        {
           Game.InputService = RegisterInputService();
        }

        public void Exit()
        {
             
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
