using UnityEngine;

public class MoveToRaycastPositionFunction : MonoBehaviour
{
	public void MoveToPosition(RaycastHit hit)
	{
		base.transform.position = hit.point;
	}
}
