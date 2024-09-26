using UnityEngine;

public class RandomSoundClip : MonoBehaviour
{
	public AudioClip[] soundClips;

	public bool playOnAwake = true;

	private void Awake()
	{
		GetComponent<AudioSource>().clip = soundClips[Random.Range(0, soundClips.Length)];
		if (playOnAwake)
		{
			GetComponent<AudioSource>().Play();
		}
	}

	private void Update()
	{
	}
}
