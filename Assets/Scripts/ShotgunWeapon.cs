using UnityEngine;

public class ShotgunWeapon : WeaponBase
{
	public int shotFragments = 8;

	public float spreadAngle = 10f;

	protected override void PrimaryFire()
	{
		for (int i = 0; i < shotFragments; i++)
		{
			Quaternion from = Quaternion.LookRotation(base.transform.forward);
			Quaternion rotation = Random.rotation;
			from = Quaternion.RotateTowards(from, rotation, Random.Range(0f, spreadAngle));
			if (Physics.Raycast(base.transform.position, from * Vector3.forward, out RaycastHit hitInfo, float.PositiveInfinity, layerMask))
			{
				GunHit gunHit = new GunHit();
				gunHit.damage = damage;
				gunHit.raycastHit = hitInfo;
				hitInfo.collider.SendMessage("Damage", gunHit, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
