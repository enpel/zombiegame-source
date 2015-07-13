using UnityEngine;
using System.Collections;

public static class AudioSourceExtensions
{
	public static void PlayOnShotCrrentClip(this AudioSource source)
	{
		source.PlayOneShot (source.clip);

	}

}
