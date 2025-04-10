using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;

namespace Logic.GameProgress
{
    public interface IGameTimer: IService
    {
        event Action OnTimeChanged;
        int Time { get; }
        UniTask StartTimer();
        void StopTimer();
    }
}