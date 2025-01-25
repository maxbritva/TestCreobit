using Loader;
using Solitaire.Deck;
using Solitaire.Loop;
using UnityEngine;
using VContainer.Unity;

namespace Solitaire
{
    public class SolitaireBootstrapp : IInitializable
    {
        private DeckCreator _deckCreator;
        private SolitaireResourceLoader _solitaireResourceLoader;
        private GameLoop _gameLoop;
        private SceneLoader _sceneLoader;

        public SolitaireBootstrapp(DeckCreator deckCreator, SolitaireResourceLoader solitaireResourceLoader, 
            GameLoop gameLoop, SceneLoader sceneLoader)
        {
            _deckCreator = deckCreator;
            _gameLoop = gameLoop;
            _sceneLoader = sceneLoader;
            _solitaireResourceLoader = solitaireResourceLoader;
        }

        public async void Initialize()
        {
            _deckCreator.CreateDeck();
            await _solitaireResourceLoader.LoadResources(_deckCreator.Deck);
            ScreenOff();
            _deckCreator.SetupDeckView();
            _gameLoop.PrepareGame();
        }

        private void ScreenOff() => _sceneLoader.ShowOffScreen(false);
    }
}