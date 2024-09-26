using System.Collections;
using UnityEngine;

namespace RPGCharacterAnims
{
	public class IKHandsFREE : MonoBehaviour
	{
		private Animator animator;

		private RPGCharacterWeaponControllerFREE rpgCharacterWeaponController;

		public Transform leftHandObj;

		public Transform attachLeft;

		[Range(0f, 1f)]
		public float leftHandPositionWeight;

		[Range(0f, 1f)]
		public float leftHandRotationWeight;

		private Transform blendToTransform;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			rpgCharacterWeaponController = GetComponent<RPGCharacterWeaponControllerFREE>();
		}

		private void OnAnimatorIK(int layerIndex)
		{
			if (leftHandObj != null)
			{
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotationWeight);
				animator.SetIKPosition(AvatarIKGoal.LeftHand, attachLeft.position);
				animator.SetIKRotation(AvatarIKGoal.LeftHand, attachLeft.rotation);
			}
		}

		public IEnumerator _BlendIK(bool blendOn, float delay, float timeToBlend, int weapon)
		{
			if (weapon > 0)
			{
				GetCurrentWeaponAttachPoint(weapon);
				yield return new WaitForSeconds(delay);
				float t = 0f;
				float blendTo = 0f;
				float blendFrom = 0f;
				if (blendOn)
				{
					blendTo = 1f;
				}
				else
				{
					blendFrom = 1f;
				}
				while (t < 1f)
				{
					t += Time.deltaTime / timeToBlend;
					attachLeft = blendToTransform;
					leftHandPositionWeight = Mathf.Lerp(blendFrom, blendTo, t);
					leftHandRotationWeight = Mathf.Lerp(blendFrom, blendTo, t);
					yield return null;
				}
				rpgCharacterWeaponController.isSwitchingFinished = true;
			}
		}

		private void GetCurrentWeaponAttachPoint(int weapon)
		{
			if (weapon == 1)
			{
				blendToTransform = rpgCharacterWeaponController.twoHandSword.transform.GetChild(0).transform;
			}
		}
	}
}
