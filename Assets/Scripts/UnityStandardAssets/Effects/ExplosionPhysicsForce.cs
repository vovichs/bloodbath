using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	public class ExplosionPhysicsForce : MonoBehaviour
	{
		public float explosionForce = 4f;

		public float Damage;

		private IEnumerator Start()
		{
			yield return null;
			float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;
			float num = 10f * multiplier;
			Collider[] array = Physics.OverlapSphere(base.transform.position, num);
			List<Rigidbody> list = new List<Rigidbody>();
			Collider[] array2 = array;
			foreach (Collider collider in array2)
			{
				if (collider.GetComponent<DamageSystem>() != null)
				{
					collider.GetComponent<DamageSystem>().TakeDamage(Damage / Vector3.Distance(collider.transform.position, base.transform.position));
				}
				if (collider.attachedRigidbody != null && !list.Contains(collider.attachedRigidbody))
				{
					list.Add(collider.attachedRigidbody);
				}
			}
			foreach (Rigidbody item in list)
			{
				item.AddExplosionForce(explosionForce * multiplier, base.transform.position, num, 1f * multiplier, ForceMode.Impulse);
			}
		}
	}
}
