using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
	[CreateAssetMenu(fileName = "New Match Configuration", menuName = "Match Configuration", order = 50)]
	public class MatchConfiguration : ScriptableObject
	{
		[SerializeField]
		private List<LevelConfiguration> _levels;

		public List<LevelConfiguration> Levels => _levels;
	}
}