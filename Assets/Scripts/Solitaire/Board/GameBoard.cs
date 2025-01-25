using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Solitaire.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private List<Transform> _firstLinePlacer;
        [SerializeField] private List<Transform> _secondLinePlacer;
        [SerializeField] private List<Transform> _thirdLinePlacer;
        [SerializeField] private List<Transform> _fourLinePlacer;
        [SerializeField] private Transform _deckPlace;
        [SerializeField] private Transform _currentCardPlace;
        public List<Transform> FirstLinePlacer => _firstLinePlacer;
        public Transform DeckPlace => _deckPlace;
        public Transform CurrentCardPlace => _currentCardPlace;
        public List<Transform> SecondLinePlacer => _secondLinePlacer;
        public List<Transform> ThirdLinePlacer => _thirdLinePlacer;
        public List<Transform> FourLinePlacer => _fourLinePlacer;
        
        public List<GameObject> FirstLineCards { get; private set; }
        public List<GameObject> SecondLineCards { get; private set; }
        public List<GameObject> ThirdLineCards { get; private set; }
        public List<GameObject> FourLineCards { get; private set; }
        
        public GameObject CurrentCard { get; private set; }

        public void Initialize()
        {
            FirstLineCards = new List<GameObject>();
            SecondLineCards = new List<GameObject>();
            ThirdLineCards = new List<GameObject>();
            FourLineCards = new List<GameObject>();
        }

        public void AddCard(List<GameObject> targetLine, GameObject targetCard) => 
            targetLine.Add(targetCard);

        public void SetCurrentCard(GameObject target)
        {
            if(CurrentCard)
                CurrentCard.SetActive(false);
            CurrentCard = target;
            CurrentCard.layer = LayerMask.NameToLayer("Default");
        }

        public void RemoveCardFromBoard(string cardName)
        {
            CheckCardInLine(cardName, FourLineCards);
            CheckCardInLine(cardName, ThirdLineCards);
            CheckCardInLine(cardName, SecondLineCards);
            CheckCardInLine(cardName, FirstLineCards);
        }

        private void CheckCardInLine(string cardName, List<GameObject> targetLine)
        {
            for (var index = 0; index < targetLine.Count; index++)
            {
                if(targetLine[index] == null) continue;
                if (targetLine[index].name == cardName)
                    targetLine[index] = null;
            }
        }

        public bool IsEmptyBoard()
        {
            if (FirstLineCards.Any(t => t != null))
                return false;
            if (SecondLineCards.Any(t => t != null))
                return false;
            if (ThirdLineCards.Any(t => t != null))
                return false;
            if (FourLineCards.Any(t => t != null))
                return false;
            return true;
        }
    }
}