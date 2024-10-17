using CodeBase.Data;
using CodeBase.Services.PersistantProgress;
using CodeBase.Services.SaveLoad;
using System;
using System.Diagnostics;

namespace CodeBase.Infrastructer.StateMachine
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISavedLoadService _savedLoadService;
        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,  ISavedLoadService savedLoadService)
        { 
            this._gameStateMachine = gameStateMachine;
            this._progressService = progressService;    
            this._savedLoadService = savedLoadService;
        }
        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState,string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        private void LoadProgressOrInitNew()
        {
         
           PlayerProgress playerProgress = NewProgress();
            _progressService.Progress = _savedLoadService.LoadProgress() ?? playerProgress;
            UnityEngine.Debug.Log("Load  = " + playerProgress.WorldData.PositionOnLevel.position);
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress(initialLevel: "Game"); 
        }

        public void Exit()
        {
         
        }
    }
}