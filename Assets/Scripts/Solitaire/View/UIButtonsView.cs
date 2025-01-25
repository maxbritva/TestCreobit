using Loader;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Solitaire.View
{
    public class UIButtonsView : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _clickerButton;

        private SceneLoader _sceneLoader;

        public void ActivateButtons()
        {
            Debug.Log("activate");
            _menuButton.interactable = true;
            _clickerButton.interactable = true;
        }
        private void OnEnable()
        {
            _menuButton.onClick.AddListener(MenuButtonOnClick);
            _clickerButton.onClick.AddListener(ClickerButtonOnClick);
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(MenuButtonOnClick);
            _clickerButton.onClick.RemoveListener(ClickerButtonOnClick);
        }
        private void MenuButtonOnClick() => _sceneLoader.LoadMenu();
        private void ClickerButtonOnClick() => _sceneLoader.LoadClicker();

        [Inject] private void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;
    }
}