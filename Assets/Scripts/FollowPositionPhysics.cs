using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowPositionPhysics : MonoBehaviour
{
	public float toVel = 2.5f;

	public float maxVel = 15f;

	public float maxForce = 40f;

	public float gain = 5f;

	public Transform target;

	private void FixedUpdate()
	{
		Vector3 a = target.position - base.transform.position;
		Vector3 a2 = Vector3.ClampMagnitude(toVel * a, maxVel) - GetComponent<Rigidbody>().velocity;
		Vector3 force = Vector3.ClampMagnitude(gain * a2, maxForce);
		GetComponent<Rigidbody>().AddForce(force);
	}
}
