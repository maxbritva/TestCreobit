using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public List<Sprite> FaceCards { get; set; }
        public GameObject CardPrefab { get; private set; }
        public GameObject HudPrefab { get; private set; }
        public Sprite BackFace { get; private set; }
        private CancellationTokenSource _cts;
        public async UniTask LoadResources(List<string> deck)
        {
            _cts = new CancellationTokenSource();
            FaceCards = new List<Sprite>();
            foreach (var card in deck) 
                FaceCards.Add(await Loader<Sprite>(card));
            CardPrefab = await Loader<GameObject>(AssetPaths.CardPrefabKey);
            BackFace = await Loader<Sprite>(AssetPaths.BackfaceKey);
            HudPrefab = await Loader<GameObject>(AssetPaths.HudKey);
            _cts.Cancel();
        }
        public GameObject InstantiateCard(Transform parent) =>
            GameObject.Instantiate(CardPrefab,
                CardPositions.DeckPosition,
                quaternion.identity, parent);

        public GameObject InstantiateHud() =>
            GameObject.Instantiate(HudPrefab);

        public async UniTask<T> Loader<T>(string key)
        {
            var asset = Addressables.LoadAssetAsync<T>(key);
            var uniTask = await asset.ToUniTask();
            return asset.Status == AsyncOperationStatus.Succeeded ? uniTask : default;
        }
    }
}