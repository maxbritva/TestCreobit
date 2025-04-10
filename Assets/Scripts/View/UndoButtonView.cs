using Infrastructure.Deck;
using Logic.Interactive;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class UndoButtonView : MonoBehaviour
    {
        [SerializeField] private Button undoButton;
        private IUndoCardService _undoCardService;
        private ICardsDeck _cardsDeck;

        public void Construct(IUndoCardService undoCardService, ICardsDeck cardsDeck)
        {
            _undoCardService = undoCardService;
            _cardsDeck = cardsDeck;
            _undoCardService.UndoCardOn += UndoButtonClickOn;
            undoButton.onClick.AddListener(UndoButtonClickOn);
        }
        
        private void OnDisable()
        {
            undoButton.onClick.RemoveListener(UndoButtonClickOn);
            _undoCardService.UndoCardOn -= UndoButtonClickOn;
        }

        private void UndoButtonClickOn()
        {
            _undoCardService.UndoCard(_cardsDeck);
        }
    }
}