using Logic.GameProgress;
using TMPro;
using UnityEngine;

namespace View
{
    public class GameTimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        private IGameTimer _gameTimer;

        public void Construct(IGameTimer gameTimer)
        {
            _gameTimer = gameTimer;
            _gameTimer.OnTimeChanged += ShowTime;
        }

        private void OnDisable() => _gameTimer.OnTimeChanged -= ShowTime;

        private void ShowTime() => FormatText();

        private void FormatText()
        {
            switch (_gameTimer.Time)
            {
                case < 10:
                    _timerText.text = $"0:0{_gameTimer.Time}";
                    break;
                case >= 10 and < 60:
                    _timerText.text = $"0:{_gameTimer.Time}";
                    break;
                case >= 60:
                {
                    var minutes = _gameTimer.Time / 60;
                    var sec = _gameTimer.Time % 60;
                    _timerText.text = sec < 10 ? $"{minutes}:0{sec}" : $"{minutes}:{sec}";
                    break;
                }
            }
        }
    }
}