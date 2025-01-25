using Solitaire.Loop;
using TMPro;
using UnityEngine;
using VContainer;

namespace Solitaire.View
{
    public class MovesCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _movesText;

        private MovesCounter _movesCounter;
        private void OnEnable() => _movesCounter.OnMovesChanged += ChangeMovesView;

        private void OnDisable() => _movesCounter.OnMovesChanged -= ChangeMovesView;

        private void ChangeMovesView() => _movesText.text = _movesCounter.Moves + " Moves";
        
        [Inject] private void Construct(MovesCounter movesCounter) => _movesCounter = movesCounter;
    }
}