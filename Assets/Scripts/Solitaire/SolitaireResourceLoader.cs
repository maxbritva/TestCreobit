using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Solitaire
{
    public class SolitaireResourceLoader
    {
        public List<Sprite> FaceCards { get; private set; }
        public GameObject CardPrefab { get; private set; }
        public Sprite BackFace { get; private set; }
        private CancellationTokenSource _cts;
        public async UniTask LoadResources(List<string> deck)
        {
            _cts = new CancellationTokenSource();
            FaceCards = new List<Sprite>();
            foreach (var card in deck) 
                FaceCards.Add(await Loader<Sprite>(card));
            CardPrefab = await Loader<GameObject>("CardPrefab");
            BackFace = await Loader<Sprite>("BackFace");
            _cts.Cancel();
        }

        private async UniTask<T> Loader<T>(string key)
        {
            var asset = Addressables.LoadAssetAsync<T>(key);
            var uniTask = await asset.ToUniTask();
            return asset.Status == AsyncOperationStatus.Succeeded ? uniTask : default;
        }
    }
}