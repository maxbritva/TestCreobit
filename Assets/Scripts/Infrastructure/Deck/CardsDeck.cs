using System.Collections.Generic;
using System.Linq;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.GameCard;
using UnityEngine;

namespace Infrastructure.Deck
{
    public class CardsDeck : ICardsDeck
    {
        public List<Card> Deck { get; private set; } = new List<Card>();
        
        public Stack<Card> DiscardedCards { get; private set; } = new Stack<Card>();
        public Card CurrentCard { get; private set; }
        
        private readonly IAssetProvider _assetProvider;
        private readonly ICardsListCreator _cardsListCreator;
        private readonly IGameFactory _gameFactory;
        public CardsDeck(IAssetProvider assetProvider, ICardsListCreator cardsListCreator, IGameFactory gameFactory)
        {
            _assetProvider = assetProvider;
            _cardsListCreator = cardsListCreator;
            _gameFactory = gameFactory;
        }
        
        public  void InitDeck(Transform parent)
        {
            var cardOrderLayer = 0;
            for (var index = 0; index < _cardsListCreator.CardsList.Count; index++)
            {
                var cardInList = _cardsListCreator.CardsList[index];
                var cardGameObject = _gameFactory.CreateCard(parent);

                cardGameObject.name = cardInList;
                var card = cardGameObject.GetComponent<Card>();
                card.SetCostAndIndex(_cardsListCreator.CalculateCardCost(cardInList), index);
                foreach (var cardSprite in _assetProvider.FaceCards.Where(
                             cardSprite => cardSprite.name == cardInList))
                    card.SetView(cardSprite, _assetProvider.BackFace);
                Deck.Add(card);
                card.SetOrderLayer(cardOrderLayer);
                cardOrderLayer--;
                card.gameObject.SetActive(true);
            }
        }

        public void TakeCardFromDeck()
        {
            if(CurrentCard != null)
                MoveCardToDiscarded(CurrentCard);
            for (int i = 28; i < Deck.Count; i++)
            {
                if((Deck[i]) == null) continue;
                CurrentCard = Deck[i];
                Deck[i] = null;
                break;
            }
        }

        public void SetCurrentCard(Card card)
        {
            CurrentCard = card;
            RemoveCardFromDeck(card);
        }

        public bool IsEmptyBoard()
        {
            for (int i = 0; i < 27; i++)
                if (Deck[i] != null) return false;
            return true;
        }

        public bool IsEmptyDeck()
        {
            for (int i = 28; i < Deck.Count -1; i++)
                if (Deck[i] != null)
                    return false;
            return true;
        }

        public void MoveCardToDiscarded(Card card)
        {
            DiscardedCards.Push(card);
            RemoveCardFromDeck(card);
            card.gameObject.SetActive(false);
        }

        public Card ReturnCardFromDiscarded()
        {
            Card card = DiscardedCards.Pop();
            Deck[card.CardIndex] = card;
            return card;
        }

        private void RemoveCardFromDeck(Card card)
        {
            for (int i = 0; i < Deck.Count; i++)
            {
                if (card == Deck[i])
                    Deck[i] = null;
            }
        }
    }
}