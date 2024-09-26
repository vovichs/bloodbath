using UnityEngine;

public class SpawnBasedOnAudio : MonoBehaviour
{
	public GameObject objectPrefab;

	public float spawnThreshold = 0.5f;

	public int frequency;

	public FFTWindow fftWindow;

	private float[] samples = new float[1024];

	private void Start()
	{
	}

	private void Update()
	{
		AudioListener.GetSpectrumData(samples, 0, fftWindow);
		if (samples[frequency] > spawnThreshold)
		{
			Object.Instantiate(objectPrefab, base.transform.position, base.transform.rotation);
		}
	}
}
