using UnityEngine;

public class ExtendedFlycam : MonoBehaviour
{
	public float cameraSensitivity = 90f;

	public float climbSpeed = 4f;

	public float normalMoveSpeed = 10f;

	public float slowMoveFactor = 0.25f;

	public float fastMoveFactor = 3f;

	private float rotationX;

	private float rotationY;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void FixedUpdate()
	{
		rotationX += UnityEngine.Input.GetAxis("Mouse X") * cameraSensitivity * Time.unscaledDeltaTime;
		rotationY += UnityEngine.Input.GetAxis("Mouse Y") * cameraSensitivity * Time.unscaledDeltaTime;
		rotationY = Mathf.Clamp(rotationY, -90f, 90f);
		base.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		base.transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
		{
			base.transform.position += base.transform.forward * (normalMoveSpeed * fastMoveFactor) * UnityEngine.Input.GetAxis("Vertical") * Time.unscaledDeltaTime;
			base.transform.position += base.transform.right * (normalMoveSpeed * fastMoveFactor) * UnityEngine.Input.GetAxis("Horizontal") * Time.unscaledDeltaTime;
		}
		else if (UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl))
		{
			base.transform.position += base.transform.forward * (normalMoveSpeed * slowMoveFactor) * UnityEngine.Input.GetAxis("Vertical") * Time.unscaledDeltaTime;
			base.transform.position += base.transform.right * (normalMoveSpeed * slowMoveFactor) * UnityEngine.Input.GetAxis("Horizontal") * Time.unscaledDeltaTime;
		}
		else
		{
			base.transform.position += base.transform.forward * normalMoveSpeed * UnityEngine.Input.GetAxis("Vertical") * Time.unscaledDeltaTime;
			base.transform.position += base.transform.right * normalMoveSpeed * UnityEngine.Input.GetAxis("Horizontal") * Time.unscaledDeltaTime;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Q))
		{
			base.transform.position += base.transform.up * climbSpeed * Time.unscaledDeltaTime;
		}
		if (UnityEngine.Input.GetKey(KeyCode.E))
		{
			base.transform.position -= base.transform.up * climbSpeed * Time.unscaledDeltaTime;
		}
		Input.GetKeyDown(KeyCode.End);
	}
}
