using UnityEngine;
using VContainer.Unity;

namespace Clicker
{
    public class ClickProgressSaver : IInitializable
    {
        private ClickCounter _clickCounter;
        private const string Count = "Count";

        public ClickProgressSaver(ClickCounter clickCounter)
        {
            _clickCounter = clickCounter;
            TryLoad();
        }

        public void Save() => PlayerPrefs.SetInt(Count, _clickCounter.ClickCount);

        public void TryLoad() => 
            _clickCounter.SetClickCount(PlayerPrefs.GetInt(Count) 
                                        >= 1 ? PlayerPrefs.GetInt(Count) : 0);

        public void Initialize() => TryLoad();
    }
}