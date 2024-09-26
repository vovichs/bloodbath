using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEvent : MonoBehaviour
{
	private void PlaySound()
	{
		GetComponent<AudioSource>().Play();
	}
}
