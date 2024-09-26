using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonToNextScene : MonoBehaviour
{
	public string sceneName;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(LoadScene);
	}

	private void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}
