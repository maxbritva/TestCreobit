using System;
using Input;
using Solitaire.Board;
using Solitaire.Deck;
using Solitaire.Interract;
using Solitaire.View;
using UnityEngine;

namespace Solitaire.Loop
{
    public class GameLoop : IDisposable
    {
        private readonly GameBoard _gameBoard;
        private readonly DeckCreator _deckCreator;
        private readonly CardAnimator _cardAnimator;
        private DragCard _dragCard;
        private PlayerInput _playerInput;
        private Timer _timer;
        private UIButtonsView _buttonsView;
        
        public GameLoop(GameBoard gameBoard, DeckCreator deckCreator, 
            CardAnimator cardAnimator, DragCard dragCard, PlayerInput playerInput, Timer timer, UIButtonsView uiButtonsView)
        {
            _timer = timer;
            _gameBoard = gameBoard;
            _deckCreator = deckCreator;
            _cardAnimator = cardAnimator;
            _dragCard = dragCard;
            _playerInput = playerInput;
            _buttonsView = uiButtonsView;
        }

        public void Dispose() => _playerInput.ClickDown -= CheckPoolArea;
        public async void PrepareGame()
        {
            await _cardAnimator.DeckStartAnimation(_deckCreator.CardsPool);
            foreach (var card in _gameBoard.FourLineCards)
            {
               await _cardAnimator.RotateCard(card, 90f);
                card.GetComponent<Card.Card>().SetSide(true);
                _cardAnimator.RotateCard(card, 0f);
            }
            GetNextCardFromPool();
            _dragCard.EnableInputs();
            _playerInput.ClickDown += CheckPoolArea;
            _buttonsView.ActivateButtons();
           await _timer.StartTimer();
        }

        public async void GetNextCardFromPool()
        {
            var card = _deckCreator.CardsPool[^1];
            var cardComponent = card.GetComponent<Card.Card>();
            cardComponent.SetSide(true);
            _deckCreator.RemoveCardFromPool();
            cardComponent.SetCost(_deckCreator.CalculateCardCost(card.transform.name));
            _gameBoard.SetCurrentCard(card);
           await _cardAnimator.GetCurrentCardAnimation();
           
        }

        private void CheckPoolArea()
        {
            var hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(_playerInput.GetPosition()), 
                Vector2.zero, 
                float.PositiveInfinity,
                LayerMask.GetMask("AreaPool"));
            if (hit == false) return;
            GetNextCardFromPool();
        }

     
        
    }
}