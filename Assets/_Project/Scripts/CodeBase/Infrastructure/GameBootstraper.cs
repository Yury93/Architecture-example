
using CodeBase.Infrastructer.StateMachine;
using UnityEngine;

namespace CodeBase.Infrastructer
{
    public class GameBootstraper : MonoBehaviour , ICoroutineRunner
    {
        Game _game;
        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        } 
    }
}