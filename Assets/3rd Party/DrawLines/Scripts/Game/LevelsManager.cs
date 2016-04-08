using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

[DisallowMultipleComponent]
public class LevelsManager : MonoBehaviour
{
		[SerializeField]
		public Sprite defaultSprite;
		public bool createRandomColor = true;
		public readonly static int rowsLimit = 12;
		public readonly static int colsLimit = 12;
		public int numberOfCols = 5;
		public int numberOfRows = 5;
    public List<Level> levels = new List<Level>();
}