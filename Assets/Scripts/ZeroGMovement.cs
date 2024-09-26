using UnityEngine;

public class ZeroGMovement : MonoBehaviour
{
	public string forwardAxisName = "Vertical";

	public string horizontalAxisName = "Horizontal";

	public float force = 10f;

	public ForceMode forceMode;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		Vector3 normalized = new Vector3(UnityEngine.Input.GetAxis(horizontalAxisName), 0f, UnityEngine.Input.GetAxis(forwardAxisName)).normalized;
		GetComponent<Rigidbody>().AddForce(base.transform.rotation * normalized * force, forceMode);
	}
}
