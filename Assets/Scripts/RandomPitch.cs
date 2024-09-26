using UnityEngine;

public class RandomPitch : MonoBehaviour
{
	public float pitchOffset = 0.2f;

	private void Awake()
	{
		GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(GetComponent<AudioSource>().pitch - pitchOffset, GetComponent<AudioSource>().pitch + pitchOffset);
	}

	private void Update()
	{
	}
}
