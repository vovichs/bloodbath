using UnityEngine;

public class TargetLeadPosition : MonoBehaviour
{
	public Rigidbody target;

	public float projectileVelocity = 1f;

	private void FixedUpdate()
	{
		Vector3 vr = target.velocity - Camera.main.GetComponent<Rigidbody>().velocity;
		Vector3 delta = target.position - Camera.main.transform.position;
		float d = AimAhead(delta, vr, projectileVelocity);
		base.transform.position = target.position + target.velocity * d;
	}

	private float AimAhead(Vector3 delta, Vector3 vr, float muzzleV)
	{
		float num = Vector3.Dot(vr, vr) - muzzleV * muzzleV;
		float num2 = 2f * Vector3.Dot(vr, delta);
		float num3 = Vector3.Dot(delta, delta);
		float num4 = num2 * num2 - 4f * num * num3;
		if (num4 > 0f)
		{
			return 2f * num3 / (Mathf.Sqrt(num4) - num2);
		}
		return -1f;
	}
}
