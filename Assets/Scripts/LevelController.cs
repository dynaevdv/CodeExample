using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CardGame
{
	public class LevelController : MonoBehaviour
	{
		[SerializeField]
		private CardGrid _cardGrid;

		[SerializeField]
		private TMP_Text _objectiveText;

		private LevelConfiguration _currentConfiguration;

		private ObjectiveController _objectiveController;

		public void Initialize(LevelConfiguration configuration, ObjectiveController objectiveController)
		{
			_currentConfiguration = configuration;

			_objectiveController = objectiveController;

			List<CardData> _chosenCards = ChooseCardsForCurrentLevel();

			_objectiveController.Initialize(_chosenCards, _objectiveText);

			_cardGrid.Initialize(_currentConfiguration, _chosenCards, _objectiveController);
		}

		public void UpdateData(LevelConfiguration configuration)
		{
			_currentConfiguration = configuration;

			List<CardData> _chosenCards = ChooseCardsForCurrentLevel();

			_objectiveController.UpdateActiveCards(_chosenCards);

			_cardGrid.UpdateData(_currentConfiguration, _chosenCards);
		}

		/// <summary>
		/// Choose and randomize required part of cards for current level
		/// </summary>
		private List<CardData> ChooseCardsForCurrentLevel()
		{
			var random = new System.Random();
			var numberOfCards = _currentConfiguration.NumberOfRows * _currentConfiguration.NumberOfColumns;

			// Should avoid LINQ for performance reason but for MVP it is ok
			return _currentConfiguration.CardBundle.Cards.OrderBy(x => random.Next()).Take(numberOfCards).ToList();
		}
	}
}