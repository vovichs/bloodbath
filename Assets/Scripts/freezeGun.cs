using UnityEngine;

public class freezeGun : MonoBehaviour
{
	public float range = 100f;

	public float fireRate = 15f;

	public float recoil = 0.01f;

	public float recoilDuration = 0.2f;

	public float lowestPitch = 0.8f;

	public float highestPitch = 1.2f;

	public Camera fpsCam;

	public AudioSource gunAS;

	public AudioClip shootSound;

	public AudioClip noAmmo;

	public FlashlightFollow parent;

	private float nextTimeToFire;

	private void Update()
	{
		if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
		}
	}

	private void Shoot()
	{
		parent.Shoot();
		gunAS.pitch = UnityEngine.Random.Range(lowestPitch, highestPitch);
		gunAS.PlayOneShot(shootSound);
		if (!Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hitInfo, range))
		{
			return;
		}
		Rigidbody component = hitInfo.transform.GetComponent<Rigidbody>();
		if (component != null)
		{
			if (!component.isKinematic)
			{
				component.isKinematic = true;
			}
			else
			{
				component.isKinematic = false;
			}
		}
	}
}
