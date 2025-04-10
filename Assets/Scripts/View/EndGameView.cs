using Logic.GameProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private TMP_Text _conditionText;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _retryGameButton;
        private IEndGameService _endGameService;

        public void Construct(IEndGameService endGameService)
        {
            _endGameService = endGameService;
            _endGameService.OnGameEnd += ShowEndGamePanel;
            _newGameButton.onClick.AddListener(NewGameButtonClick);
            _retryGameButton.onClick.AddListener(NewGameButtonClick);
        }
        private void OnDisable()
        {
            _endGameService.OnGameEnd -= ShowEndGamePanel;
            _newGameButton.onClick.RemoveListener(NewGameButtonClick);
            _retryGameButton.onClick.RemoveListener(RetryButtonClick);
        }

        private void ShowEndGamePanel()
        {
            _endGamePanel.SetActive(true);
            _conditionText.text = _endGameService.IsWin ? "YOU WIN!" : "YOU LOOSE!";
        }

        private void NewGameButtonClick() => _endGameService.StartNewGame();

        private void RetryButtonClick() => _endGameService.RetryCurrentGame();

    }
}