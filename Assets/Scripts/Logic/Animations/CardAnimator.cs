using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using Infrastructure.Deck;
using UnityEngine;

namespace Logic.Animations
{
    public class CardAnimator : ICardAnimator
    {
        private readonly ICardsDeck _cardsDeck;
        private CancellationTokenSource _cts;
        private readonly Vector3 _cardScale = Vector3.one * 0.77f;
        
        public CardAnimator(ICardsDeck cardsDeck) => _cardsDeck = cardsDeck;

        public async UniTask DeckInitAnimation()
        {
            var cardDeckYOffset = 0f;

            for (var cardCounter = _cardsDeck.Deck.Count -1; cardCounter >= 0; cardCounter--)
            {
                var card = _cardsDeck.Deck[cardCounter].gameObject;
                card.transform.position =
                    new Vector3(CardPositions.DeckPosition.x, CardPositions.DeckPosition.y + cardDeckYOffset, 0);
                card.transform.DOScale(_cardScale, 0.8f).SetEase(Ease.OutBack);
                cardDeckYOffset-=0.015f;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));
            await SetCardsStartPosition();
        }

        public async UniTask SetCardsStartPosition()
        {
            var cardOrderLayer = 0;
            _cts = new CancellationTokenSource();
            for (int i = 0; i < 28; i++)
            {
                _cardsDeck.Deck[i].SetOrderLayer(cardOrderLayer);
                cardOrderLayer++;
                _cardsDeck.Deck[i].IsInteractable(true);
                await _cardsDeck.Deck[i].transform.DOMove(
                    CardPositions.BoardPositions[i], 0.07f).SetEase(Ease.OutBack);
            }
            _cts.Cancel();
        }

        public async UniTask GetCurrentCardAnimation()
        { 
            _cts = new CancellationTokenSource();
            await _cardsDeck.CurrentCard.transform.DOMove(CardPositions.CurrentCardPosition, 0.5f).SetEase(Ease.OutBack);
           // await UniTask.Delay(TimeSpan.FromSeconds(0.8f));
            _cts.Cancel();
        }
        public async UniTask RotateCard(GameObject card, float angle) => await 
            card.transform.DOLocalRotate(new Vector3(0, angle, 0), 0.1f).SetEase(Ease.OutBack);

        public async UniTask MoveCard(GameObject card, Vector3 position) =>
           await card.transform.DOMove(position, 0.3f).SetEase(Ease.OutBack);

    }
}