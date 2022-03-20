using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
	public class MatchController : MonoBehaviour
	{
		[SerializeField]
		private MatchConfiguration _matchConfiguration;

		[SerializeField]
		private LevelController _levelController;

		[SerializeField]
		private Image _endGamePanelImage;

		[SerializeField]
		private GameObject _restartButton;

		private ObjectiveController _objectiveController = new ObjectiveController();

		private int _currentLevelIndex;

		public void OnRestartButtonClick()
		{
			InitNewMatch();
		}

		private void Start()
		{
			_objectiveController.AddSuccessCheckListener(OnLevelFinished);

			InitNewMatch();
		}

		private void ShowEndGameUI()
		{
			_restartButton.SetActive(true);
			_endGamePanelImage.gameObject.SetActive(true);
			_endGamePanelImage.DoFadeInAnimation();
		}

		private void InitNewMatch()
		{
			_endGamePanelImage.gameObject.SetActive(false);
			_restartButton.SetActive(false);

			_currentLevelIndex = 0;

			InitLevel(_matchConfiguration.Levels[_currentLevelIndex]);
		}

		private void OnLevelFinished()
		{
			_currentLevelIndex++;

			if (!IsMatchCompleted())
				UpdateLevel(_matchConfiguration.Levels[_currentLevelIndex]);
			else
				ShowEndGameUI();
		}

		private void InitLevel(LevelConfiguration levelConfiguration)
		{
			_levelController.Initialize(_matchConfiguration.Levels[_currentLevelIndex], _objectiveController);
		}

		private void UpdateLevel(LevelConfiguration levelConfiguration)
		{
			_levelController.UpdateData(_matchConfiguration.Levels[_currentLevelIndex]);
		}

		private bool IsMatchCompleted()
		{
			return _currentLevelIndex >= _matchConfiguration.Levels.Count;
		}
	}
}