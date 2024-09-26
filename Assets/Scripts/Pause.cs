using UnityEngine;

public class Pause : MonoBehaviour
{
	public void PauseGame()
	{
		Time.timeScale = 0f;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
	}
}
