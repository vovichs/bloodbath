using UnityEngine;

public class AddForceOnStart : MonoBehaviour
{
	public float force = 100f;

	public ForceMode forceMode;

	private void Start()
	{
		GetComponent<Rigidbody>().AddForce(base.transform.forward * force, forceMode);
	}
}
