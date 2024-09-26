using UnityEngine;

public class MaintainOrientation : MonoBehaviour
{
	private Quaternion originalRotation;

	private void Start()
	{
		originalRotation = base.transform.rotation;
	}

	private void LateUpdate()
	{
		base.transform.rotation = originalRotation;
	}
}
