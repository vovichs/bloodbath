using UnityEngine;

public class MoveToClickLocation : MonoBehaviour
{
	public float movementSpeed = 1f;

	public float rotationSpeed = 30f;

	public LayerMask movementLayer = -1;

	private Vector3 targetPosition;

	private Quaternion targetRotation;

	private bool atTarget = true;

	private bool facingTarget;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out RaycastHit hitInfo, float.PositiveInfinity, movementLayer))
		{
			UnityEngine.Debug.DrawLine(base.transform.position, targetPosition, Color.white, 1f);
			targetPosition = hitInfo.point;
			targetRotation = Quaternion.LookRotation(targetPosition - base.transform.position);
			StopAllCoroutines();
			atTarget = false;
		}
		Vector3 position = base.transform.position;
		Vector3 forward = targetPosition - position;
		base.transform.position = Vector3.MoveTowards(base.transform.position, targetPosition, movementSpeed * Time.deltaTime);
		if (forward.sqrMagnitude != 0f)
		{
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.LookRotation(forward), rotationSpeed * Time.deltaTime);
		}
	}
}
