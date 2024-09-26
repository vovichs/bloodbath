using UnityEngine;

public class ExplodeOnButton : MonoBehaviour
{
	public string buttonName = "Fire1";

	public float force = 100f;

	public float radius = 5f;

	public float upwardsModifier;

	public ForceMode forceMode;

	private void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetButtonDown(buttonName))
		{
			return;
		}
		Collider[] array = Physics.OverlapSphere(base.transform.position, radius);
		foreach (Collider collider in array)
		{
			if (collider.GetComponent<Rigidbody>() != null)
			{
				collider.GetComponent<Rigidbody>().AddExplosionForce(force, base.transform.position, radius, upwardsModifier, forceMode);
			}
		}
	}
}
