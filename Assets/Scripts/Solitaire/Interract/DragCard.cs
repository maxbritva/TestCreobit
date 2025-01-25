using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Input;
using Solitaire.Board;
using Solitaire.Deck;
using Solitaire.Loop;
using UnityEngine;
using Timer = Solitaire.Loop.Timer;

namespace Solitaire.Interract
{
    public class DragCard: IDisposable
    {
        private Transform _draggingCard;
        private Vector3 _offset;
        private Vector3 _currentPosition;
        private CancellationTokenSource _cts;
        private PlayerInput _playerInput;
        private GameBoard _gameBoard;
        private DeckCreator _deckCreator;
        private Timer _timer;
        private MovesCounter _movesCounter;
        private EndGameView _endGameView;
        private CardMatchChecker _cardMatchChecker;

        public DragCard(PlayerInput playerInput, GameBoard gameBoard, CardMatchChecker cardMatchChecker,
            DeckCreator deckCreator, Timer timer, MovesCounter movesCounter, EndGameView endGameView)
        {
            _deckCreator = deckCreator;
            _playerInput = playerInput;
            _gameBoard = gameBoard;
            _timer = timer;
            _movesCounter = movesCounter;
            _cardMatchChecker = cardMatchChecker;
            _endGameView = endGameView;
        }

        public void EnableInputs()
        {
            _playerInput.ClickDown += CheckForDrag;
            _playerInput.ClickUp += StopDrag;
        }
        public void Dispose()
        {
            _playerInput.ClickDown -= CheckForDrag;
            _playerInput.ClickUp -= StopDrag;
        }

        private void CheckForEndGame()
        {
            if (_gameBoard.IsEmptyBoard())
            {
                _timer.StopTimer();
                _endGameView.ShowEndGamePanel(true);
                _playerInput.DisableInputs();
            }
            else if (_deckCreator.CardsPool.Count <= 0)
            {
                _timer.StopTimer();
                _endGameView.ShowEndGamePanel(false);
                _playerInput.DisableInputs();
            }
        }
        private async void CheckForDrag()
        {
            var hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(_playerInput.GetPosition()), 
                Vector2.zero, 
                float.PositiveInfinity,
                LayerMask.GetMask("Movable"));
            if (hit == false) return;
            if(hit.transform.GetComponent<Card.Card>().IsFaceUp == false)
               return;
            _cts = new CancellationTokenSource();
            _draggingCard = hit.transform;
            _currentPosition = _draggingCard.position;
            _offset = Camera.main.ScreenToWorldPoint(
                _playerInput.GetPosition()) - _draggingCard.position;
            await Drag().SuppressCancellationThrow();
        }

        private async UniTask Drag()
        {
            while (_cts.IsCancellationRequested == false)
            {
                var pointerPosition = Camera.main.ScreenToWorldPoint(
                    _playerInput.GetPosition());
                    _draggingCard.Translate(pointerPosition);
                    _draggingCard.position = pointerPosition - _offset;
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
        }

        private void StopDrag()
        {
            if (_draggingCard && IsValidPosition())
            {
                var draggingCardComp = _draggingCard.GetComponent<Card.Card>();
                var currentCardComp = _gameBoard.CurrentCard.GetComponent<Card.Card>();
                if (_cardMatchChecker.CheckMatch(draggingCardComp.CardCost, currentCardComp.CardCost))
                {
                    _draggingCard.position = _gameBoard.CurrentCardPlace.position;
                    _gameBoard.RemoveCardFromBoard(_draggingCard.gameObject.name);
                    _gameBoard.SetCurrentCard(_draggingCard.gameObject);
                    _draggingCard.GetComponent<Card.Card>().SetOrderLayer(1);
                  _cardMatchChecker.CheckForOpen();
                  _movesCounter.AddMove();
                  CheckForEndGame();
                }
                else
                    ReturnPosition();
            }
            else
                ReturnPosition();
            _draggingCard = null;
           _cts?.Cancel(); 
        }

        private bool IsValidPosition() => Vector3.Distance(_draggingCard.position, 
            _gameBoard.CurrentCardPlace.position) < 2f;

        private void ReturnPosition()
        {
            if(_draggingCard != null)
                _draggingCard.DOMove(_currentPosition, 0.3f).SetEase(Ease.OutBack);
        }
    }
}