using System;
using Data;
using Infrastructure.Deck;
using Infrastructure.GameCard;
using Infrastructure.Services;
using Logic.Animations;

namespace Logic.Interactive
{
    public interface IUndoCardService : IService
    {
        public event Action UndoCardOn;
        void UndoCard(ICardsDeck cardsDeck);
    }

    public class UndoCardService : IUndoCardService
    {
        public event Action UndoCardOn;
        
        private readonly ICardAnimator _cardAnimator = AllServices.Container.Single<ICardAnimator>();
        private ICardsDeck _cardsDeck;
        private IUndoCardService _undoCardServiceImplementation;

        public async void UndoCard(ICardsDeck cardsDeck)
        { 
            UndoCardOn?.Invoke();
            Card returnedCard = cardsDeck.ReturnCardFromDiscarded();
           await _cardAnimator.MoveCard(returnedCard.gameObject,
                returnedCard.CardIndex is <= 0 or > 28
                    ? CardPositions.BoardPositions[returnedCard.CardIndex]
                    : CardPositions.DeckPosition);
        }
    }
}