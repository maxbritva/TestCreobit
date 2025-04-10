using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Loader
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        private CancellationTokenSource _cts;
        
        private void Awake() => DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1f;
        }

        public async UniTask Hide()
        {
            _cts = new CancellationTokenSource();
           while(_curtain.alpha > 0)
           {
               _curtain.alpha -= 0.03f;
               UniTask.Delay(TimeSpan.FromSeconds(0.03f), _cts.IsCancellationRequested);
           }
           gameObject.SetActive(false);
           _cts.Cancel();
        }
    }
}