using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
	[CreateAssetMenu(fileName = "New Card Bundle", menuName = "Card Bundle", order = 50)]
	public class CardBundle : ScriptableObject
	{
		[SerializeField]
		private List<CardData> _cards;

		public List<CardData> Cards => _cards;
	}
}