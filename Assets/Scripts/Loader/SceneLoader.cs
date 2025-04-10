using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Loader
{
    public class SceneLoader
    {
        private CancellationTokenSource _cts;
        
        public async UniTask LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                return;
            }
            _cts = new CancellationTokenSource();
            var waitNextScene = Addressables.LoadSceneAsync(name);
            while (waitNextScene.IsDone == false) 
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            onLoaded?.Invoke();
        }
        
    }
}