using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generics.Dynamics
{
	public class Core
	{
		public enum Solvers
		{
			CyclicDescend,
			FastReach
		}

		[Serializable]
		public class Joint
		{
			public enum MotionLimit
			{
				Full,
				FullRestricted,
				SingleDegree
			}

			public MotionLimit motionFreedom;

			public Transform joint;

			[Range(0f, 1f)]
			public float weight = 1f;

			public float length;

			public Vector3 pos;

			public Quaternion rot;

			public Vector3 localAxis = Vector3.up;

			public Vector3 axis = Vector3.right;

			public Vector2 hingLimit = Vector2.one * 180f;

			public float maxAngle = 180f;

			public float maxTwist = 180f;

			public void MapVirtual()
			{
				pos = joint.position;
				rot = joint.rotation;
			}

			public void ApplyVirtualMap(bool applyPos, bool applyRot)
			{
				if (applyPos)
				{
					joint.position = pos;
				}
				if (applyRot)
				{
					joint.rotation = rot;
				}
			}

			public Quaternion ApplyRestrictions()
			{
				switch (motionFreedom)
				{
				case MotionLimit.Full:
					return joint.localRotation;
				case MotionLimit.FullRestricted:
					return TwistAndSwing();
				case MotionLimit.SingleDegree:
					return SingleDegree();
				default:
					return joint.localRotation;
				}
			}

			private Quaternion TwistAndSwing()
			{
				Func<Quaternion, float, Quaternion> obj = delegate(Quaternion q, float x)
				{
					if (x == 0f)
					{
						return Quaternion.identity;
					}
					float num2 = GenericMath.QuaternionAngle(Quaternion.identity, q);
					float t = Mathf.Clamp01(x / num2);
					return Quaternion.Slerp(Quaternion.identity, q, t);
				};
				Func<float, float> func = (float x) => x * x;
				Quaternion quaternion = GenericMath.RotateFromTo(GenericMath.TransformVector(axis, joint.localRotation), axis);
				Quaternion qB = obj(quaternion, maxAngle);
				Quaternion quaternion2 = GenericMath.ApplyQuaternion(Quaternion.Inverse(quaternion), joint.localRotation);
				float num = Mathf.Sqrt(func(quaternion2.w) + func(quaternion2.x) + func(quaternion2.y) + func(quaternion2.z));
				float w = quaternion2.w / num;
				float x2 = quaternion2.x / num;
				float y = quaternion2.y / num;
				float z = quaternion2.z / num;
				Quaternion qA = obj(new Quaternion(x2, y, z, w), maxTwist);
				joint.localRotation = GenericMath.ApplyQuaternion(qA, qB);
				return joint.localRotation;
			}

			private Quaternion SingleDegree()
			{
				Vector3 target = GenericMath.TransformVector(axis, joint.transform.localRotation);
				GenericMath.QuaternionToAngleAxis(GenericMath.ApplyQuaternion(GenericMath.RotateFromTo(axis, target), joint.localRotation), out float _angle, out Vector3 _axis);
				float x = hingLimit.x;
				float y = hingLimit.y;
				float num = Vector3.Dot(axis, _axis);
				_angle = GenericMath.Clamp(_angle * num, x, y);
				joint.localRotation = GenericMath.QuaternionFromAngleAxis(_angle, axis);
				return joint.localRotation;
			}
		}

		[Serializable]
		public class Chain
		{
			public Transform target;

			[Range(0f, 1f)]
			public float weight;

			public int iterations;

			public List<Joint> joints = new List<Joint>();

			private Vector3 IKpos;

			public bool initiated
			{
				get;
				private set;
			}

			public float chainLength
			{
				get;
				private set;
			}

			public Chain()
			{
				iterations = 2;
			}

			public void InitiateJoints()
			{
				MapVirtualJoints();
				for (int i = 0; i < joints.Count - 1; i++)
				{
					joints[i].localAxis = GenericMath.GetLocalAxisToTarget(joints[i].joint, joints[i + 1].joint.position);
					joints[i].length = Vector3.Distance(joints[i].joint.position, joints[i + 1].joint.position);
					chainLength += joints[i].length;
				}
				joints[joints.Count - 1].localAxis = GenericMath.GetLocalAxisToTarget(joints[0].joint, joints[joints.Count - 1].joint.position);
				SetIKTarget(GetVirtualEE());
				initiated = true;
			}

			public Transform GetEndEffector()
			{
				if (joints.Count <= 0)
				{
					return null;
				}
				return joints[joints.Count - 1].joint;
			}

			public Vector3 GetVirtualEE()
			{
				if (joints.Count <= 0)
				{
					return Vector3.zero;
				}
				return joints[joints.Count - 1].pos;
			}

			public Vector3 GetIKtarget()
			{
				if (!target)
				{
					return IKpos;
				}
				return target.position;
			}

			public Vector3 SetIKTarget(Vector3 target)
			{
				IKpos = (this.target ? this.target.position : target);
				return IKpos;
			}

			public Quaternion SetEERotation(Quaternion target)
			{
				return joints[joints.Count - 1].joint.rotation = target;
			}

			public void MapVirtualJoints()
			{
				for (int i = 0; i < joints.Count; i++)
				{
					joints[i].MapVirtual();
				}
			}
		}

		[Serializable]
		public class KinematicChain
		{
			[Header("Chain")]
			[Range(0f, 1f)]
			public float weight = 1f;

			public List<Joint> joints = new List<Joint>();

			[Header("Interafce")]
			public AnimationCurve solverFallOff;

			public Vector3 gravity = Vector3.down;

			public float momentOfInteria = 1000f;

			public float stiffness = -0.1f;

			public float torsionDamping;

			public bool initiated
			{
				get;
				private set;
			}

			public Quaternion[] initLocalRot
			{
				get;
				private set;
			}

			public Vector3[] prevPos
			{
				get;
				private set;
			}

			public KinematicChain()
			{
				momentOfInteria = 1000f;
				stiffness = -0.1f;
			}

			public void InitiateJoints()
			{
				initLocalRot = new Quaternion[joints.Count];
				prevPos = new Vector3[joints.Count];
				int num = 0;
				for (num = 0; num < joints.Count - 1; num++)
				{
					joints[num].MapVirtual();
					joints[num].localAxis = GenericMath.GetLocalAxisToTarget(joints[num].joint, joints[num + 1].joint.position);
					joints[num].length = Vector3.Distance(joints[num].joint.position, joints[num + 1].joint.position);
					initLocalRot[num] = joints[num].joint.localRotation;
					prevPos[num] = joints[num].joint.position;
				}
				joints[num].MapVirtual();
				initLocalRot[num] = joints[num].joint.localRotation;
				prevPos[num] = joints[num].joint.position;
				joints[num].localAxis = GenericMath.GetLocalAxisToTarget(joints[0].joint, joints[num].joint.position);
				initiated = true;
			}

			public void MapVirtualJoints()
			{
				for (int i = 0; i < joints.Count; i++)
				{
					joints[i].joint.localRotation = initLocalRot[i];
				}
			}
		}
	}
}
