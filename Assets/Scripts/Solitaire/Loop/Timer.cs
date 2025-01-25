using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Solitaire.Loop
{
    public class Timer: IDisposable
    {
        public event Action OnTimeChanged;
        public int Time { get; private set; }
        private CancellationTokenSource _cts;


        public async UniTask StartTimer()
        {
            _cts = new CancellationTokenSource();
            while (_cts.IsCancellationRequested == false)
            {
              await  UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.DeltaTime);
                Time++;
                OnTimeChanged?.Invoke();
            }
        }

        public void StopTimer() => _cts.Cancel();

        public void Dispose() => _cts?.Dispose();
    }
}