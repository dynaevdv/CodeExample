using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
	public class CardView : MonoBehaviour
	{
		[SerializeField]
		private Image _mainImage;

		[SerializeField]
		private RectTransform _mainImageRect;

		[SerializeField]
		private Image _background;

		private string _identifier;
		private Action<string> _onClickEvent;

		public string Identifier => _identifier;
		public Sprite MainSprite => _mainImage.sprite;
		public RectTransform MainImageRect => _mainImageRect;

		public void Initialize(CardData cardData, Color colorBackgroundColor)
		{
			_mainImage.sprite = cardData.Sprite;
			_background.color = colorBackgroundColor;
			_identifier = cardData.Identifier;
		}

		public void AddOnClickHandler(Action<string> clickedCardIdHandler)
		{
			_onClickEvent += clickedCardIdHandler;
		}

		public void HandleCardVerifyResult(bool isSuccess)
		{
			if (!isSuccess)
				this.transform.DoShakeAnimation();
		}

		public void OnCardClick()
		{
			_onClickEvent?.Invoke(_identifier);
		}
	}
}