using UnityEngine;

public class SetAspectRatioBasedOnCamera : MonoBehaviour
{
	public Camera target;

	private void Start()
	{
		GetComponent<Camera>().aspect = target.aspect;
	}
}
