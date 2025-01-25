using Solitaire.Loop;
using TMPro;
using UnityEngine;
using VContainer;

namespace Solitaire.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        private Timer _timer;

        private void OnEnable() => _timer.OnTimeChanged += ShowTime;

        private void OnDisable() => _timer.OnTimeChanged -= ShowTime;

        private void ShowTime() => FormatText();

        private void FormatText()
        {
            switch (_timer.Time)
            {
                case < 10:
                    _timerText.text = $"0:0{_timer.Time}";
                    break;
                case >= 10 and < 60:
                    _timerText.text = $"0:{_timer.Time}";
                    break;
                case >= 60:
                {
                    var minutes = _timer.Time / 60;
                    var sec = _timer.Time % 60;
                    _timerText.text = sec < 10 ? $"{minutes}:0{sec}" : $"{minutes}:{sec}";
                    break;
                }
            }
        }

        [Inject] private void Construct(Timer timer) => _timer = timer;
    }
}