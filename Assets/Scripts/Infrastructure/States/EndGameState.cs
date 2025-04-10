using Logic.GameProgress;

namespace Infrastructure.States
{
    public class EndGameState : IState
    {
        private GameStateMachine _stateMachine;
        private readonly IEndGameService _endGameService;
        private readonly IGameTimer _gameTimer;
       

        public EndGameState(GameStateMachine stateMachine, IEndGameService endGameService, IGameTimer gameTimer)
        {
            _stateMachine = stateMachine;
            _endGameService = endGameService;
            _gameTimer = gameTimer;
        }

        public void Enter()
        {
            if (_endGameService.IsWin)
            {
                
            }
            else
            {
                
            }
            _endGameService.ShowEndGamePanel();
            _gameTimer.StopTimer();
            //     _endGameView.ShowEndGamePanel(true);
            //     _endGameView.ShowEndGamePanel(false);

        }

        public void Exit()
        {
            
        }

       
    }
}