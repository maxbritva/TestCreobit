using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Solitaire.Board;
using Solitaire.Card;
using Solitaire.Deck;
using UnityEngine;

namespace Solitaire
{
    public class CardAnimator
    {
        private GameBoard _gameBoard;
        private DeckCreator _deckCreator;
        private CancellationTokenSource _cts;

        public CardAnimator(GameBoard gameBoard, DeckCreator deckCreator)
        {
            _gameBoard = gameBoard;
            _deckCreator = deckCreator;
        }

        public async UniTask GetCurrentCardAnimation()
        { 
            _cts = new CancellationTokenSource();
             _gameBoard.CurrentCard.transform.DOMove(new Vector3(_gameBoard.CurrentCardPlace.position.x, 
                 _gameBoard.CurrentCardPlace.position.y, 10f), 0.4f).
             SetEase(Ease.OutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));
            _cts.Cancel();
        }

        public async UniTask RotateCard(GameObject card, float angle) => await 
            card.transform.DOLocalRotate(new Vector3(0, angle, 0), 0.1f).SetEase(Ease.OutBack);

        public async UniTask DeckStartAnimation(List<GameObject> cards)
        {
            var cardPoolPos = 0.03f;
            foreach (var card in cards)
            {
                await card.transform.DOMove(new Vector3(_gameBoard.DeckPlace.position.x, 
                        _gameBoard.DeckPlace.position.y + cardPoolPos, 10f), 0.02f).
                    SetEase(Ease.OutBack);
                cardPoolPos-=0.03f;
            }

            _gameBoard.Initialize();
            await AnimateLines(cards);
        }

        private async UniTask AnimateLines(List<GameObject> cards)
        {
            await AnimateLine(_gameBoard.FirstLinePlacer, cards, 2, _gameBoard.FirstLineCards);
            await AnimateLine(_gameBoard.SecondLinePlacer, cards, 3, _gameBoard.SecondLineCards);
            await AnimateLine(_gameBoard.ThirdLinePlacer, cards, 4, _gameBoard.ThirdLineCards);
            await AnimateLine(_gameBoard.FourLinePlacer, cards, 5, _gameBoard.FourLineCards);
        }

        private async UniTask AnimateLine(List<Transform> linePlacer, List<GameObject> cards, int order, List<GameObject> lineCard)
        {
            _cts = new CancellationTokenSource();
            foreach (var place in linePlacer)
            {
                var card = cards[^1];
                card.transform.DOMove(place.position, 0.3f).
                    SetEase(Ease.OutBack);
                _deckCreator.RemoveCardFromPool();
                card.layer = LayerMask.NameToLayer("Movable");
                card.GetComponent<Card.Card>().SetOrderLayer(order);
                _gameBoard.AddCard(lineCard, card);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            _cts.Cancel();
        }
    }
}