using UnityEngine;

public class DrawPositionVectorOnDrawGizmos : MonoBehaviour
{
	public Color color = Color.green;

	public float arrowHeadLength = 0.25f;

	public bool showLocalPosition = true;

	private void OnDrawGizmos()
	{
		Gizmos.color = color;
		if (showLocalPosition && base.transform.parent != null)
		{
			Gizmos.DrawLine(base.transform.parent.position, base.transform.position);
			if (Camera.current != null)
			{
				Vector3 normalized = base.transform.localPosition.normalized;
				Vector3 b = Vector3.Cross(normalized, Camera.current.transform.forward).normalized * arrowHeadLength - normalized * arrowHeadLength;
				Gizmos.DrawLine(base.transform.position, base.transform.position + b);
				Vector3 b2 = Vector3.Cross(Camera.current.transform.forward, normalized).normalized * arrowHeadLength - normalized * arrowHeadLength;
				Gizmos.DrawLine(base.transform.position, base.transform.position + b2);
			}
		}
		else
		{
			Gizmos.DrawLine(Vector3.zero, base.transform.position);
			if (Camera.current != null)
			{
				Vector3 normalized2 = base.transform.position.normalized;
				Vector3 b3 = Vector3.Cross(normalized2, Camera.current.transform.forward).normalized * arrowHeadLength - normalized2 * arrowHeadLength;
				Gizmos.DrawLine(base.transform.position, base.transform.position + b3);
				Vector3 b4 = Vector3.Cross(Camera.current.transform.forward, normalized2).normalized * arrowHeadLength - normalized2 * arrowHeadLength;
				Gizmos.DrawLine(base.transform.position, base.transform.position + b4);
			}
		}
	}
}
