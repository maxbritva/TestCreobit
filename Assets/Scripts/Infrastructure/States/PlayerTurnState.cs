using System;
using Infrastructure.Services.Input;
using Logic.GameBoard;
using Logic.Interactive;

namespace Infrastructure.States
{
    public class PlayerTurnState : IState, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IDragCardService _dragCardService;
        private readonly IInputService _inputService;
        private IGameBoardLogicService _boardLogic;

        public PlayerTurnState(GameStateMachine gameStateMachine, IInputService inputService, 
            IDragCardService dragCardService, IGameBoardLogicService boardLogic)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _dragCardService = dragCardService;
            _boardLogic = boardLogic;
            _dragCardService.OnCurrentCardChanged += CurrentCardChanged;
            _boardLogic.OnCurrentCardChanged += CurrentCardChanged;
        }

        public void Dispose()
        {
            _dragCardService.OnCurrentCardChanged -= CurrentCardChanged;
            _boardLogic.OnCurrentCardChanged -= CurrentCardChanged;
        }

        public void Enter() => _inputService.IsEnabledInputs(true);

        public void Exit() => _inputService.IsEnabledInputs(false);

        private void CurrentCardChanged() => _gameStateMachine.Enter<GameProcessState>();
    }
}