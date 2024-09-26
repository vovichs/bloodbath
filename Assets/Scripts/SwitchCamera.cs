using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
	public Camera newCamera;

	public string escapeKey = "Fire1";

	private bool activeNow;

	private void Start()
	{
	}

	private void Update()
	{
		if (activeNow && Input.GetButtonDown(escapeKey))
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
			newCamera.enabled = false;
			activeNow = false;
		}
	}

	private void InteractEvent()
	{
		if (!activeNow)
		{
			activeNow = true;
			newCamera.enabled = true;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
		}
	}
}
