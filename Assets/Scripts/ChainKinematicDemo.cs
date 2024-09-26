using Generics.Dynamics;
using UnityEngine;

public class ChainKinematicDemo : MonoBehaviour
{
	public Core.KinematicChain chain;

	private void LateUpdate()
	{
		ChainKinematicSolver.Process(chain);
	}
}
