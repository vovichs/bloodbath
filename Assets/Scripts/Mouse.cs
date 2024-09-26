using UnityEngine;

public class Mouse : MonoBehaviour
{
	public bool Lock;

	private void Update()
	{
		if (Lock)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
