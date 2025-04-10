using System.Collections.Generic;
using Infrastructure.Services;

namespace Infrastructure.Deck
{
    public interface ICardsListCreator : IService
    {
        List<string> CardsList { get; }
        void CreateCardsList();
        void ShuffleCards<T>(List<T> cards);
        int CalculateCardCost(string cardInDeck);
    }
}