using UnityEngine;

public class HingeJointTarget : MonoBehaviour
{
	public HingeJoint hj;

	public Transform target;

	[Tooltip("Only use one of these values at a time. Toggle invert if the rotation is backwards.")]
	public bool x;

	[Tooltip("Only use one of these values at a time. Toggle invert if the rotation is backwards.")]
	public bool y;

	[Tooltip("Only use one of these values at a time. Toggle invert if the rotation is backwards.")]
	public bool z;

	[Tooltip("Only use one of these values at a time. Toggle invert if the rotation is backwards.")]
	public bool invert;

	private void Start()
	{
	}

	private void Update()
	{
		if (!(hj != null))
		{
			return;
		}
		if (x)
		{
			JointSpring spring = hj.spring;
			spring.targetPosition = target.transform.localEulerAngles.x;
			if (spring.targetPosition > 180f)
			{
				spring.targetPosition -= 360f;
			}
			if (invert)
			{
				spring.targetPosition *= -1f;
			}
			spring.targetPosition = Mathf.Clamp(spring.targetPosition, hj.limits.min + 5f, hj.limits.max - 5f);
			hj.spring = spring;
		}
		else if (y)
		{
			JointSpring spring2 = hj.spring;
			spring2.targetPosition = target.transform.localEulerAngles.y;
			if (spring2.targetPosition > 180f)
			{
				spring2.targetPosition -= 360f;
			}
			if (invert)
			{
				spring2.targetPosition *= -1f;
			}
			spring2.targetPosition = Mathf.Clamp(spring2.targetPosition, hj.limits.min + 5f, hj.limits.max - 5f);
			hj.spring = spring2;
		}
		else if (z)
		{
			JointSpring spring3 = hj.spring;
			spring3.targetPosition = target.transform.localEulerAngles.z;
			if (spring3.targetPosition > 180f)
			{
				spring3.targetPosition -= 360f;
			}
			if (invert)
			{
				spring3.targetPosition *= -1f;
			}
			spring3.targetPosition = Mathf.Clamp(spring3.targetPosition, hj.limits.min + 5f, hj.limits.max - 5f);
			hj.spring = spring3;
		}
	}
}
