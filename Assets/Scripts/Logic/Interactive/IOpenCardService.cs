using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Logic.Interactive
{
    public interface IOpenCardService : IService
    {
        UniTask CheckForOpen();
        void OpenStartCards();
        UniTask OpenCard(GameObject card, bool isInteractable);
    }
}