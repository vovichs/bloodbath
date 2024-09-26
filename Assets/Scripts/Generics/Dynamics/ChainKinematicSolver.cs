using System;
using UnityEngine;

namespace Generics.Dynamics
{
	public static class ChainKinematicSolver
	{
		public static void Process(Core.KinematicChain chain)
		{
			if (!chain.initiated)
			{
				chain.InitiateJoints();
			}
			chain.MapVirtualJoints();
			FollowParent(chain);
			MapSolverOutput(chain);
		}

		private static void FollowParent(Core.KinematicChain chain)
		{
			for (int i = 1; i < chain.joints.Count; i++)
			{
				Vector3 a = chain.joints[i].pos - chain.prevPos[i];
				float d = 6f * Mathf.Pow(chain.joints.Count * i, 2f) - 4f * (float)chain.joints.Count * Mathf.Pow(i, 3f) + Mathf.Pow(i, 4f);
				Vector3 vector = chain.gravity * d / (chain.momentOfInteria * (float)chain.joints.Count * 12f);
				float time = (float)i / ((float)chain.joints.Count - 1f);
				float num = chain.solverFallOff.Evaluate(time) * chain.weight;
				float d2 = (chain.torsionDamping != 0f) ? (Mathf.Sin(num * ((float)Math.PI / 2f)) * Mathf.Exp((0f - chain.torsionDamping) * num)) : 1f;
				chain.prevPos[i] = chain.joints[i].pos;
				chain.joints[i].pos += a * d2;
				chain.joints[i].pos += vector;
				chain.prevPos[0] = chain.joints[0].pos;
				chain.joints[0].pos = chain.joints[0].joint.position;
				float length = chain.joints[i - 1].length;
				Vector3 a2 = chain.joints[i].joint.position + (chain.joints[i - 1].pos - chain.joints[i - 1].joint.position) - chain.joints[i].pos;
				chain.joints[i].pos += a2 * (1f - chain.weight);
				Vector3 a3 = a2 * (2f - chain.weight);
				float magnitude = a3.magnitude;
				float num2 = length * (0f - chain.stiffness) * num;
				Vector3 vector2 = a3 * (Mathf.Max(magnitude - num2, 0f) / Mathf.Max(1f, magnitude));
				chain.joints[i].pos += vector2;
				Vector3 a4 = chain.joints[i - 1].pos - chain.joints[i].pos;
				float magnitude2 = a4.magnitude;
				chain.joints[i].pos += a4 * ((magnitude2 - length) / Mathf.Max(magnitude2, 1f));
				chain.joints[i].pos = Vector3.Lerp(chain.joints[i].joint.position, chain.joints[i].pos, chain.joints[i].weight);
			}
		}

		private static void MapSolverOutput(Core.KinematicChain chain)
		{
			for (int i = 1; i < chain.joints.Count; i++)
			{
				Vector3 target = GenericMath.TransformVector(chain.joints[i - 1].localAxis, chain.joints[i - 1].rot);
				Quaternion qA = GenericMath.RotateFromTo(chain.joints[i].pos - chain.joints[i - 1].pos, target);
				chain.joints[i - 1].rot = GenericMath.ApplyQuaternion(qA, chain.joints[i - 1].rot);
				chain.joints[i - 1].ApplyVirtualMap(applyPos: false, applyRot: true);
				chain.joints[i].ApplyVirtualMap(applyPos: true, applyRot: false);
				chain.joints[i - 1].ApplyRestrictions();
				chain.joints[i].ApplyRestrictions();
			}
		}
	}
}
