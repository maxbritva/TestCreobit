using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Deck;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Loader;
using Logic.Animations;
using Logic.GameBoard;
using Logic.GameProgress;
using Logic.Interactive;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public async void Enter() => await _sceneLoader.LoadScene(StaticDataNames.InitialScene, EnterLoadProgress);

        public void Exit()
        { }
        private void RegisterServices()
        {
            _services.RegisterSingle(InputsService());
            RegisterGameProgress();
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            
            _services.RegisterSingle<IPersistentProgressService>(
                new PersistentProgressService());
            
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), 
                _services.Single<IMovesCounter>(), _services.Single<IGameTimer>(), 
                _services.Single<IEndGameService>(), _services.Single<IUndoCardService>(), _services.Single<ICardsDeck>()));  
            
            _services.RegisterSingle<IUndoCardService>(new UndoCardService());
            
            _services.RegisterSingle<ICardsListCreator>(new CardsListCreator());
            
            _services.RegisterSingle<ICardsDeck>(new CardsDeck(_services.Single<IAssetProvider>(), 
                _services.Single<ICardsListCreator>(), _services.Single<IGameFactory>()));
            
            _services.RegisterSingle<ICardAnimator>(new CardAnimator(_services.Single<ICardsDeck>()));
            
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
            
            _services.RegisterSingle<IOpenCardService>(new OpenCardService(
                _services.Single<ICardAnimator>(), _services.Single<ICardsDeck>()));
            
            _services.RegisterSingle<IGameBoardLogicService>( new GameBoardLogicService(
                _services.Single<ICardAnimator>(), _services.Single<ICardsDeck>(), 
                _services.Single<IOpenCardService>(), _services.Single<IInputService>()));
            
            _services.RegisterSingle<IDragCardService>(new DragCardService(_services.Single<IInputService>(),
                _services.Single<ICardsDeck>(), _services.Single<IGameBoardLogicService>(), _services.Single<ICardAnimator>()));
            
        }

        private void EnterLoadProgress() => _stateMachine.Enter<LoadProgressState>();

        private static IInputService InputsService() => new InputService();

        private void RegisterGameProgress()
        {
            _services.RegisterSingle<IMovesCounter>(new MovesCounter());
            _services.RegisterSingle<IGameTimer>(new GameTimer());
            _services.RegisterSingle<IEndGameService>(new EndGameService(_sceneLoader));
        }
    }
}