using UnityEngine;

public class ApplyForceInRandomDirection : MonoBehaviour
{
	public string buttonName = "Fire1";

	public float forceAmount = 10f;

	public float torqueAmount = 10f;

	public ForceMode forceMode;

	private void Update()
	{
		if (Input.GetButtonDown(buttonName))
		{
			GetComponent<Rigidbody>().AddForce(UnityEngine.Random.onUnitSphere * forceAmount, forceMode);
			GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.onUnitSphere * torqueAmount, forceMode);
		}
	}
}
