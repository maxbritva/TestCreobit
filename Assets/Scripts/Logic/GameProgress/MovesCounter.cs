using System;

namespace Logic.GameProgress
{
    public class MovesCounter : IMovesCounter
    {
        public event Action OnMovesChanged;
        public int Moves { get; private set; }

        public void AddMove()
        {
            Moves++;
            OnMovesChanged?.Invoke();
        }
    }
}