using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Deck;
using Infrastructure.Services.PersistentProgress;
using Logic.GameProgress;
using Logic.Interactive;
using UnityEngine;
using View;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private readonly IAssetProvider _assets;
        private readonly IMovesCounter _movesCounter;
        private readonly IGameTimer _gameTimer;
        private readonly IEndGameService _endGameService;
        private readonly IUndoCardService _undoCardService;
        private readonly ICardsDeck _cardsDeck;

        public GameFactory(IAssetProvider assetProvider, IMovesCounter movesCounter, 
            IGameTimer gameTimer, IEndGameService endGameService, IUndoCardService undoCardService, 
            ICardsDeck cardsDeck)
        {
            _assets = assetProvider;
            _movesCounter = movesCounter;
            _gameTimer = gameTimer;
            _endGameService = endGameService;
            _undoCardService = undoCardService;
            _cardsDeck = cardsDeck;
        }

        public GameObject CreateCard(Transform parent)
        {
            var cardGameObject = _assets.InstantiateCard(parent);
            RegisterProgressWatchers(cardGameObject);
            cardGameObject.transform.localScale = Vector3.zero;
            return cardGameObject;
        }

        public GameObject CreateHud()
        {
            var instantiateHud = _assets.InstantiateHud();
            instantiateHud.GetComponentInChildren<MovesCounterView>().Construct(_movesCounter);
            instantiateHud.GetComponentInChildren<GameTimerView>().Construct(_gameTimer);
            instantiateHud.GetComponentInChildren<EndGameView>().Construct(_endGameService);
            instantiateHud.GetComponentInChildren<UndoButtonView>().Construct(_undoCardService, _cardsDeck);
            return instantiateHud;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
        private void RegisterProgressWatchers(GameObject cardGameObject)
        {
            foreach (var progressReader in cardGameObject.GetComponentsInChildren<ISavedProgressReader>()) 
                Register(progressReader);
        }
        
        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            ProgressReaders.Add(progressReader);
        }
    }
}