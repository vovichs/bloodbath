using UnityEngine;

public class CrossfadeOnButton : MonoBehaviour
{
	public AudioClip[] tracks;

	public string buttonName = "Fire1";

	public float fadeTime = 1f;

	private int currentTrack;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetButtonDown(buttonName))
		{
			currentTrack++;
			if (currentTrack >= tracks.Length)
			{
				currentTrack = 0;
			}
			MusicManager.Crossfade(tracks[currentTrack], fadeTime);
		}
	}
}
