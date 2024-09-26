using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{
	public bool UseAble = true;

	public LayerMask mask;

	public string gunName;

	public Text ammoText;

	public int totalClips;

	public int clipLeft;

	public int clipSize = 10;

	public float reloadTime = 4f;

	public bool maxAmmo;

	private int level;

	public bool auto;

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

	public Animator anim;

	public Camera fpsCam;

	public AudioSource gunAS;

	public AudioClip shootSound;

	public AudioClip noAmmo;

	public FlashlightFollow parent;

	private float nextTimeToFire;

	private void OnEnable()
	{
		reloading = false;
		anim.SetBool("Reloading", value: false);
	}

	public IEnumerator Reload()
	{
		if (totalClips != 0)
		{
			anim.SetBool("Reloading", value: true);
			clipLeft = 0;
			ammoText.text = clipLeft.ToString() + " / " + clipSize.ToString() + " (" + totalClips + ")";
			yield return new WaitForSeconds(reloadTime);
			totalClips--;
			clipLeft = clipSize;
			ammoText.text = clipLeft.ToString() + " / " + clipSize.ToString() + " (" + totalClips + ")";
			yield return new WaitForSeconds(0.5f);
			reloading = false;
			anim.SetBool("Reloading", value: false);
		}
	}

	private void Start()
	{
		startSpreadAngle = spreadAngle;
		if (!maxAmmo)
		{
			ammoText.text = clipLeft.ToString() + " / " + clipSize.ToString() + " (" + totalClips + ")";
			return;
		}
		clipLeft = 1;
		UnityEngine.Object.Destroy(ammoText.gameObject);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown("r") && !reloading)
		{
			reloading = true;
			StartCoroutine(Reload());
		}
		if (Input.GetMouseButton(1) && !reloading)
		{
			anim.SetBool("Aiming", value: true);
			spreadAngle = startSpreadAngle / 3f;
		}
		if (Input.GetMouseButton(1) && !reloading)
		{
			anim.SetBool("Aiming", value: true);
			spreadAngle = startSpreadAngle / 3f;
		}
		else
		{
			anim.SetBool("Aiming", value: false);
			spreadAngle = startSpreadAngle;
		}
		if (Time.timeScale == 0f)
		{
			return;
		}
		if (auto && !reloading)
		{
			if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
			{
				nextTimeToFire = Time.time + 1f / fireRate;
				StartCoroutine(Shoot());
			}
		}
		else if (!reloading && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			StartCoroutine(Shoot());
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
			ammoText.text = clipLeft.ToString() + " / " + clipSize.ToString() + " (" + totalClips + ")";
		}
		parent.Shoot();
		gunAS.pitch = UnityEngine.Random.Range(lowestPitch, highestPitch);
		gunAS.PlayOneShot(shootSound);
		ParticleSystem particleSystem = UnityEngine.Object.Instantiate(flash, flashPos.position, flashPos.rotation);
		particleSystem.transform.parent = flashPos;
		particleSystem.Play();
		UnityEngine.Object.Destroy(particleSystem, 5f);
		for (int i = 0; i < shotFragments; i++)
		{
			Quaternion from = Quaternion.LookRotation(fpsCam.transform.forward);
			Quaternion rotation = UnityEngine.Random.rotation;
			from = Quaternion.RotateTowards(from, rotation, UnityEngine.Random.Range(0f, spreadAngle));
			if (!Physics.Raycast(fpsCam.transform.position, from * Vector3.forward, out RaycastHit hit, range, mask))
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
