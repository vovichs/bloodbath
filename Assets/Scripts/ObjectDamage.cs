using UnityEngine;
using UnityEngine.Events;

public class ObjectDamage : MonoBehaviour
{
	public float health = 100f;

	public float minMagnitude = 5f;

	public bool damageOnCollision = true;

	public bool broken;

	public UnityEvent onDeath;

	private AudioSource AS;

	private Rigidbody rigid;

	private void Start()
	{
		AS = GetComponent<AudioSource>();
		rigid = GetComponent<Rigidbody>();
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (damageOnCollision && !broken && collision.relativeVelocity.magnitude > minMagnitude)
		{
			health -= collision.relativeVelocity.magnitude / 2f;
			if (health <= 0f)
			{
				rigid.isKinematic = false;
				broken = true;
				onDeath.Invoke();
			}
		}
	}
}
