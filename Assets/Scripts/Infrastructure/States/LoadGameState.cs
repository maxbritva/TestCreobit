using System.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Deck;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Loader;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadGameState : IPayloadedState<string>
    {
        private readonly IPersistentProgressService _progressService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ICardsListCreator _cardsListCreator;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameFactory _gameFactory;
        private readonly Transform _parent;
        private readonly ICardsDeck _cardsDeck;

        public LoadGameState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Transform parent,
            LoadingCurtain curtain, IAssetProvider assetProvider, 
            IPersistentProgressService progressService, IGameFactory gameFactory, ICardsDeck cardsDeck, 
            ICardsListCreator cardsListCreator)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _parent = parent;
            _curtain = curtain;
            _assetProvider = assetProvider;
            _progressService = progressService;
            _gameFactory = gameFactory;
            _cardsDeck = cardsDeck;
            _cardsListCreator = cardsListCreator;
        }
        
        public async void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup(); // IF NEEDED ????
           await _sceneLoader.LoadScene(sceneName, OnLoaded);
        }

        public async void Exit() =>  await _curtain.Hide();

        private async void OnLoaded()
        {
            await InitCardDeckAndResources();
            _gameFactory.CreateHud();
            InformProgressReaders();
            _gameStateMachine.Enter<PrepareGameState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders) 
                progressReader.LoadProgress(_progressService.GameProgress);
        }

        private async Task InitCardDeckAndResources()
        {
            _cardsListCreator.CreateCardsList();
            await _assetProvider.LoadResources(_cardsListCreator.CardsList);
            _cardsDeck.InitDeck(_parent);
        }
    }
}