using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	public float distance = 1f;

	public bool useInitalCameraDistance;

	private float actualDistance;

	private void Start()
	{
		if (useInitalCameraDistance)
		{
			actualDistance = Vector3.Project(base.transform.position - Camera.main.transform.position, Camera.main.transform.forward).magnitude;
		}
		else
		{
			actualDistance = distance;
		}
	}

	private void Update()
	{
		Vector3 mousePosition = UnityEngine.Input.mousePosition;
		mousePosition.z = actualDistance;
		base.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
	}
}
