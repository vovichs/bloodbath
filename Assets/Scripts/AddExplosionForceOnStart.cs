using UnityEngine;

public class AddExplosionForceOnStart : MonoBehaviour
{
	public float force = 100f;

	public float radius = 5f;

	public float upwardsModifier;

	public ForceMode forceMode;

	private void Start()
	{
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
