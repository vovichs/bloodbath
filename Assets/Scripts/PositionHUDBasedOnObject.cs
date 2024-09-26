using UnityEngine;

public class PositionHUDBasedOnObject : MonoBehaviour
{
	public Transform target;

	private void Update()
	{
		base.transform.position = Camera.main.WorldToScreenPoint(target.position);
	}
}
