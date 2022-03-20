using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
	public class CardGrid : MonoBehaviour
	{
		[SerializeField]
		private RectTransform _gridRect;

		[SerializeField]
		private CardSpawner _cardSpawner;

		private LevelConfiguration _levelConfiguration;

		private Vector2[,] _elementPositions;

		private List<CardView> _currentElements = new List<CardView>();

		private List<CardData> _chosenCards;

		public void Initialize(LevelConfiguration configuration,
			List<CardData> chosenCards, ObjectiveController objectiveController)
		{
			ResetValues();

			_levelConfiguration = configuration;
			_chosenCards = chosenCards;

			_cardSpawner.Initialize(_levelConfiguration, objectiveController);

			FillElementPositions();
			FillGridWithNewCards(true);
		}

		public void UpdateData(LevelConfiguration configuration,
			List<CardData> chosenCards)
		{
			ResetValues();

			_levelConfiguration = configuration;
			_chosenCards = chosenCards;

			_cardSpawner.UpdateData(_levelConfiguration);

			FillElementPositions();
			FillGridWithNewCards();
		}

		public void AppendCardToGrid(CardView cardView)
		{
			_currentElements.Add(cardView);

			cardView.gameObject.transform.SetParent(_gridRect);

			var currentElementPositionIndices = GetElementPositionInGrid(
				_currentElements.Count - 1,
				_levelConfiguration.NumberOfRows,
				_levelConfiguration.NumberOfColumns);

			cardView.gameObject.transform.localPosition = _elementPositions[
				currentElementPositionIndices.x,
				currentElementPositionIndices.y
			];
		}

		private void FillGridWithNewCards(bool isAnimatedAppearence = false)
		{
			foreach (var card in _chosenCards)
			{
				var cardView = _cardSpawner.GetNewViewForCard(card, isAnimatedAppearence);
				AppendCardToGrid(cardView);
			}
		}

		private void FillElementPositions()
		{
			var numberOfRows = _levelConfiguration.NumberOfRows;
			var numberOfColumns = _levelConfiguration.NumberOfColumns;
			var elementSize = _cardSpawner.CardPrefabSize;

			_elementPositions = new Vector2[numberOfRows, numberOfColumns];

			var gridTargetSize = new Vector2(
				elementSize.x * numberOfColumns,
				elementSize.y * numberOfRows
			);

			float startPosX = -(gridTargetSize.x / 2) + (elementSize.x / 2);
			float startPosY = -(gridTargetSize.y / 2) + (elementSize.y / 2);

			float currentPosX = startPosX;
			float currentPosY = startPosY;

			for (int i = 0; i < numberOfRows; i++)
			{
				currentPosX = startPosX;

				for (int j = 0; j < numberOfColumns; j++)
				{
					_elementPositions[i, j] = new Vector2(currentPosX, currentPosY);

					currentPosX += elementSize.x;
				}

				currentPosY += elementSize.y;
			}
		}

		/// <summary>
		/// Grid fills from left to right, from bottom to top
		/// </summary>
		private Vector2Int GetElementPositionInGrid(int elementIndex, int numberOfRows, int numberOfColumns)
		{
			return new Vector2Int(
				elementIndex / numberOfColumns,
				elementIndex % numberOfColumns);
		}

		private void DestroyCardViews()
		{
			foreach (var it in _currentElements)
				Destroy(it.gameObject);
		}

		private void ResetValues()
		{
			DestroyCardViews();

			_elementPositions = null;

			_currentElements.Clear();
		}
	}
}
