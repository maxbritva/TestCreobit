using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loader
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        private const string ClickerScene = "Clicker";
        private const string SolitaireScene = "Solitaire";
        private const string MenuScene = "Menu";

        public void ShowOffScreen(bool value) => _loadingScreen.SetActive(value);

        public async void LoadClicker()
        {
            ShowOffScreen(true);
            await SceneManager.LoadSceneAsync(ClickerScene);
            ShowOffScreen(false);
        }

        public async void LoadSolitaire()
        {
            ShowOffScreen(true);
            await SceneManager.LoadSceneAsync(SolitaireScene);
        }

        public async void LoadMenu()
        {
            ShowOffScreen(true);
            await SceneManager.LoadSceneAsync(MenuScene);
            ShowOffScreen(false);
        }
    }
}