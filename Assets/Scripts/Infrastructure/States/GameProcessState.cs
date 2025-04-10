using Infrastructure.Deck;
using Logic.GameBoard;
using Logic.GameProgress;
using Logic.Interactive;

namespace Infrastructure.States
{
    public class GameProcessState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ICardsDeck _cardsDeck;
        private readonly IOpenCardService _openCardService;
        private readonly IMovesCounter _movesCounter;
        private readonly IEndGameService _endGameService;
        private IGameBoardLogicService _boardLogic;

        public GameProcessState(GameStateMachine stateMachine, IOpenCardService openCardService, IMovesCounter movesCounter, 
            ICardsDeck cardsDeck, IEndGameService endGameService, IGameBoardLogicService boardLogic)
        {
            _stateMachine = stateMachine;
            _openCardService = openCardService;
            _movesCounter = movesCounter;
            _cardsDeck = cardsDeck;
            _endGameService = endGameService;
            _boardLogic = boardLogic;
        }

        public async void Enter()
        {
           await _openCardService.CheckForOpen();
            _movesCounter.AddMove();
            CheckForEndGame();
        }

        public void Exit()
        { }
        
        
        private void CheckForEndGame()
        {
            if (_cardsDeck.IsEmptyBoard())
            {
                _endGameService.IsWinCondition(true);
                _stateMachine.Enter<EndGameState>();
            }
            else if (_cardsDeck.IsEmptyDeck() && _boardLogic.IsAvailableTurn() == false) 
                _stateMachine.Enter<EndGameState>();
            else
                _stateMachine.Enter<PlayerTurnState>();
        }
    }
}