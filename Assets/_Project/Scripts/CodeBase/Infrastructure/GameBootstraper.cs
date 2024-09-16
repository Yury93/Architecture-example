
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructer
{
    public class GameBootstraper : MonoBehaviour , ICoroutineRunner
    {
        public LoadingCurtain Curtain;
        private Game _game;
        private void Awake()
        {
            _game = new Game(this,Curtain);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        } 
    }
}