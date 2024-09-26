using System.Collections;
using UnityEngine;

public class eventGun : MonoBehaviour
{
	public LayerMask mask;

	public bool UseAble = true;

	public bool forceFire;

	public int totalClips;

	public int clipLeft;

	public int clipSize = 10;

	public float reloadTime = 4f;

	public bool maxAmmo;

	public int shotFragments = 8;

	public float spreadAngle = 10f;

	private float startSpreadAngle;

	public float damage = 10f;

	public float range = 100f;

	public float impactForce = 30f;

	public float fireRate = 15f;

	private float recoil = 0.01f;

	private float recoilDuration = 0.2f;

	private bool reloading;

	private float lowestPitch = 0.8f;

	private float highestPitch = 1.2f;

	public ParticleSystem flash;

	public Transform flashPos;

	public GameObject impactEffect;

	public GameObject bloodEffect;

	public GameObject impactHole;

	public GameObject bloodHole;

	public AudioSource gunAS;

	public AudioClip shootSound;

	public AudioClip noAmmo;

	private float nextTimeToFire;

	private void OnEnable()
	{
		reloading = false;
	}

	public IEnumerator Reload()
	{
		if (totalClips > 0)
		{
			clipLeft = 0;
			yield return new WaitForSeconds(reloadTime);
			totalClips--;
			clipLeft = clipSize;
			yield return new WaitForSeconds(0.5f);
			reloading = false;
		}
	}

	private void Start()
	{
		startSpreadAngle = spreadAngle;
	}

	private void Update()
	{
		if (UseAble)
		{
			if (clipLeft == 0 && !reloading)
			{
				reloading = true;
				StartCoroutine(Reload());
			}
			if (Time.timeScale != 0f && !reloading && forceFire && Time.time >= nextTimeToFire)
			{
				nextTimeToFire = Time.time + 1f / fireRate;
				StartCoroutine(Shoot());
			}
		}
	}

	public IEnumerator Shoot()
	{
		if (clipLeft == 0 && !maxAmmo)
		{
			gunAS.PlayOneShot(noAmmo);
			yield break;
		}
		if (!maxAmmo)
		{
			clipLeft--;
		}
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
			if (!Physics.Raycast(base.transform.position, from * Vector3.forward, out RaycastHit hit, range, mask))
			{
				continue;
			}
			DamageSystem component = hit.transform.GetComponent<DamageSystem>();
			if (component != null)
			{
				component.TakeDamage(damage);
			}
			if (hit.transform.tag == "NPC")
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
