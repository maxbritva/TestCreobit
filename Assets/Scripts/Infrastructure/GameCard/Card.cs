using Data;
using UnityEngine;

namespace Infrastructure.GameCard
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        private Sprite _faceSide;
        private Sprite _backSide;
        public int CardCost { get; private set; }
        public int CardIndex { get; private set; }
        public bool IsFaceUp { get; private set; }

        public void SetView(Sprite face, Sprite back)
        {
            _faceSide = face;
            _backSide = back;
            SetSide(false);
        }

        public void SetSide(bool faceUp)
        {
            IsFaceUp = faceUp; 
            _renderer.sprite = IsFaceUp ? _faceSide : _backSide;
        }

        public void SetCostAndIndex(int cost, int index)
        {
            if(cost is > 1 and <= 14)
                CardCost = cost;
            if (index is >= 0 and <= 52)
                CardIndex = index;
        }

        public void IsInteractable(bool value) => 
            gameObject.layer = LayerMask.NameToLayer(value ? 
                StaticDataNames.Movable : StaticDataNames.Static);

        public void SetOrderLayer(int order) => _renderer.sortingOrder = order;
    }
}