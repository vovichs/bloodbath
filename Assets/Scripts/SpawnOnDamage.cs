using UnityEngine;

public class SpawnOnDamage : MonoBehaviour
{
	public GameObject objectToSpawn;

	private void Damage(GunHit gunHit)
	{
		Object.Instantiate(objectToSpawn, gunHit.raycastHit.point, Quaternion.LookRotation(gunHit.raycastHit.normal));
	}
}
