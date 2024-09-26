using UnityEngine;
using UnityEngine.Audio;

public class AdjustAudioMixerParameter : MonoBehaviour
{
	public AudioMixer mixer;

	public float value;

	public string parameterName;

	private void Start()
	{
	}

	public void SetAudioParameter(float newValue)
	{
		value = newValue;
	}

	private void Update()
	{
		mixer.SetFloat(parameterName, value);
	}
}
