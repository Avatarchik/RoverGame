using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class SoundSettings
{
	private const string KEY_MUSIC_VOLUME = "music-volume";
	private const string KEY_EFFECTS_VOLUME = "effects-volume";
	private const string KEY_SPEECH_VOLUME = "speech-volume";
		
	public static System.Action<float> MusicVolumeChanged = delegate {};
	public static System.Action<float> EffectsVolumeChanged = delegate {};
	public static System.Action<float> SpeechVolumeChanged = delegate {};

	private static float musicVolume = 0.1f;
	private static float effectsVolume = 0.5f;
	private static float speechVolume = 0.75f;
	private static Vector2 musicVolumeRange = new Vector2(0f, 1f);
	private static Vector2 effectsVolumeRange = new Vector2(0f, 1f);
	private static Vector2 speechVolumeRange = new Vector2(0f, 1f);
		
	private static bool settingsLoaded = false;
		
		
	// Takes and returns 0-1, but converts it to be within a fixed range (musicVolumeRange)
	public static float NormalizedMusicVolume
	{
		get { return (MusicVolume - musicVolumeRange.x) / (musicVolumeRange.y - musicVolumeRange.x); }
		set { MusicVolume = Mathf.Lerp (musicVolumeRange.x, musicVolumeRange.y, value); }
	}
		
		
	public static float MusicVolume
	{
		get
		{
			LoadSettings ();
				
			return musicVolume;
		}
		set
		{
			if (musicVolume != value)
			{
				musicVolume = value;
				PlayerPrefs.SetFloat (KEY_MUSIC_VOLUME, musicVolume);
				PlayerPrefs.Save ();
					
				MusicVolumeChanged (musicVolume);
			}
		}
	}
		
		
	// Takes 0-1, but converts it to be within a fixed range (effectsVolumeRange)
	public static float NormalizedEffectsVolume
	{
		get { return (EffectsVolume - effectsVolumeRange.x) / (effectsVolumeRange.y - effectsVolumeRange.x); }
		set { EffectsVolume = Mathf.Lerp (effectsVolumeRange.x, effectsVolumeRange.y, value); }
	}
		
		
	public static float EffectsVolume
	{
		get
		{
			LoadSettings ();
				
			return (effectsVolume - effectsVolumeRange.x) / (effectsVolumeRange.y - effectsVolumeRange.x);
		}
		set
		{
			if (effectsVolume != value)
			{
				effectsVolume = Mathf.Lerp (effectsVolumeRange.x, effectsVolumeRange.y, value);
				PlayerPrefs.SetFloat (KEY_EFFECTS_VOLUME, effectsVolume);
				PlayerPrefs.Save ();
					
				EffectsVolumeChanged (effectsVolume);
			}
		}
	}
		
		
	// Takes 0-1, but converts it to be within a fixed range (speechVolumeRange)
	public static float NormalizedSpeechVolume
	{
		get { return (SpeechVolume - speechVolumeRange.x) / (speechVolumeRange.y - speechVolumeRange.x); }
		set { SpeechVolume = Mathf.Lerp (speechVolumeRange.x, speechVolumeRange.y, value); }
	}
		
		
	public static float SpeechVolume
	{
		get
		{
			LoadSettings ();
				
			return (speechVolume - speechVolumeRange.x) / (speechVolumeRange.y - speechVolumeRange.x);
		}
		set
		{
			if (speechVolume != value)
			{
				speechVolume = Mathf.Lerp (speechVolumeRange.x, speechVolumeRange.y, value);
				PlayerPrefs.SetFloat (KEY_SPEECH_VOLUME, speechVolume);
				PlayerPrefs.Save ();
					
				SpeechVolumeChanged (speechVolume);
			}
		}
	}
		
		
	private static void LoadSettings ()
	{
		if (!settingsLoaded)
		{
			settingsLoaded = true;
			musicVolume = PlayerPrefs.HasKey (KEY_MUSIC_VOLUME) ? PlayerPrefs.GetFloat (KEY_MUSIC_VOLUME) : musicVolume;
			effectsVolume = PlayerPrefs.HasKey (KEY_EFFECTS_VOLUME) ? PlayerPrefs.GetFloat (KEY_EFFECTS_VOLUME) : effectsVolume;
			speechVolume = PlayerPrefs.HasKey (KEY_SPEECH_VOLUME) ? PlayerPrefs.GetFloat (KEY_SPEECH_VOLUME) : speechVolume;
		}
	}
}
