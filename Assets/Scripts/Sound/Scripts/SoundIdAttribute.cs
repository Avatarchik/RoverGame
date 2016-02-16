using UnityEngine;
using System.Collections;


public class SoundIdAttribute : PropertyAttribute
{
	public SoundType type = SoundType.Any;


	public SoundIdAttribute () {}


	public SoundIdAttribute (params SoundType[] types)
	{
		this.type = SoundTypeExtensions.Combine (types);
	}
}

