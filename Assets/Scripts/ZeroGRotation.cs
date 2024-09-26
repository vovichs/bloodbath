using UnityEngine;

public class ZeroGRotation : MonoBehaviour
{
	public string verticalAxisName = "Mouse Y";

	public string horizontalAxisName = "Mouse X";

	public float force = 10f;

	public ForceMode forceMode;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		GetComponent<Rigidbody>().AddTorque(base.transform.up * force * UnityEngine.Input.GetAxis(horizontalAxisName), forceMode);
		GetComponent<Rigidbody>().AddTorque(-base.transform.right * force * UnityEngine.Input.GetAxis(verticalAxisName), forceMode);
	}
}
