using System;
using Cysharp.Threading.Tasks;
using Infrastructure.GameCard;
using Infrastructure.Services;

namespace Logic.GameBoard
{
    public interface IGameBoardLogicService : IService
    {
        UniTask TakeNextCurrentCard();
        bool IsMatching(int costA, int costB);
        void SetNewCurrentCard(Card cardToSet);
        event Action OnCurrentCardChanged;
        bool IsAvailableTurn();
    }
}