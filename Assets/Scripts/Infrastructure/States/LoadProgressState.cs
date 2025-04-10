using System;
using System.Collections.Generic;
using Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private const string Solitaire = "Solitaire";

        public LoadProgressState(GameStateMachine gameStateMachine, 
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadGameState, string>(Solitaire);
        }
        
        public void Exit() { }
        
        private void LoadProgressOrInitNew() => 
            _progressService.GameProgress = 
                _saveLoadService.LoadProgress() 
                ?? NewProgress();

        private GameProgress NewProgress() => new(new List<string>());
    }
}