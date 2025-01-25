using Loader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Solitaire
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private TMP_Text _conditionText;
        [SerializeField] private Button _endGame;
        [SerializeField] private Button _retry;
        private SceneLoader _sceneLoader;

        public void ShowEndGamePanel(bool condition)
        {
            _endGamePanel.SetActive(true);
            _conditionText.text = condition ? "YOU WIN!" : "YOU LOOSE!";
        }
        private void OnEnable()
        {
            _endGame.onClick.AddListener(EndGameButtonClick);
            _retry.onClick.AddListener(RetryButtonClick);
        }

        private void OnDisable()
        {
            _endGame.onClick.RemoveListener(EndGameButtonClick);
            _retry.onClick.RemoveListener(RetryButtonClick);
        }

        private void EndGameButtonClick() => _sceneLoader.LoadMenu();

        private void RetryButtonClick() => _sceneLoader.LoadSolitaire();

        [Inject]
        private void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;
    }
}