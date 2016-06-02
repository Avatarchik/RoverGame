﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

/// <summary>
/// A level used with LevelsManager Component.
/// When you create a new level using Inspector ,you will create an instace of this class
/// </summary>
[System.Serializable]
public class Level
{
	public enum WireTypes
	{
		Aluminum,
		Copper,
		Gold,
		Silver
	}
		/// <summary>
		/// Whether the level is visible
		/// </summary>
		public bool showLevel = false;

		/// <summary>
		/// Whether to show the number of the pairs.
		/// </summary>
		public bool showPairsNumber = true;

		/// <summary>
		/// The dots(elements) pairs list.
		/// </summary>
		public List<DotsPair> dotsPairs = new List<DotsPair> ();

    public List<Barrier> barriers = new List<Barrier>();

		/// <summary>
		/// DotsPair Class.
	    /// Note : Dots Pair is an Elements Pair , we did not rename this class and its instances to 
		///        avoid reference losing for each level,hopefully the unity will fix this issue in the future. 
		/// </summary>
		[System.Serializable]
		public class DotsPair
		{
		public int wireIndex;		
		public WireTypes wireType;

				/// <summary>
				/// Whether the pair is visible(used with inspector only).
				/// </summary>
				public bool showPair = true;

				/// <summary>
				/// Whether to apply the color on the sprite.
				/// </summary>
				public bool applyColorOnSprite = true;

				/// <summary>
				/// The sprite of the pair.
				/// </summary>
				public Sprite pairSprite;

				/// <summary>
				/// The connect sprite.
				/// </summary>
				public Sprite connectSprite;

				/// <summary>
				/// The connect sprite.
				/// </summary>
				public Sprite connectBGSprite;

				/// <summary>
				/// The color of the pair.
				/// </summary>
				public Color pairColor = Color.red;

				/// <summary>
				/// The color of the pair.
				/// </summary>
				public Color onConnectColor = Color.red;

				/// <summary>
				/// The color of the pair.
				/// </summary>
				public Color bgColor = Color.red;

		    	/// <summary>
		   	 	/// The color of the line.
		    	/// </summary>
				public Color lineColor;

				/// <summary>
				/// The first dot(element) of the pair.
				/// </summary>
				public Dot firstDot = new Dot ();

				/// <summary>
				/// The second dot(element) of the pair.
				/// </summary>
				public Dot secondDot = new Dot ();
		}

		/// <summary>
		/// Dot(Element) Class.
		/// </summary>
		[System.Serializable]
		public class Dot
		{
				/// <summary>
				/// The index of the dot(element) in the Grid.
				/// </summary>
				public int index;
		}


    [System.Serializable]
    public class Barrier
    {
        public bool showPair = true;

        public Sprite sprite;
        public Color color;
        public int index;
    }
}