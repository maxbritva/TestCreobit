using System.Collections.Generic;
using Infrastructure.GameCard;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Deck
{
    public interface ICardsDeck : IService
    {
        List<Card> Deck { get; }
        Card CurrentCard { get; }
        Stack<Card> DiscardedCards { get; }
        void InitDeck(Transform parent);
        void TakeCardFromDeck();
        void SetCurrentCard(Card card);
        bool IsEmptyBoard();
        bool IsEmptyDeck();
        void MoveCardToDiscarded(Card card);
        Card ReturnCardFromDiscarded();
    }
}