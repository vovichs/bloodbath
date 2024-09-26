using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowRotationPhysics : MonoBehaviour
{
	public float toVel = 2.5f;

	public float maxVel = 15f;

	public float maxForce = 40f;

	public float gain = 5f;

	public Transform target;

	public bool forceSphericalTensor;

	private void Start()
	{
		if (forceSphericalTensor)
		{
			GetComponent<Rigidbody>().inertiaTensorRotation = Quaternion.identity;
			GetComponent<Rigidbody>().inertiaTensor = Vector3.one;
			GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
		}
	}

	private void FixedUpdate()
	{
		UpdateAngularVelocity(target.rotation);
	}

	private void UpdateAngularVelocity(Quaternion desired)
	{
		Vector3 vector = Vector3.Cross(base.transform.forward, desired * Vector3.forward);
		Vector3 vector2 = Vector3.Cross(base.transform.up, desired * Vector3.up);
		float d = Mathf.Asin(vector.magnitude);
		float d2 = Mathf.Asin(vector2.magnitude);
		Vector3 a = vector.normalized * d;
		Vector3 b = vector2.normalized * d2;
		Vector3 point = Vector3.ClampMagnitude(toVel * (a + b), maxVel);
		Quaternion rotation = base.transform.rotation * GetComponent<Rigidbody>().inertiaTensorRotation;
		Vector3 a2 = rotation * Vector3.Scale(GetComponent<Rigidbody>().inertiaTensor, Quaternion.Inverse(rotation) * point) - GetComponent<Rigidbody>().angularVelocity;
		Vector3 vector3 = Vector3.ClampMagnitude(gain * a2, maxForce);
		if (vector3 != Vector3.zero)
		{
			GetComponent<Rigidbody>().AddTorque(vector3);
		}
	}
}
