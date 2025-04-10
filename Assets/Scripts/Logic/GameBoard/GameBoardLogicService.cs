using System;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Deck;
using Infrastructure.GameCard;
using Infrastructure.Services.Input;
using Logic.Animations;
using Logic.Interactive;
using UnityEngine;

namespace Logic.GameBoard
{
    public class GameBoardLogicService : IGameBoardLogicService, IDisposable
    {
        public event Action OnCurrentCardChanged;
        
        private readonly ICardAnimator _cardAnimator;
        private readonly IOpenCardService _openCardService;
        private readonly ICardsDeck _cardsDeck;
        private DragCardService _dragCardService;
        private readonly IInputService _inputService;
        public GameBoardLogicService(ICardAnimator cardAnimator,ICardsDeck cardsDeck, IOpenCardService openCardService, 
            IInputService inputService)
        {
            _cardAnimator = cardAnimator;
            _cardsDeck = cardsDeck;
            _openCardService = openCardService;
            _inputService = inputService;
            _inputService.ClickDown += CheckPoolArea;
        }

        public void Dispose() => _inputService.ClickDown -= CheckPoolArea;

       public async UniTask TakeNextCurrentCard()
        {
            _cardsDeck.TakeCardFromDeck();
            var currentCard = _cardsDeck.CurrentCard;
            currentCard.SetOrderLayer(0);
            await _cardAnimator.GetCurrentCardAnimation();
            await _openCardService.OpenCard(currentCard.gameObject, false);
        }

        public void SetNewCurrentCard(Card cardToSet)
        {
            _cardsDeck.MoveCardToDiscarded(_cardsDeck.CurrentCard);
            _cardsDeck.SetCurrentCard(cardToSet);
            cardToSet.SetOrderLayer(0);
            cardToSet.IsInteractable(false);
        }

        public bool IsMatching(int costA, int costB)
        {
            return costA - costB == 1
                   || costA - costB == -1
                   || (costA == 2 && costB == 14) 
                   || (costA == 14 && costB == 2);
        }

        public bool IsAvailableTurn()
        {
            for (int i = 0; i < 28; i++)
            {
                if(_cardsDeck.Deck[i] == null) continue;
                if(_cardsDeck.Deck[i].IsFaceUp == false) continue;
                if (IsMatching(_cardsDeck.Deck[i].CardCost, _cardsDeck.CurrentCard.CardCost))
                    return true;
            }
            return false;
        }
        
        private async void CheckPoolArea()
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_inputService.GetPosition()), 
                Vector2.zero, float.PositiveInfinity, LayerMask.GetMask(StaticDataNames.AreaPool));
            if (hit == false) return;
            OnCurrentCardChanged?.Invoke();
           await TakeNextCurrentCard();
        }
    }
}