using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Deck;
using Infrastructure.GameCard;
using Logic.Animations;
using UnityEngine;

namespace Logic.Interactive
{
    public class OpenCardService : IOpenCardService, IDisposable
    {
        private readonly ICardsDeck _cardsDeck;
        private readonly ICardAnimator _cardAnimator;
        private CancellationTokenSource _cts;

        public OpenCardService(ICardAnimator cardAnimator, ICardsDeck cardsDeck)
        {
            _cardAnimator = cardAnimator;
            _cardsDeck = cardsDeck;
        }
        public void Dispose() => _cts?.Dispose();

        public async void OpenStartCards()
        {
            for (int i = 18; i < 28; i++) 
                await  OpenCard(_cardsDeck.Deck[i].gameObject, true);
        }

        public async UniTask CheckForOpen()
        {
            var cardDeck = _cardsDeck.Deck;
            // FIRST LINE CHECK
            if (cardDeck[3] == null && cardDeck[4] == null && cardDeck[0] && cardDeck[0].IsFaceUp == false)
                await OpenCard(cardDeck[0].gameObject, true);
            if (cardDeck[5] == null && cardDeck[6] == null && cardDeck[1] && cardDeck[1].IsFaceUp == false)
                await OpenCard(cardDeck[1].gameObject, true);
            if (cardDeck[7] == null && cardDeck[8] == null && cardDeck[2] && cardDeck[2].IsFaceUp == false)
                await OpenCard(cardDeck[2].gameObject, true);
            // SECOND LINE CHECK
            if (cardDeck[9] == null && cardDeck[10] == null && cardDeck[3] && cardDeck[3].IsFaceUp == false)
                await OpenCard(cardDeck[3].gameObject, true);
            if (cardDeck[10] == null && cardDeck[11] == null && cardDeck[4] && cardDeck[4].IsFaceUp == false)
                await OpenCard(cardDeck[4].gameObject, true);
            if (cardDeck[12] == null && cardDeck[13] == null && cardDeck[5] && cardDeck[5].IsFaceUp == false)
                await OpenCard(cardDeck[5].gameObject, true);
            if (cardDeck[13] == null && cardDeck[14] == null && cardDeck[6] && cardDeck[6].IsFaceUp == false)
                await OpenCard(cardDeck[6].gameObject, true);
            if (cardDeck[15] == null && cardDeck[16] == null && cardDeck[7] && cardDeck[7].IsFaceUp == false)
                await OpenCard(cardDeck[7].gameObject, true);
            if (cardDeck[16] == null && cardDeck[17] == null && cardDeck[8] && cardDeck[8].IsFaceUp == false)
                await OpenCard(cardDeck[8].gameObject, true);
            // THIRD LINE CHECK
            if (cardDeck[18] == null && cardDeck[19] == null && cardDeck[9] && cardDeck[9].IsFaceUp == false)
                await OpenCard(cardDeck[9].gameObject, true);
            if (cardDeck[19] == null && cardDeck[20] == null && cardDeck[10] && cardDeck[10].IsFaceUp == false)
                await OpenCard(cardDeck[10].gameObject, true);
            if (cardDeck[20] == null && cardDeck[21] == null && cardDeck[11] && cardDeck[11].IsFaceUp == false)
                await OpenCard(cardDeck[11].gameObject, true);
            if (cardDeck[21] == null && cardDeck[22] == null && cardDeck[12] && cardDeck[12].IsFaceUp == false)
                await OpenCard(cardDeck[12].gameObject, true);
            if (cardDeck[22] == null && cardDeck[23] == null && cardDeck[13] && cardDeck[13].IsFaceUp == false)
                await OpenCard(cardDeck[13].gameObject, true);
            if (cardDeck[23] == null && cardDeck[24] == null && cardDeck[14] && cardDeck[14].IsFaceUp == false)
                await OpenCard(cardDeck[14].gameObject, true);
            if (cardDeck[24] == null && cardDeck[25] == null && cardDeck[15] && cardDeck[15].IsFaceUp == false)
                await OpenCard(cardDeck[15].gameObject, true);
            if (cardDeck[25] == null && cardDeck[26] == null && cardDeck[16] && cardDeck[16].IsFaceUp == false)
                await OpenCard(cardDeck[16].gameObject, true);
            if (cardDeck[26] == null && cardDeck[27] == null && cardDeck[17] && cardDeck[17].IsFaceUp == false)
                await OpenCard(cardDeck[17].gameObject, true);
        }

        public async UniTask OpenCard(GameObject card, bool isInteractable)
        {
            _cts = new CancellationTokenSource();
            if(card == null) return;
            await _cardAnimator.RotateCard(card, 90f);
            Card cardComponent = card.GetComponent<Card>();
            cardComponent.SetSide(true);
            if(isInteractable)
                cardComponent.IsInteractable(true);
            _cardAnimator.RotateCard(card, 0f);
            _cts.Cancel();
        }
        
    }
}