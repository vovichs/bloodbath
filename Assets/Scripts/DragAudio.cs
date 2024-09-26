using UnityEngine;

public class DragAudio : MonoBehaviour
{
	public Rigidbody rigidbody;

	public AudioSource audio;

	private void Start()
	{
		if (rigidbody == null)
		{
			rigidbody = GetComponent<Rigidbody>();
		}
	}

	private void Update()
	{
		float volume = rigidbody.velocity.magnitude / 2f;
		audio.volume = volume;
	}
}
