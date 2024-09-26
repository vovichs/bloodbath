using UnityEngine;

public class ChangeScaleBasedOnAudioVolume : MonoBehaviour
{
	public float scaleBoost = 1f;

	private float[] samples = new float[1];

	private void Update()
	{
		AudioListener.GetOutputData(samples, 0);
		float num = 0f;
		float[] array = samples;
		foreach (float num2 in array)
		{
			num += num2;
		}
		num /= (float)samples.Length;
		base.transform.localScale = Vector3.one * num * scaleBoost;
	}
}
