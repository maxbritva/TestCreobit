using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public UniTask LoadResources(List<string> deck);
        public GameObject InstantiateCard(Transform parent);
        public UniTask<T> Loader<T>(string key);
        List<Sprite> FaceCards { get; set; }
        GameObject CardPrefab { get; }
        Sprite BackFace { get; }
        GameObject InstantiateHud();
    }
}