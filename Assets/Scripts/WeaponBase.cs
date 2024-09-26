using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
	public float damage = 1f;

	public int maxAmmo;

	public float fireDelay = 0.1f;

	public string primaryFire = "Fire1";

	public LayerMask layerMask = -1;

	public bool automaticFire;

	private bool readyToFire = true;

	private int currentAmmo;

	protected abstract void PrimaryFire();

	private void Start()
	{
		currentAmmo = maxAmmo;
	}

	private void Update()
	{
		CheckInput();
	}

	protected virtual void CheckInput()
	{
		bool flag = (!automaticFire) ? Input.GetButtonDown(primaryFire) : Input.GetButton(primaryFire);
		if (flag && readyToFire && (currentAmmo > 0 || maxAmmo == 0))
		{
			PrimaryFire();
			readyToFire = false;
			currentAmmo--;
			Invoke("SetReadyToFire", fireDelay);
		}
	}

	private void SetReadyToFire()
	{
		readyToFire = true;
	}
}
