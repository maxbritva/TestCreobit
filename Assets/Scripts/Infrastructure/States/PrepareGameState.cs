using Infrastructure.Deck;
using Logic;
using Logic.Animations;
using Logic.GameBoard;
using Logic.GameProgress;
using Logic.Interactive;

namespace Infrastructure.States
{
    public class PrepareGameState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ICardAnimator _cardAnimator;
        private readonly IGameBoardLogicService _boardLogic;
        private CardsListCreator _cardListCreator;
        private IGameTimer _gameTimer;
       
        private readonly IOpenCardService _openCardService;

        public PrepareGameState(GameStateMachine gameStateMachine, ICardAnimator cardAnimator, 
            IOpenCardService openCardService, IGameBoardLogicService boardLogic, IGameTimer gameTimer)
        {
            _cardAnimator = cardAnimator;
            _openCardService = openCardService;
            _boardLogic = boardLogic;
            _gameTimer = gameTimer;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await _cardAnimator.DeckInitAnimation();
             _openCardService.OpenStartCards();
            await _boardLogic.TakeNextCurrentCard();
            _gameTimer.StartTimer();
            _gameStateMachine.Enter<PlayerTurnState>();
        }
        
        public void Exit()
        { }
    }
}