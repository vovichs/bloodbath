using UnityEngine;

public class KinematicLinearGravity : MonoBehaviour
{
	public LayerMask layerMask = -1;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		Vector3 vector = Physics.gravity * Time.deltaTime;
		if (!Physics.Raycast(base.transform.position, Physics.gravity, out RaycastHit _, vector.magnitude, layerMask))
		{
			base.transform.position += vector;
		}
	}
}
