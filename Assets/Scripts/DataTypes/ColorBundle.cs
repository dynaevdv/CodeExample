using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
	[CreateAssetMenu(fileName = "New Color Bundle", menuName = "Color Bundle", order = 50)]
	public class ColorBundle : ScriptableObject
	{
		[SerializeField]
		private List<Color> _colors;

		public List<Color> Colors => _colors;
	}
}