using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static MusicManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
	}

	public void ButtonCrossfade(AudioClip newTrack)
	{
		Crossfade(newTrack);
	}

	public static void Crossfade(AudioClip newTrack)
	{
		Instance.StopAllCoroutines();
		if (Instance.GetComponents<AudioSource>().Length > 1)
		{
			UnityEngine.Object.Destroy(Instance.GetComponent<AudioSource>());
		}
		AudioSource audioSource = Instance.gameObject.AddComponent<AudioSource>();
		audioSource.volume = 0f;
		audioSource.clip = newTrack;
		audioSource.Play();
		Instance.StartCoroutine(Instance.ActuallyCrossfade(audioSource, 1f));
	}

	public static void Crossfade(AudioClip newTrack, float fadeTime)
	{
		Instance.StopAllCoroutines();
		if (Instance.GetComponents<AudioSource>().Length > 1)
		{
			UnityEngine.Object.Destroy(Instance.GetComponent<AudioSource>());
		}
		AudioSource audioSource = Instance.gameObject.AddComponent<AudioSource>();
		audioSource.volume = 0f;
		audioSource.clip = newTrack;
		audioSource.Play();
		Instance.StartCoroutine(Instance.ActuallyCrossfade(audioSource, fadeTime));
	}

	private IEnumerator ActuallyCrossfade(AudioSource newSource, float fadeTime)
	{
		float t = 0f;
		float initialVolume = GetComponent<AudioSource>().volume;
		while (t < fadeTime)
		{
			GetComponent<AudioSource>().volume = Mathf.Lerp(initialVolume, 0f, t / fadeTime);
			newSource.volume = Mathf.Lerp(0f, 1f, t / fadeTime);
			t += Time.deltaTime;
			yield return null;
		}
		newSource.volume = 1f;
		UnityEngine.Object.Destroy(GetComponent<AudioSource>());
	}
}
