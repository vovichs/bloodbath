using UnityEngine;

namespace Generics.Dynamics
{
	public static class CyclicDescendSolver
	{
		public static bool Process(Core.Chain chain)
		{
			if (chain.joints.Count <= 0)
			{
				return false;
			}
			chain.MapVirtualJoints();
			for (int i = 0; i < chain.iterations; i++)
			{
				for (int num = chain.joints.Count - 1; num >= 0; num--)
				{
					float t = chain.weight * chain.joints[num].weight;
					Vector3 source = chain.GetIKtarget() - chain.joints[num].joint.position;
					Vector3 target = chain.joints[chain.joints.Count - 1].joint.position - chain.joints[num].joint.position;
					Quaternion rotation = chain.joints[num].joint.rotation;
					Quaternion qA = Quaternion.Lerp(Quaternion.identity, GenericMath.RotateFromTo(source, target), t);
					chain.joints[num].rot = Quaternion.Lerp(rotation, GenericMath.ApplyQuaternion(qA, rotation), t);
					chain.joints[num].ApplyVirtualMap(applyPos: false, applyRot: true);
					chain.joints[num].ApplyRestrictions();
				}
			}
			return true;
		}
	}
}
