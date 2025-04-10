using System;
using Infrastructure.Services;

namespace Logic.GameProgress
{
    public interface IEndGameService : IService
    {
        bool IsWin { get; }
        bool IsWinCondition(bool condition);
        void ShowEndGamePanel();
        event Action OnGameEnd;
        void StartNewGame();
        void RetryCurrentGame();
    }
}