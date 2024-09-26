using UnityEngine;

public class Bomb : MonoBehaviour
{
	private bool triggered;

	public float minCollision = 10f;

	public float timeUntilBoom = 5f;

	public GameObject explosionGO;

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > minCollision && !triggered)
		{
			new WaitForSeconds(timeUntilBoom);
			Explode();
		}
	}

	public void Explode()
	{
		if (!triggered)
		{
			triggered = true;
			UnityEngine.Debug.Log("BOOM");
			Object.Instantiate(explosionGO, base.transform.position, base.transform.rotation);
		}
	}
}
