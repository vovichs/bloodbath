using UnityEngine;
using UnityEngine.Events;

public class PressEvent : MonoBehaviour
{
	public string KeyName;

	public UnityEvent OnKeyPress;

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyName))
		{
			OnKeyPress.Invoke();
		}
	}
}
