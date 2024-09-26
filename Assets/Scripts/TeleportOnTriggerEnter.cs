using UnityEngine;

public class TeleportOnTriggerEnter : MonoBehaviour
{
	public Transform target;

	private void OnTriggerEnter(Collider col)
	{
		Vector3 b = col.transform.position - base.transform.position;
		col.transform.position = target.position + b;
	}
}
