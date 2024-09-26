using System;
using UnityEngine;

namespace Generics.Dynamics
{
	public static class GenericMath
	{
		public static Quaternion ApplyQuaternion(Quaternion _qA, Quaternion _qB)
		{
			Quaternion identity = Quaternion.identity;
			Vector3 vector = new Vector3(_qA.x, _qA.y, _qA.z);
			Vector3 vector2 = new Vector3(_qB.x, _qB.y, _qB.z);
			identity.w = _qA.w * _qB.w - Vector3.Dot(vector, vector2);
			Vector3 vector3 = Vector3.Cross(vector, vector2) + _qA.w * vector2 + _qB.w * vector;
			identity.x = vector3.x;
			identity.y = vector3.y;
			identity.z = vector3.z;
			return identity;
		}

		public static Quaternion QuaternionFromAngleAxis(float _angle, Vector3 _axis)
		{
			Quaternion identity = Quaternion.identity;
			_axis.Normalize();
			_angle *= (float)Math.PI / 180f;
			identity.x = _axis.x * Mathf.Sin(_angle / 2f);
			identity.y = _axis.y * Mathf.Sin(_angle / 2f);
			identity.z = _axis.z * Mathf.Sin(_angle / 2f);
			identity.w = Mathf.Cos(_angle / 2f);
			return identity;
		}

		public static Quaternion QuaternionToAngleAxis(Quaternion quaternion, out float _angle, out Vector3 _axis)
		{
			_angle = 0f;
			_axis = Vector3.zero;
			_angle = 2f * Mathf.Acos(quaternion.w) * 57.29578f;
			_axis.x = quaternion.x / Mathf.Sqrt(1f - Mathf.Pow(quaternion.w, 2f));
			_axis.y = quaternion.y / Mathf.Sqrt(1f - Mathf.Pow(quaternion.w, 2f));
			_axis.z = quaternion.z / Mathf.Sqrt(1f - Mathf.Pow(quaternion.w, 2f));
			return quaternion;
		}

		public static Vector3 Interpolate(Vector3 _from, Vector3 _to, float _weight)
		{
			_weight = Mathf.Clamp(_weight, 0f, 1f);
			return new Vector3((1f - _weight) * _from.x + _weight * _to.x, (1f - _weight) * _from.y + _weight * _to.y, (1f - _weight) * _from.z + _weight * _to.z);
		}

		public static float VectorsAngle(Vector3 _v0, Vector3 _v1)
		{
			_v0.Normalize();
			_v1.Normalize();
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(_v0, _v1), -1f, 1f)) * 57.29578f;
		}

		public static float QuaternionAngle(Quaternion _q1, Quaternion _q2)
		{
			float value = Quaternion.Dot(_q1, _q2);
			return 2f * Mathf.Acos(Mathf.Clamp01(value)) * 57.29578f;
		}

		public static Quaternion RotateFromTo(Vector3 _source, Vector3 _target)
		{
			_source.Normalize();
			_target.Normalize();
			return Quaternion.Inverse(QuaternionFromAngleAxis(VectorsAngle(_source, _target), Vector3.Cross(_source, _target).normalized));
		}

		public static Vector3 TransformVector(Vector3 _v, Quaternion _q)
		{
			Quaternion qB = new Quaternion(_v.x, _v.y, _v.z, 0f);
			Quaternion qA = ApplyQuaternion(_q, qB);
			qA = ApplyQuaternion(qA, Quaternion.Inverse(_q));
			return new Vector3(qA.x, qA.y, qA.z);
		}

		public static Quaternion RotationLookAt(Vector3 _normal)
		{
			Quaternion identity = Quaternion.identity;
			return Quaternion.LookRotation(-_normal);
		}

		public static Vector3 GetLocalAxisToTarget(Transform self, Vector3 target)
		{
			Quaternion q = Quaternion.Inverse(self.rotation);
			return TransformVector((target - self.position).normalized, q);
		}

		public static bool ConeBounded(Core.Joint joint, Vector3 obj)
		{
			float num = VectorsAngle(obj - joint.pos, joint.pos + TransformVector(joint.axis, joint.rot));
			return joint.maxAngle >= num;
		}

		public static Vector3 GetConeNextPoint(Core.Joint joint, Vector3 obj)
		{
			if (ConeBounded(joint, obj))
			{
				return obj;
			}
			Vector3 pos = joint.pos;
			Vector3 v = obj - pos;
			Vector3 vector = TransformVector(joint.axis, joint.rot);
			float num = VectorsAngle(v, pos + vector);
			float num2 = Mathf.Cos(num * ((float)Math.PI / 180f)) * v.magnitude;
			float d = num2 * (Mathf.Tan(num * ((float)Math.PI / 180f)) - Mathf.Tan(joint.maxAngle * ((float)Math.PI / 180f)));
			Vector3 vector2 = joint.joint.position + TransformVector(vector * num2, joint.rot) - obj;
			float f = Vector3.Dot(joint.joint.position + TransformVector(vector, joint.rot), v.normalized);
			return (vector2.normalized * d + obj) * Mathf.Clamp01(Mathf.Sign(f)) + pos * Mathf.Clamp01(0f - Mathf.Sign(f));
		}

		public static float Clamp(float value, float min, float max)
		{
			if (value.CompareTo(min) < 0)
			{
				return min;
			}
			if (value.CompareTo(max) > 0)
			{
				return max;
			}
			return value;
		}
	}
}
