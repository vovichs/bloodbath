using UnityEngine;

namespace Generics.Dynamics
{
	public static class FastReachSolver
	{
		public static bool Process(Core.Chain chain)
		{
			if (chain.joints.Count <= 0)
			{
				return false;
			}
			if (!chain.initiated)
			{
				chain.InitiateJoints();
			}
			chain.MapVirtualJoints();
			for (int i = 0; i < chain.iterations; i++)
			{
				SolveInward(chain);
				SolveOutward(chain);
			}
			MapSolverOutput(chain);
			return true;
		}

		public static void SolveInward(Core.Chain chain)
		{
			int count = chain.joints.Count;
			chain.joints[count - 1].pos = Vector3.Lerp(chain.GetVirtualEE(), chain.GetIKtarget(), chain.weight);
			for (int num = count - 2; num >= 0; num--)
			{
				Vector3 pos = chain.joints[num + 1].pos;
				Vector3 a = chain.joints[num].pos - pos;
				a.Normalize();
				a *= Vector3.Distance(chain.joints[num + 1].joint.position, chain.joints[num].joint.position);
				chain.joints[num].pos = pos + a;
			}
		}

		public static void SolveOutward(Core.Chain chain)
		{
			chain.joints[0].pos = chain.joints[0].joint.position;
			for (int i = 1; i < chain.joints.Count; i++)
			{
				Vector3 pos = chain.joints[i - 1].pos;
				Vector3 a = chain.joints[i].pos - pos;
				a.Normalize();
				a *= Vector3.Distance(chain.joints[i - 1].joint.position, chain.joints[i].joint.position);
				chain.joints[i].pos = pos + a;
			}
		}

		public static void MapSolverOutput(Core.Chain chain)
		{
			for (int i = 0; i < chain.joints.Count - 1; i++)
			{
				Vector3 source = chain.joints[i + 1].pos - chain.joints[i].pos;
				Vector3 target = GenericMath.TransformVector(chain.joints[i].localAxis, chain.joints[i].rot);
				Quaternion qA = GenericMath.RotateFromTo(source, target);
				chain.joints[i].rot = GenericMath.ApplyQuaternion(qA, chain.joints[i].rot);
				chain.joints[i].ApplyVirtualMap(applyPos: true, applyRot: true);
			}
		}
	}
}
