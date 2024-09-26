using UnityEngine;

public class Reload : MonoBehaviour
{
	private void Start()
	{
		Invoke("ReloadNow", 5f);
	}

	private void ReloadNow()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}
