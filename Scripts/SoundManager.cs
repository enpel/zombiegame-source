using UnityEngine;
using System.Collections;

public class SoundManager
{
	AudioSource source = null;
	private static SoundManager _instance = null;
	public static SoundManager Instance
	{
		get 
		{
			if (_instance == null)
				_instance = new SoundManager();

			if (_instance.source == null)
			{
				var obj = new GameObject ();
				_instance.source = obj.AddComponent<AudioSource> ();
			}

			return _instance;
		}
	}

	public void PlayOneShot(AudioClip clip)
	{
		Instance.source.PlayOneShot (clip);
	}

	private SoundManager ()
	{
	}
}

public static class SoundManagerExtensions
{
	
	public static void PlayOnShot(this AudioClip clip)
	{
		SoundManager.Instance.PlayOneShot (clip);
	}
}

