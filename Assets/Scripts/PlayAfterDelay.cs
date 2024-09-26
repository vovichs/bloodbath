using UnityEngine;

public class PlayAfterDelay : MonoBehaviour
{
	public float delay = 1f;

	private void Start()
	{
		Invoke("PlayNow", delay);
	}

	private void PlayNow()
	{
		GetComponent<AudioSource>().Play();
	}
}
