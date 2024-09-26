using UnityEngine;

public class ToggleGameObjectsOnInteract : MonoBehaviour
{
	public GameObject[] gameObjectsToToggle;

	private bool activeNow;

	private void Start()
	{
	}

	private void InteractEvent()
	{
		activeNow = !activeNow;
		GameObject[] array = gameObjectsToToggle;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(activeNow);
		}
	}
}
