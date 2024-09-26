using UnityEngine;

namespace Generics.Dynamics
{
	public static class DirectionalSwingSolver
	{
		public static void Process(Core.Chain chain, Vector3 lookAtAxis)
		{
			Process(chain, lookAtAxis, chain.GetEndEffector());
		}

		public static void Process(Core.Chain chain, Vector3 lookAtAxis, Transform virtualEndEffector)
		{
			Transform endEffector = virtualEndEffector ?? chain.GetEndEffector();
			for (int i = 0; i < chain.iterations; i++)
			{
				Solve(chain, endEffector, lookAtAxis);
			}
		}

		private static void Solve(Core.Chain chain, Transform endEffector, Vector3 LookAtAxis)
		{
			for (int i = 0; i < chain.joints.Count; i++)
			{
				Vector3 target = GenericMath.TransformVector(LookAtAxis, endEffector.rotation);
				Quaternion b = GenericMath.RotateFromTo(chain.GetIKtarget() - endEffector.position, target);
				Quaternion qA = Quaternion.Lerp(Quaternion.identity, b, chain.weight * chain.joints[i].weight);
				chain.joints[i].joint.rotation = GenericMath.ApplyQuaternion(qA, chain.joints[i].joint.rotation);
				chain.joints[i].ApplyRestrictions();
			}
		}
	}
}
