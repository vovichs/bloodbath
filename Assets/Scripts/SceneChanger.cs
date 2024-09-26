using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public string sceneName;

	public GameObject dontDestroyOnLoad;

	public void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
		if (dontDestroyOnLoad != null)
		{
			Object.DontDestroyOnLoad(dontDestroyOnLoad);
		}
	}

	private void Awake()
	{
		if ((bool)GameObject.Find(dontDestroyOnLoad.name) && GameObject.Find(dontDestroyOnLoad.name) != dontDestroyOnLoad.gameObject)
		{
			UnityEngine.Object.Destroy(GameObject.Find(dontDestroyOnLoad.name));
		}
	}
}
