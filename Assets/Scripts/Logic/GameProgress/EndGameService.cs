using System;
using Data;
using Loader;

namespace Logic.GameProgress
{
    public class EndGameService : IEndGameService
    {
        public event Action OnGameEnd;
        public bool IsWin { get; private set; }
        public bool IsWinCondition(bool condition) => IsWin = condition;
        private SceneLoader _sceneLoader;

        public EndGameService(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void ShowEndGamePanel() => OnGameEnd?.Invoke();


        public async void StartNewGame()
        {
            await _sceneLoader.LoadScene(StaticDataNames.InitialScene);
        }

        public void RetryCurrentGame()
        {
            
        }
    }
}