using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
	public float minImpactVelocity = 0.1f;

	public float maxImpactVelocity = 1f;

	private float _sqrMinImpactVelocity;

	private float _lastMinImpactVelocity;

	private float _sqrMaxImpactVelocity;

	private float _lastMaxImpactVelocity;

	public float minPitch = 0.5f;

	public float maxPitch = 1.5f;

	public float sqrMinImpactVelocity
	{
		get
		{
			if (_lastMinImpactVelocity != minImpactVelocity)
			{
				_lastMinImpactVelocity = minImpactVelocity;
				_sqrMinImpactVelocity = minImpactVelocity * minImpactVelocity;
			}
			return _sqrMinImpactVelocity;
		}
	}

	public float sqrMaxImpactVelocity
	{
		get
		{
			if (_lastMaxImpactVelocity != maxImpactVelocity)
			{
				_lastMaxImpactVelocity = maxImpactVelocity;
				_sqrMaxImpactVelocity = maxImpactVelocity * maxImpactVelocity;
			}
			return _sqrMaxImpactVelocity;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		float sqrMagnitude = collision.relativeVelocity.sqrMagnitude;
		if (sqrMagnitude > sqrMinImpactVelocity)
		{
			GetComponent<AudioSource>().volume = Mathf.Lerp(0f, 1f, (sqrMagnitude - sqrMinImpactVelocity) / sqrMaxImpactVelocity);
			GetComponent<AudioSource>().pitch = Mathf.Lerp(minPitch, maxPitch, (sqrMagnitude - sqrMinImpactVelocity) / sqrMaxImpactVelocity);
			GetComponent<AudioSource>().Play();
		}
	}
}
