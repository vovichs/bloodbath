using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class BruteForceMesh : MonoBehaviour
{
	private int triangleCount;

	private Vector3[] vertices;

	private int[] tris;

	private Vector3[] triangleNormals;

	private Mesh mesh;

	private void Awake()
	{
		mesh = GetComponent<MeshCollider>().sharedMesh;
		tris = mesh.triangles;
		vertices = mesh.vertices;
		triangleCount = mesh.triangles.Length / 3;
		triangleNormals = new Vector3[triangleCount];
		for (int i = 0; i < tris.Length; i += 3)
		{
			Vector3 normalized = Vector3.Cross((vertices[tris[i + 1]] - vertices[tris[i]]).normalized, (vertices[tris[i + 2]] - vertices[tris[i]]).normalized).normalized;
			triangleNormals[i / 3] = normalized;
		}
	}

	public Vector3 ClosestPointOn(Vector3 to)
	{
		to = base.transform.InverseTransformPoint(to);
		Vector3 position = ClosestPointOnTriangle(tris, to);
		return base.transform.TransformPoint(position);
	}

	private Vector3 ClosestPointOnTriangle(int[] triangles, Vector3 to)
	{
		float num = float.MaxValue;
		Vector3 result = Vector3.zero;
		for (int i = 0; i < triangles.Length; i += 3)
		{
			int num2 = i;
			Vector3 vertex = vertices[tris[num2]];
			Vector3 vertex2 = vertices[tris[num2 + 1]];
			Vector3 vertex3 = vertices[tris[num2 + 2]];
			ClosestPointOnTriangleToPoint(ref vertex, ref vertex2, ref vertex3, ref to, out Vector3 result2);
			float sqrMagnitude = (to - result2).sqrMagnitude;
			if (sqrMagnitude <= num)
			{
				num = sqrMagnitude;
				result = result2;
			}
		}
		return result;
	}

	private void ClosestPointOnTriangleToPoint(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, ref Vector3 point, out Vector3 result)
	{
		Vector3 vector = vertex2 - vertex1;
		Vector3 vector2 = vertex3 - vertex1;
		Vector3 rhs = point - vertex1;
		float num = Vector3.Dot(vector, rhs);
		float num2 = Vector3.Dot(vector2, rhs);
		if (num <= 0f && num2 <= 0f)
		{
			result = vertex1;
			return;
		}
		Vector3 rhs2 = point - vertex2;
		float num3 = Vector3.Dot(vector, rhs2);
		float num4 = Vector3.Dot(vector2, rhs2);
		if (num3 >= 0f && num4 <= num3)
		{
			result = vertex2;
			return;
		}
		float num5 = num * num4 - num3 * num2;
		if (num5 <= 0f && num >= 0f && num3 <= 0f)
		{
			float d = num / (num - num3);
			result = vertex1 + d * vector;
			return;
		}
		Vector3 rhs3 = point - vertex3;
		float num6 = Vector3.Dot(vector, rhs3);
		float num7 = Vector3.Dot(vector2, rhs3);
		if (num7 >= 0f && num6 <= num7)
		{
			result = vertex3;
			return;
		}
		float num8 = num6 * num2 - num * num7;
		if (num8 <= 0f && num2 >= 0f && num7 <= 0f)
		{
			float d2 = num2 / (num2 - num7);
			result = vertex1 + d2 * vector2;
			return;
		}
		float num9 = num3 * num7 - num6 * num4;
		if (num9 <= 0f && num4 - num3 >= 0f && num6 - num7 >= 0f)
		{
			float d3 = (num4 - num3) / (num4 - num3 + (num6 - num7));
			result = vertex2 + d3 * (vertex3 - vertex2);
			return;
		}
		float num10 = 1f / (num9 + num8 + num5);
		float d4 = num8 * num10;
		float d5 = num5 * num10;
		result = vertex1 + vector * d4 + vector2 * d5;
	}
}
