
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardGame
{
	public class ObjectiveController
	{
		private List<CardData> _activeCards;
		private List<CardData> _usedCardsHistory = new List<CardData>();
		private TMP_Text _objectiveText;
		private CardData _currentObjective;
		private Dictionary<string, Action<bool>> _verifyHandlerByCardId =
				new Dictionary<string, Action<bool>>();
		private Action _onSuccessCheckEvent;

		public string CorrectChooseIdentifier => _currentObjective.Identifier;

		public void Initialize(List<CardData> cardData, TMP_Text objectiveText)
		{
			_activeCards = cardData;
			_objectiveText = objectiveText;

			ResetPrivateVariables();
			UpdateCurrentObjective();
			UpdateObjectiveText(true);
		}

		public void UpdateActiveCards(List<CardData> activeCards)
		{
			_activeCards = activeCards;

			ResetPrivateVariables();
			UpdateCurrentObjective();
			UpdateObjectiveText();
		}

		private void ResetPrivateVariables()
		{
			_currentObjective = null;
			_verifyHandlerByCardId.Clear();
		}

		public void AddSuccessCheckListener(Action listener)
		{
			_onSuccessCheckEvent += listener;
		}

		public void SetOnClickHandlerForCard(Action<bool> handler, string cardId)
		{
			_verifyHandlerByCardId[cardId] = handler;
		}

		public void VerifyClickedCard(string clickedCardId)
		{
			if (!_verifyHandlerByCardId.ContainsKey(clickedCardId))
				return;

			bool isSuccess = clickedCardId == _currentObjective.Identifier;

			if (isSuccess)
				_onSuccessCheckEvent?.Invoke();
			else
				_verifyHandlerByCardId[clickedCardId]?.Invoke(isSuccess);
		}

		private void UpdateObjectiveText(bool isTextAppearsWithAnimation = false)
		{
			_objectiveText.text = $"Find {_currentObjective.Identifier}";

			if (isTextAppearsWithAnimation)
				_objectiveText.DoFadeInAnimation();
		}

		// TODO: Prevent infinite while loop
		private void UpdateCurrentObjective()
		{
			_currentObjective = GetRandomActiveCard();

			if (!IsCurrentObjectiveNew())
				UpdateCurrentObjective();
		}

		private bool IsCurrentObjectiveNew()
		{
			return !_usedCardsHistory.Any(cardData => cardData.Identifier == _currentObjective.Identifier);
		}

		private CardData GetRandomActiveCard()
		{
			return _activeCards[UnityEngine.Random.Range(0, _activeCards.Count - 1)];
		}
	}
}