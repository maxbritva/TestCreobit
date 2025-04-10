using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Services.PersistentProgress;
using Random = System.Random;

namespace Infrastructure.Deck
{
    public class CardsListCreator : ISavedProgress, ICardsListCreator
    {
        private static readonly string[] Suits = { "C", "D", "H", "S" };
        private static readonly string[] Values = 
            { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        
        public List<string> CardsList { get; private set;}
      
        public void CreateCardsList()
        {
            CardsList = new List<string>();
            foreach (var suit in Suits) 
                CardsList.AddRange(Values.Select(value => suit + value));
            ShuffleCards(CardsList);
        }

        public void ShuffleCards<T>(List<T> cards)
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

        public void LoadProgress(GameProgress progress) => progress.InitialCardsList = CardsList;

        public void UpdateProgress(GameProgress progress)
        {
            if(CardsList != null) 
                CardsList = progress.InitialCardsList;
        }
    }
}
