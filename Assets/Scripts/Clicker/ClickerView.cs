using Cysharp.Threading.Tasks;
using DG.Tweening;
using Loader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Clicker
{
    public class ClickerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _clickCountText;
        [SerializeField] private Button _buttonClicker;
        [SerializeField] private GameObject _buttonGameObject;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _solitaireButton;
        private ClickCounter _clickCounter;
        private ClickProgressSaver _clickProgressSaver;
        private SceneLoader _sceneLoader;
        private Vector3 _scale;

        private void OnEnable()
        {
            _scale = _buttonGameObject.transform.localScale;
            _clickCounter.OnClickChanged += OnClicker;
            _buttonClicker.onClick.AddListener(ButtonClick);
            _menuButton.onClick.AddListener(MenuButtonOnClick);
            _solitaireButton.onClick.AddListener(SolitaireButtonOnClick);
            OnClicker();
        }

        private void OnDisable()
        {
            _clickCounter.OnClickChanged -= OnClicker;
            _buttonClicker.onClick.RemoveListener(ButtonClick);
            _menuButton.onClick.RemoveListener(MenuButtonOnClick);
            _solitaireButton.onClick.RemoveListener(SolitaireButtonOnClick);
        }

        private void OnClicker() => _clickCountText.text = "Clicked: " + _clickCounter.ClickCount;

        private async void ButtonClick()
        {
            _clickCounter.AddClick();
           await AnimateButton();
        }

        private void MenuButtonOnClick()
        {
            _clickProgressSaver.Save();
            _sceneLoader.LoadMenu();
        }
        
        private void SolitaireButtonOnClick()
        {
            _clickProgressSaver.Save();
            _sceneLoader.LoadSolitaire();
        }

        private async UniTask AnimateButton()
        {
            await _buttonGameObject.transform.DOPunchScale(_scale * 1.05f, 0.2f, 1);
            _buttonGameObject.transform.DOScale(_scale, 0.2f);
        }


        [Inject] private void Construct(ClickCounter clickCounter, ClickProgressSaver clickProgressSaver,
            SceneLoader sceneLoader)
        {
            _clickCounter = clickCounter;
            _clickProgressSaver = clickProgressSaver;
            _sceneLoader = sceneLoader;
        }
    }
}