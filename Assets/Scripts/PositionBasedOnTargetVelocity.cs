using UnityEngine;

public class PositionBasedOnTargetVelocity : MonoBehaviour
{
	public Rigidbody target;

	public float time = 1f;

	private void FixedUpdate()
	{
		base.transform.position = target.position + target.velocity * time;
	}
}
