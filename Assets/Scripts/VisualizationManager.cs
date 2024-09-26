using UnityEngine;

public class VisualizationManager : MonoBehaviour
{
	public FFTWindow fftWindow;

	public static float[] samples = new float[1024];

	private void Update()
	{
		AudioListener.GetSpectrumData(samples, 0, fftWindow);
	}
}
