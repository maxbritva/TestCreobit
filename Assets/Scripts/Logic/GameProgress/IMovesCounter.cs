using System;
using Infrastructure.Services;

namespace Logic.GameProgress
{
    public interface IMovesCounter : IService
    {
        event Action OnMovesChanged;
        int Moves { get; }
        void AddMove();
    }
}