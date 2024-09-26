using UnityEngine;

public class RaycastGun : MonoBehaviour
{
	public float fireDelay = 0.1f;

	public float damage = 1f;

	public string buttonName = "Fire1";

	public float maxBulletSpreadAngle = 15f;

	public float timeTillMaxSpreadAngle = 1f;

	public AnimationCurve bulletSpreadCurve;

	public LayerMask layerMask = -1;

	private bool readyToFire = true;

	private float fireTime;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetButton(buttonName))
		{
			fireTime += Time.deltaTime;
			if (readyToFire)
			{
				Quaternion from = Quaternion.LookRotation(base.transform.forward);
				Quaternion rotation = UnityEngine.Random.rotation;
				float max = bulletSpreadCurve.Evaluate(fireTime / timeTillMaxSpreadAngle) * maxBulletSpreadAngle;
				from = Quaternion.RotateTowards(from, rotation, UnityEngine.Random.Range(0f, max));
				if (Physics.Raycast(base.transform.position, from * Vector3.forward, out RaycastHit hitInfo, float.PositiveInfinity, layerMask))
				{
					GunHit gunHit = new GunHit();
					gunHit.damage = damage;
					gunHit.raycastHit = hitInfo;
					hitInfo.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);
					readyToFire = false;
					Invoke("SetReadyToFire", fireDelay);
				}
			}
		}
		else
		{
			fireTime = 0f;
		}
	}

	private void SetReadyToFire()
	{
		readyToFire = true;
	}
}
