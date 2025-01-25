using System;

namespace Solitaire.Loop
{
    public class MovesCounter
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