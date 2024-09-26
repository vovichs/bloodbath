using UnityEngine;

public class MoveTriggeredObjectForward : MonoBehaviour
{
	public float speed = 1f;

	private void OnTriggerStay(Collider col)
	{
		col.transform.position += base.transform.forward * speed * Time.deltaTime;
	}
}
