using UnityEngine;

public class MoveOnAxisInput : MonoBehaviour
{
	public string horizontalAxis = "Horizontal";

	public string verticalAxis = "Vertical";

	public float speed = 1f;

	private void Update()
	{
		base.transform.position += (Vector3.right * UnityEngine.Input.GetAxis(horizontalAxis) + Vector3.forward * UnityEngine.Input.GetAxis(verticalAxis)).normalized * speed * Time.deltaTime;
	}
}
