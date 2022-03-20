using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
	public static class AnimationController
	{
		public static void DoBounceAnimation(this Transform transform)
		{
			transform.localScale = new Vector3(0, 0, 0);
			transform.DOScale(1.0f, 1.2f).SetEase(Ease.OutBounce);
		}

		public static void DoFadeInAnimation(this TMP_Text text)
		{
			text.DOFade(0.0f, 0.0f);
			text.DOFade(1.0f, 2.0f);
		}

		public static void DoFadeInAnimation(this Image image)
		{
			image.DOFade(0.0f, 0.0f);
			image.DOFade(0.7f, 1.5f);
		}

		public static void DoShakeAnimation(this Transform transform)
		{
			transform.DOShakePosition(0.5f, 20.0f).SetEase(Ease.InBounce);
		}
	}
}