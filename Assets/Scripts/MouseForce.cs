using UnityEngine;

public class MouseForce : MonoBehaviour
{
	public float impulseScale = 25f;

	private Rigidbody grabBody;

	private Vector3 grabPoint;

	public float grabDistance;

	public Character chararcter;

	public Camera cam;

	public float distance;

	public void Update()
	{
		GrabBody();
		ReleaseBody();
	}

	public void FixedUpdate()
	{
		MoveBody();
	}

	private void GrabBody()
	{
		if (grabBody == null && Input.GetMouseButtonDown(0))
		{
			SetKinematic(newValue: true);
			if (Physics.Raycast(cam.ScreenPointToRay(UnityEngine.Input.mousePosition), out RaycastHit hitInfo, distance) && hitInfo.rigidbody != null)
			{
				grabBody = hitInfo.rigidbody;
				grabPoint = grabBody.transform.InverseTransformPoint(hitInfo.point);
				grabDistance = hitInfo.distance;
			}
		}
	}

	private void ReleaseBody()
	{
		if (grabBody != null && Input.GetMouseButtonUp(0))
		{
			grabBody = null;
		}
	}

	public void SetKinematic(bool newValue)
	{
		Rigidbody[] componentsInChildren = GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = newValue;
		}
	}

	private void MoveBody()
	{
		if (grabBody != null)
		{
			Vector3 position = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, grabDistance);
			Vector3 vector = cam.ScreenToWorldPoint(position);
			Vector3 vector2 = grabBody.transform.TransformPoint(grabPoint);
			UnityEngine.Debug.DrawLine(vector, vector2, Color.red);
			Vector3 force = (vector - vector2) * (impulseScale * Time.fixedDeltaTime);
			grabBody.AddForceAtPosition(force, vector2, ForceMode.Impulse);
		}
	}
}
