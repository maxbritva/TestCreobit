using Clicker;
using Solitaire;
using Solitaire.Board;
using Solitaire.Deck;
using Solitaire.Interract;
using Solitaire.Loop;
using Solitaire.View;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
using PlayerInput = Input.PlayerInput;

namespace DI
{
    public class SolitaireScope : LifetimeScope
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private EndGameView _endGameView;
        [SerializeField] private UIButtonsView _buttonsView;
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterSolitaire(builder);
        }
        
        private void RegisterSolitaire(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SolitaireBootstrapp>();
            builder.Register<PlayerInput>(Lifetime.Singleton);
            builder.Register<DeckCreator>(Lifetime.Singleton);
            builder.Register<SolitaireResourceLoader>(Lifetime.Singleton);
            builder.Register<CardAnimator>(Lifetime.Singleton);
            builder.Register<CardMatchChecker>(Lifetime.Singleton);
            builder.Register<GameLoop>(Lifetime.Singleton);
            builder.Register<DragCard>(Lifetime.Singleton);
            builder.Register<Timer>(Lifetime.Singleton);
            builder.Register<MovesCounter>(Lifetime.Singleton);
            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_endGameView);
            builder.RegisterInstance(_buttonsView);
        }
    }
}