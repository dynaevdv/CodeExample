using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
	public class CardSpawner : MonoBehaviour
	{
		[SerializeField]
		private CardView _cardPrefab;

		[SerializeField]
		private RectTransform _cardPrefabRect;

		[SerializeField]
		private bool _shouldCheckSpriteOrientation;

		private LevelConfiguration _levelConfiguration;
		private ObjectiveController _objectiveController;

		public Vector2 CardPrefabSize => _cardPrefabRect.sizeDelta;

		public void Initialize(LevelConfiguration configuration, ObjectiveController objectiveController)
		{
			_levelConfiguration = configuration;
			_objectiveController = objectiveController;
		}

		public void UpdateData(LevelConfiguration configuration)
		{
			_levelConfiguration = configuration;
		}

		public CardView GetNewViewForCard(CardData it, bool isAnimatedAppearence = false)
		{
			CardView cardView = Instantiate(_cardPrefab);
			cardView.Initialize(it, GetRandomizedCardColorFromBundle(_levelConfiguration.ColorBundle));

			RegisterObjectiveControllerHandlersForCard(cardView);

			if (isAnimatedAppearence)
				AnimateCardAppearence(cardView);

			if (_shouldCheckSpriteOrientation)
				CheckForCorrectSpriteRotation(cardView);

			return cardView;
		}

		private void AnimateCardAppearence(CardView cardView)
		{
			cardView.gameObject.transform.DoBounceAnimation();
		}

		private void RegisterObjectiveControllerHandlersForCard(CardView cardView)
		{
			cardView.AddOnClickHandler(_objectiveController.VerifyClickedCard);

			_objectiveController.SetOnClickHandlerForCard(cardView.HandleCardVerifyResult, cardView.Identifier);
		}

		/// <summary>
		/// Temporary solution for wrong-orientated sprites issue. 
		/// Suppose that sprites can be portrait or 90-degrees counterclockwise 
		/// rotated
		/// </summary>
		private void CheckForCorrectSpriteRotation(CardView cardView)
		{
			if (cardView.MainSprite.name.Contains("should rotate"))
				cardView.MainImageRect.transform.rotation = Quaternion.Euler(0, 0, -90f);
		}

		/// <summary>
		/// Choose random color from level configuration
		/// </summary>
		private Color GetRandomizedCardColorFromBundle(ColorBundle colorBundle)
		{
			var colorsList = colorBundle.Colors;
			return colorsList[UnityEngine.Random.Range(0, colorsList.Count - 1)];
		}
	}
}