using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Deck;
using Infrastructure.GameCard;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Logic.Animations;
using Logic.GameBoard;
using UnityEngine;


namespace Logic.Interactive
{
    public interface IDragCardService : IService
    {
        void EnableInputs();
        void StartDrag();
        UniTask Drag();
        void StopDrag();
        event Action OnCurrentCardChanged;
    }

    public class DragCardService : IDisposable, IDragCardService
    {
        public event Action OnCurrentCardChanged;
        
        private readonly IInputService _inputService;
        private readonly ICardsDeck _cardsDeck;
        private readonly ICardAnimator _cardAnimator;
        private readonly IGameBoardLogicService _boardLogic;

        private Transform _draggingCard;
        private Vector3 _offset;
        private Vector3 _currentPosition;
        private CancellationTokenSource _cts;

        public DragCardService(IInputService inputService, ICardsDeck cardsDeck, IGameBoardLogicService boardLogic, 
            ICardAnimator cardAnimator)
        {
            _inputService = inputService;
            _cardsDeck = cardsDeck;
            _boardLogic = boardLogic;
            _cardAnimator = cardAnimator;
            EnableInputs();
        }

        public void EnableInputs()
        {
            _inputService.ClickDown += StartDrag;
            _inputService.ClickUp += StopDrag;
        }

        public void Dispose()
        {
            _inputService.ClickDown -= StartDrag;
            _inputService.ClickUp -= StopDrag;
        }

        public async void StartDrag()
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_inputService.GetPosition()),
                Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Movable"));
            if (hit == false) return;
            if (hit.transform.GetComponent<Card>().IsFaceUp == false)
                return;
            _cts = new CancellationTokenSource();
            _draggingCard = hit.transform;
            _currentPosition = _draggingCard.position;
            _offset = Camera.main.ScreenToWorldPoint(_inputService.GetPosition()) - _draggingCard.position;
            await Drag().SuppressCancellationThrow();
        }

        public async UniTask Drag()
        {
            while (_cts.IsCancellationRequested == false)
            {
                _draggingCard.GetComponent<Card>().SetOrderLayer(100);
                var pointerPosition = Camera.main.ScreenToWorldPoint(_inputService.GetPosition());
                _draggingCard.Translate(pointerPosition);
                _draggingCard.position = pointerPosition - _offset;
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
        }

        public void StopDrag()
        {
            if (_draggingCard && IsValidPosition())
            {
                var draggingCardComp = _draggingCard.GetComponent<Card>();
                var currentCardComp = _cardsDeck.CurrentCard.GetComponent<Card>();
                if (_boardLogic.IsMatching(draggingCardComp.CardCost, currentCardComp.CardCost))
                {
                    _boardLogic.SetNewCurrentCard(draggingCardComp);
                    _draggingCard.position = CardPositions.CurrentCardPosition;
                    OnCurrentCardChanged?.Invoke();
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
            CardPositions.CurrentCardPosition) < 2.5f;

        private async void ReturnPosition()
        {
            if (_draggingCard == null) return;
            _draggingCard.GetComponent<Card>().SetOrderLayer(50);
            await _cardAnimator.MoveCard(_draggingCard.gameObject, _currentPosition);
        }
    }
}