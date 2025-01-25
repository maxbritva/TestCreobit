using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Solitaire.Board;
using UnityEngine;

namespace Solitaire.Interract
{
    public class CardMatchChecker
    {
        private GameBoard _gameBoard;
        private CardAnimator _cardAnimator;

        public CardMatchChecker(GameBoard gameBoard, CardAnimator cardAnimator)
        {
            _gameBoard = gameBoard;
            _cardAnimator = cardAnimator;
        }

        public void CheckForOpen()
        {
            CheckThirdLine();
            CheckSecondLine();
            CheckFirstLine();
        }
        public bool CheckMatch(int costA, int costB)
        {
            return costA - costB == 1
                   || costA - costB == -1
                   || (costA == 2 && costB == 14) 
                   || (costA == 14 && costB == 2);
        }

        private async void CheckFirstLine()
        {
            if (_gameBoard.SecondLineCards[0] == null && _gameBoard.SecondLineCards[1] == null &&      _gameBoard.FirstLineCards[0]
                && _gameBoard.FirstLineCards[0].GetComponent<Card.Card>().IsFaceUp == false)
                await OpenCard(_gameBoard.FirstLineCards[0]);
            
            if (_gameBoard.SecondLineCards[2] == null && _gameBoard.SecondLineCards[3] == null && _gameBoard.FirstLineCards[1]
                && _gameBoard.FirstLineCards[1].GetComponent<Card.Card>().IsFaceUp == false)
                await OpenCard(_gameBoard.FirstLineCards[1]);
            
            if (_gameBoard.SecondLineCards[4] == null && _gameBoard.SecondLineCards[5] == null && _gameBoard.FirstLineCards[2]
                && _gameBoard.FirstLineCards[2].GetComponent<Card.Card>().IsFaceUp == false)
                await OpenCard(_gameBoard.FirstLineCards[2]);
        }

        private async void CheckSecondLine()
        {
            if (_gameBoard.ThirdLineCards[0] == null && _gameBoard.ThirdLineCards[1] == null && _gameBoard.SecondLineCards[0] 
               && _gameBoard.SecondLineCards[0].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.SecondLineCards[0]);
            
            if (_gameBoard.ThirdLineCards[1] == null && _gameBoard.ThirdLineCards[2] == null && _gameBoard.SecondLineCards[1]
                && _gameBoard.SecondLineCards[1].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.SecondLineCards[1]);
            
            if (_gameBoard.ThirdLineCards[3] == null && _gameBoard.ThirdLineCards[4] == null && _gameBoard.SecondLineCards[2]
                && _gameBoard.SecondLineCards[2].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.SecondLineCards[2]);
            
            if (_gameBoard.ThirdLineCards[4] == null && _gameBoard.ThirdLineCards[5] == null &&  _gameBoard.SecondLineCards[3]
                && _gameBoard.SecondLineCards[3].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.SecondLineCards[3]);
            
            if (_gameBoard.ThirdLineCards[6] == null && _gameBoard.ThirdLineCards[7] == null &&  _gameBoard.SecondLineCards[4]
                && _gameBoard.SecondLineCards[4].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.SecondLineCards[4]);
            
            if (_gameBoard.ThirdLineCards[7] == null && _gameBoard.ThirdLineCards[8] == null && _gameBoard.SecondLineCards[5]
               && _gameBoard.SecondLineCards[5].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.SecondLineCards[5]);
        }

        private async void CheckThirdLine()
        {
            if (_gameBoard.FourLineCards[0] == null && _gameBoard.FourLineCards[1] == null && _gameBoard.ThirdLineCards[0]
                && _gameBoard.ThirdLineCards[0].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[0]);
            
            if (_gameBoard.FourLineCards[1] == null && _gameBoard.FourLineCards[2] == null &&  _gameBoard.ThirdLineCards[1]
                && _gameBoard.ThirdLineCards[1].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[1]);
            
            if (_gameBoard.FourLineCards[2] == null && _gameBoard.FourLineCards[3] == null && _gameBoard.ThirdLineCards[2]
               && _gameBoard.ThirdLineCards[2].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[2]);
            
            if (_gameBoard.FourLineCards[3] == null && _gameBoard.FourLineCards[4] == null && _gameBoard.ThirdLineCards[3]
               && _gameBoard.ThirdLineCards[3].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[3]);
            
            if (_gameBoard.FourLineCards[4] == null && _gameBoard.FourLineCards[5] == null &&  _gameBoard.ThirdLineCards[4]
                && _gameBoard.ThirdLineCards[4].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[4]);
            
            if (_gameBoard.FourLineCards[5] == null && _gameBoard.FourLineCards[6] == null && _gameBoard.ThirdLineCards[5]
                && _gameBoard.ThirdLineCards[5].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[5]);
            
            if (_gameBoard.FourLineCards[6] == null && _gameBoard.FourLineCards[7] == null && 
                _gameBoard.ThirdLineCards[6] &&  _gameBoard.ThirdLineCards[6].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[6]);
            
            if (_gameBoard.FourLineCards[7] == null && _gameBoard.FourLineCards[8] == null &&  _gameBoard.ThirdLineCards[7] &&
                _gameBoard.ThirdLineCards[7].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[7]);
            
            if (_gameBoard.FourLineCards[8] == null && _gameBoard.FourLineCards[9] == null &&  _gameBoard.ThirdLineCards[8]
               && _gameBoard.ThirdLineCards[8].GetComponent<Card.Card>().IsFaceUp == false) 
                await OpenCard(_gameBoard.ThirdLineCards[8]);
        }

        private async UniTask OpenCard(GameObject card)
        {
            if(card == null) return;
            await _cardAnimator.RotateCard(card, 90f);
            card.GetComponent<Card.Card>().SetSide(true);
            _cardAnimator.RotateCard(card, 0f);
            card.layer = LayerMask.NameToLayer("Movable");
        }
    }
}