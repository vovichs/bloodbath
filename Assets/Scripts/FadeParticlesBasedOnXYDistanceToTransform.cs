using UnityEngine;

public class FadeParticlesBasedOnXYDistanceToTransform : MonoBehaviour
{
	public float distanceFalloffMultiplier = 1f;

	private void Start()
	{
	}

	private void Update()
	{
		ParticleSystem.Particle[] array = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];
		GetComponent<ParticleSystem>().GetParticles(array);
		for (int i = 0; i < GetComponent<ParticleSystem>().particleCount; i++)
		{
			Vector3 point = array[i].position - base.transform.position;
			point = Quaternion.Inverse(base.transform.rotation) * point;
			point.z = 0f;
			point = base.transform.rotation * point;
			float a = Mathf.Lerp(1f, 0f, point.sqrMagnitude * distanceFalloffMultiplier);
			Color c = array[i].color;
			c.a = a;
			array[i].color = c;
		}
		GetComponent<ParticleSystem>().SetParticles(array, GetComponent<ParticleSystem>().particleCount);
	}
}
