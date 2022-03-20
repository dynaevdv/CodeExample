using UnityEngine;

namespace CardGame
{
	[CreateAssetMenu(fileName = "New Level Configuration", menuName = "Level Configuration", order = 50)]
	public class LevelConfiguration : ScriptableObject
	{
		[SerializeField]
		private CardBundle _cardBundle;

		[SerializeField]
		private ColorBundle _colorBundle;

		[SerializeField]
		private int _numberOfRows;

		[SerializeField]
		private int _numberOfColumns;

		public CardBundle CardBundle => _cardBundle;
		public ColorBundle ColorBundle => _colorBundle;
		public int NumberOfRows => _numberOfRows;
		public int NumberOfColumns => _numberOfColumns;
	}
}