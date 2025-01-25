using System;

namespace Clicker
{
    public class ClickCounter
    {
        public event Action OnClickChanged;
        
        public int ClickCount { get; private set; }

        public void AddClick()
        {
            ClickCount++;
            OnClickChanged?.Invoke();
        }

        public void SetClickCount(int count)
        {
            if (count >= 0)
                ClickCount = count;
            OnClickChanged?.Invoke();
        }

        
    }
}