using UnityEngine;

public class SelfSavingTimer : MonoBehaviour
{
	private float time;

	private void Start()
	{
		time = PlayerPrefs.GetFloat("Timer", 0f);
	}

	private void Update()
	{
		time += Time.deltaTime;
	}

	private void OnDestroy()
	{
		PlayerPrefs.SetFloat("Timer", time);
		PlayerPrefs.Save();
	}

	private void OnGUI()
	{
		GUILayout.Label(time.ToString());
	}
}
