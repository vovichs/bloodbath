using UnityEngine;
using UnityEngine.UI;

public class MusicMaster : MonoBehaviour
{
	public float defaultVolume;

	private float Volume;

	public Slider slider;

	public string PlayerPrefsName;

	public void OnEnable()
	{
		if (PlayerPrefs.HasKey(PlayerPrefsName))
		{
			slider.value = PlayerPrefs.GetFloat(PlayerPrefsName);
			return;
		}
		Volume = defaultVolume;
		VolumeUpdate();
	}

	public void VolumeUpdate()
	{
		Volume = slider.value;
		PlayerPrefs.SetFloat(PlayerPrefsName, Volume);
	}
}
