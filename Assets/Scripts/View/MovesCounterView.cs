using Logic.GameProgress;
using TMPro;
using UnityEngine;

namespace View
{
    public class MovesCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _movesText;
        private IMovesCounter _movesCounter;

        public void Construct(IMovesCounter movesCounter)
        {
            _movesCounter = movesCounter;
            _movesCounter.OnMovesChanged += ChangeMovesView;
        }
        private void OnDisable() => _movesCounter.OnMovesChanged -= ChangeMovesView;

        private void ChangeMovesView() => _movesText.text = _movesCounter.Moves + " Moves";
        
    }
}