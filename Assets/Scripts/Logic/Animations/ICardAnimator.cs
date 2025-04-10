using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Logic.Animations
{
    public interface ICardAnimator : IService
    {
        UniTask DeckInitAnimation();
        UniTask SetCardsStartPosition();
        UniTask GetCurrentCardAnimation();
        UniTask RotateCard(GameObject card, float angle);
        UniTask MoveCard(GameObject card, Vector3 position);
    }
}