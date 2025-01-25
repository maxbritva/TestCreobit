using System;
using UnityEngine;

namespace Solitaire.Card
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        private Sprite _faceSide;
        private Sprite _backSide;
        
        public int CardCost { get; private set; }

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

        public void SetCost(int cost)
        {
            CardCost = cost;
        }

        public void SetOrderLayer(int order) => _renderer.sortingOrder = order;
    }
}