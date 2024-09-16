
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Services.InputService;
using UnityEngine;

namespace CodeBase.Infrastructer
{
    public class Game
    {
        public static IInputService InputService;
        public  GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(sceneLoader: new SceneLoader(coroutineRunner));
        }
     
    }
}