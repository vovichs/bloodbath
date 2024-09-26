using UnityEngine;

public class ChangePitchToTimeScale : MonoBehaviour
{
	private void Update()
	{
		GetComponent<AudioSource>().pitch = Time.timeScale;
	}
}
