
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructer
{
    public class GameBootstraper : MonoBehaviour , ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;
        private Game _game;
        private void Awake()
        {

            _game = new Game(this,Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        } 
    }
}