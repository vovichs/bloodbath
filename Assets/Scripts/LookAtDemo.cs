using Generics.Dynamics;
using UnityEngine;

public class LookAtDemo : MonoBehaviour
{
	public Vector3 lookAtAxis = Vector3.forward;

	public Core.Chain chain;

	private void LateUpdate()
	{
		DirectionalSwingSolver.Process(chain, lookAtAxis);
	}
}
