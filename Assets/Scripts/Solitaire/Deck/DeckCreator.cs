using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = System.Random;

namespace Solitaire.Deck
{
    public class DeckCreator
    {
        public static readonly string[] Suits = { "C", "D", "H", "S" };
        public static readonly string[] Values = 
            { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        private IObjectResolver _objectResolver;
        private SolitaireResourceLoader _solitaireResourceLoader;

        public DeckCreator(IObjectResolver objectResolver, SolitaireResourceLoader solitaireResourceLoader)
        {
            _solitaireResourceLoader = solitaireResourceLoader;
            _objectResolver = objectResolver;
        }
        public List<string> Deck { get; private set;}
        public List<GameObject> CardsPool { get; private set;}
        public void CreateDeck()
        {
            Deck = new List<string>();
            CardsPool = new List<GameObject>();
            foreach (var suit in Suits) 
                Deck.AddRange(Values.Select(value => suit + value));
            ShuffleCards(Deck);
        }

        public  void SetupDeckView()
        {
          
            foreach (var cardName in Deck)
            {
                var card = _objectResolver.Instantiate(_solitaireResourceLoader.CardPrefab, 
                    new Vector3(0f,-50f, 10f), quaternion.identity);
                card.name = cardName;
                var cardView = card.GetComponent<Card.Card>();
                cardView.SetCost(CalculateCardCost(cardName));
                foreach (var cardSprite in _solitaireResourceLoader.FaceCards.Where(
                             cardSprite => cardSprite.name == cardName)) 
                    cardView.SetView(cardSprite, _solitaireResourceLoader.BackFace);
                CardsPool.Add(card);
                card.SetActive(true);
            }
        }
        public void RemoveCardFromPool()
        {
            CardsPool.RemoveAt(CardsPool.Count - 1);
        }

        private void ShuffleCards<T>(List<T> cards)
        {
            var random = new Random();
            var cardsCount = cards.Count;
            while (cardsCount > 1)
            {
                var newRandom = random.Next(cardsCount);
                cardsCount--;
                (cards[newRandom], cards[cardsCount]) = (cards[cardsCount], cards[newRandom]);
            }
        }

        public int CalculateCardCost(string cardInDeck)
        {
            var costString = "";
            for (int i = 1; i < cardInDeck.Length; i++)
            {
                var c = cardInDeck[i];
                costString += c.ToString();
            }
            return costString switch
            {
                "2" => 2, "3" => 3,
                "4" => 4, "5" => 5,
                "6" => 6, "7" => 7,
                "8" => 8, "9" => 9,
                "10" => 10, "J" => 11,
                "Q" => 12, "K" => 13,
                "A" => 14, _ => default
            };
        }
    }
}
