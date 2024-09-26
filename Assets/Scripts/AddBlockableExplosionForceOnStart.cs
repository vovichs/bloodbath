using UnityEngine;

public class AddBlockableExplosionForceOnStart : MonoBehaviour
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
			RaycastHit hitInfo;
			if (collider.GetComponent<Rigidbody>() != null && Physics.Raycast(base.transform.position, collider.transform.position - base.transform.position, out hitInfo, float.PositiveInfinity) && hitInfo.collider == collider)
			{
				collider.GetComponent<Rigidbody>().AddExplosionForce(force, base.transform.position, radius, upwardsModifier, forceMode);
			}
		}
	}
}
