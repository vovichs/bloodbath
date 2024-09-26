using System.Collections;
using UnityEngine;

public class NPCgun : MonoBehaviour
{
	public int shotFragments = 8;

	public float spreadAngle = 10f;

	private float startSpreadAngle;

	public float damage = 10f;

	public float range = 100f;

	public float impactForce = 30f;

	public float fireRate = 15f;

	public float recoil = 0.01f;

	public float recoilDuration = 0.2f;

	public float lowestPitch = 0.8f;

	public float highestPitch = 1.2f;

	public ParticleSystem flash;

	public Transform flashPos;

	public GameObject impactEffect;

	public GameObject bloodEffect;

	public GameObject impactHole;

	public GameObject bloodHole;

	public AudioSource gunAS;

	public AudioClip shootSound;

	public bool useAble = true;

	public bool gonnaShoot;

	private float nextTimeToFire;

	private void Update()
	{
		if (Time.timeScale != 0f && gonnaShoot && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			StartCoroutine(Shoot());
		}
	}

	public void UnuseAble()
	{
		useAble = false;
	}

	public IEnumerator Shoot()
	{
		if (!useAble)
		{
			yield break;
		}
		gonnaShoot = false;
		gunAS.pitch = UnityEngine.Random.Range(lowestPitch, highestPitch);
		gunAS.PlayOneShot(shootSound);
		ParticleSystem particleSystem = UnityEngine.Object.Instantiate(flash, flashPos.position, flashPos.rotation);
		particleSystem.transform.parent = flashPos;
		particleSystem.Play();
		UnityEngine.Object.Destroy(particleSystem, 5f);
		for (int i = 0; i < shotFragments; i++)
		{
			Quaternion from = Quaternion.LookRotation(base.transform.forward);
			Quaternion rotation = UnityEngine.Random.rotation;
			from = Quaternion.RotateTowards(from, rotation, UnityEngine.Random.Range(0f, spreadAngle));
			if (!Physics.Raycast(base.gameObject.transform.position, from * Vector3.forward, out RaycastHit hit, range))
			{
				continue;
			}
			Player component = hit.transform.GetComponent<Player>();
			if (component != null)
			{
				component.TakeDamage(damage);
			}
			if (hit.transform.tag == "Player")
			{
				UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal)), 2f);
			}
			else if (hit.transform.tag == "NPC")
			{
				UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal)), 2f);
			}
			else
			{
				UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)), 2f);
			}
			if (hit.transform.tag != "Decal" && hit.transform.tag == "NPC")
			{
				Object.Instantiate(bloodHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)).transform.parent = hit.collider.gameObject.transform;
			}
			if (hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * impactForce);
				if (hit.rigidbody.isKinematic)
				{
					yield return new WaitForSeconds(0.01f);
					hit.rigidbody.AddForce(-hit.normal * 1000f);
				}
			}
		}
	}
}
