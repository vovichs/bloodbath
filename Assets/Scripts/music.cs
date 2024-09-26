using UnityEngine;

public class music : MonoBehaviour
{
	public string PlayerPrefsName;

	private AudioSource AS;

	private void Start()
	{
		AS = base.gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		AS.volume = PlayerPrefs.GetFloat(PlayerPrefsName);
	}
}
