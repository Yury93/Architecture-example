
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Logic;
using CodeBase.Services.InputService;
using UnityEngine;

namespace CodeBase.Infrastructer
{
    public class Game
    { 
        public  GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(sceneLoader: new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
     
    }
}