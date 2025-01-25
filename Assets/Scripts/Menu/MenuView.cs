using Loader;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonGameClicker;
        [SerializeField] private Button _buttonGameSolitaire;
        private SceneLoader _sceneLoader;

        public void OnEnable()
        {
            _buttonGameClicker.onClick.AddListener(ButtonClickerOnClick);
            _buttonGameSolitaire.onClick.AddListener(ButtonSolitaireOnClick);
        }

        private void OnDisable()
        {
            _buttonGameClicker.onClick.RemoveListener(ButtonClickerOnClick);
            _buttonGameSolitaire.onClick.RemoveListener(ButtonSolitaireOnClick);
        }

        private void ButtonClickerOnClick() => _sceneLoader.LoadClicker();
        private void ButtonSolitaireOnClick() => _sceneLoader.LoadSolitaire();

        [Inject] private void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;
    }
}