using Generics.Dynamics;
using UnityEngine;

public class SwingAtDemo : MonoBehaviour
{
	public Transform virtualEndEffector;

	public Vector3 offsetInAnim;

	public Vector3 lookAtAxis = Vector3.forward;

	public Core.Chain chain;

	private void LateUpdate()
	{
		Vector3 b = offsetInAnim - virtualEndEffector.position;
		Quaternion rotation = GenericMath.RotateFromTo(base.transform.position + b, lookAtAxis);
		virtualEndEffector.rotation = rotation;
		DirectionalSwingSolver.Process(chain, lookAtAxis, virtualEndEffector);
	}
}
