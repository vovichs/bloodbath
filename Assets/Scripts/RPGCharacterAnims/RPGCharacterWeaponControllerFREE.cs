using System.Collections;
using UnityEngine;

namespace RPGCharacterAnims
{
	public class RPGCharacterWeaponControllerFREE : MonoBehaviour
	{
		private RPGCharacterControllerFREE rpgCharacterController;

		private Animator animator;

		[HideInInspector]
		public int leftWeapon;

		[HideInInspector]
		public bool isSwitchingFinished = true;

		[HideInInspector]
		public bool isWeaponSwitching;

		[HideInInspector]
		public bool instantWeaponSwitch;

		public GameObject twoHandSword;

		private void Awake()
		{
			rpgCharacterController = GetComponent<RPGCharacterControllerFREE>();
			animator = GetComponentInChildren<Animator>();
			StartCoroutine(_HideAllWeapons(timed: false, resetToUnarmed: false));
		}

		public IEnumerator _SwitchWeapon(int weaponNumber)
		{
			if (instantWeaponSwitch)
			{
				StartCoroutine(_InstantWeaponSwitch(weaponNumber));
				yield break;
			}
			isSwitchingFinished = false;
			isWeaponSwitching = true;
			if (IsNoWeapon(animator.GetInteger("Weapon")))
			{
				if (weaponNumber == -1)
				{
					StartCoroutine(_SheathWeapon(0, -1));
				}
				else
				{
					StartCoroutine(_UnSheathWeapon(weaponNumber));
				}
			}
			else if (Is2HandedWeapon(animator.GetInteger("Weapon")))
			{
				StartCoroutine(_SheathWeapon(leftWeapon, weaponNumber));
				yield return new WaitForSeconds(1.2f);
				if (weaponNumber > 0)
				{
					StartCoroutine(_UnSheathWeapon(weaponNumber));
				}
			}
			yield return null;
		}

		public IEnumerator _UnSheathWeapon(int weaponNumber)
		{
			if (Is2HandedWeapon(weaponNumber))
			{
				if (Is2HandedWeapon(animator.GetInteger("Weapon")))
				{
					DoWeaponSwitch(0, weaponNumber, weaponNumber, -1, sheath: false);
					yield return new WaitForSeconds(0.75f);
					SetAnimator(weaponNumber, -2, animator.GetInteger("Weapon"), -1, -1);
				}
				else
				{
					DoWeaponSwitch(animator.GetInteger("Weapon"), weaponNumber, weaponNumber, -1, sheath: false);
					yield return new WaitForSeconds(0.75f);
					SetAnimator(weaponNumber, -2, weaponNumber, -1, -1);
				}
			}
			rpgCharacterController.SetWeaponState(weaponNumber);
			yield return null;
		}

		public IEnumerator _SheathWeapon(int weaponNumber, int weaponTo)
		{
			if (weaponTo < 1)
			{
				if (leftWeapon != 0)
				{
					DoWeaponSwitch(weaponTo, weaponNumber, animator.GetInteger("Weapon"), -1, sheath: true);
					yield return new WaitForSeconds(0.5f);
					SetAnimator(weaponTo, -2, 0, 0, -1);
				}
			}
			else if (Is2HandedWeapon(weaponTo))
			{
				if (animator.GetInteger("Weapon") == 7)
				{
					DoWeaponSwitch(0, weaponNumber, 7, -1, sheath: true);
					yield return new WaitForSeconds(0.5f);
				}
				else
				{
					DoWeaponSwitch(0, weaponNumber, animator.GetInteger("Weapon"), -1, sheath: true);
					yield return new WaitForSeconds(0.5f);
					SetAnimator(weaponNumber, -2, weaponNumber, 0, -1);
				}
			}
			rpgCharacterController.SetWeaponState(weaponTo);
			yield return null;
		}

		private IEnumerator _InstantWeaponSwitch(int weaponNumber)
		{
			animator.SetInteger("Weapon", -2);
			yield return new WaitForEndOfFrame();
			animator.SetTrigger("InstantSwitchTrigger");
			animator.SetInteger("Weapon", weaponNumber);
			StartCoroutine(_HideAllWeapons(timed: false, resetToUnarmed: false));
			StartCoroutine(_WeaponVisibility(weaponNumber, visible: true, dual: false));
			rpgCharacterController.SetWeaponState(weaponNumber);
		}

		private void DoWeaponSwitch(int weaponSwitch, int weaponVisibility, int weaponNumber, int leftRight, bool sheath)
		{
			animator.SetInteger("Weapon", -2);
			while (animator.isActiveAndEnabled && animator.GetInteger("Weapon") != -2)
			{
			}
			if (weaponSwitch < 1 && Is2HandedWeapon(weaponNumber))
			{
				rpgCharacterController.Lock(lockMovement: true, lockAction: true, timed: true, 0f, 1f);
			}
			if (weaponSwitch != -2)
			{
				animator.SetInteger("WeaponSwitch", weaponSwitch);
			}
			animator.SetInteger("Weapon", weaponNumber);
			if (sheath)
			{
				animator.SetTrigger("WeaponSheathTrigger");
				StartCoroutine(_WeaponVisibility(weaponVisibility, visible: false, dual: false));
				if (rpgCharacterController.ikHands != null)
				{
					StartCoroutine(rpgCharacterController.ikHands._BlendIK(blendOn: false, 0f, 0.2f, weaponVisibility));
				}
			}
			else
			{
				animator.SetTrigger("WeaponUnsheathTrigger");
				StartCoroutine(_WeaponVisibility(weaponVisibility, visible: true, dual: false));
				if (rpgCharacterController.ikHands != null)
				{
					StartCoroutine(rpgCharacterController.ikHands._BlendIK(blendOn: true, 0.5f, 1f, weaponVisibility));
				}
			}
		}

		public void SwitchWeaponTwoHand(int upDown)
		{
			if (instantWeaponSwitch)
			{
				StartCoroutine(_HideAllWeapons(timed: false, resetToUnarmed: false));
			}
			isSwitchingFinished = false;
			if (upDown == 0)
			{
				StartCoroutine(_SwitchWeapon(0));
			}
			if (upDown == 1)
			{
				StartCoroutine(_SwitchWeapon(1));
			}
		}

		public void WeaponSwitch()
		{
			if (isWeaponSwitching)
			{
				isWeaponSwitching = false;
			}
		}

		public IEnumerator _HideAllWeapons(bool timed, bool resetToUnarmed)
		{
			if (timed)
			{
				while (!isWeaponSwitching && instantWeaponSwitch)
				{
					yield return null;
				}
			}
			if (resetToUnarmed)
			{
				animator.SetInteger("Weapon", 0);
				rpgCharacterController.weapon = Weapon.UNARMED;
				StartCoroutine(rpgCharacterController.rpgCharacterWeaponController._WeaponVisibility(rpgCharacterController.rpgCharacterWeaponController.leftWeapon, visible: false, dual: true));
				animator.SetInteger("RightWeapon", 0);
				animator.SetInteger("LeftWeapon", 0);
				animator.SetInteger("LeftRight", 0);
			}
			if (twoHandSword != null)
			{
				twoHandSword.SetActive(value: false);
			}
		}

		private void SetAnimator(int weapon, int weaponSwitch, int Lweapon, int Rweapon, int weaponSide)
		{
			UnityEngine.Debug.Log("SETANIMATOR: Weapon:" + weapon + " Weaponswitch:" + weaponSwitch + " Lweapon:" + Lweapon + " Rweapon:" + Rweapon + " Weaponside:" + weaponSide);
			if (weapon != -2)
			{
				animator.SetInteger("Weapon", weapon);
			}
			if (weaponSwitch != -2)
			{
				animator.SetInteger("WeaponSwitch", weaponSwitch);
			}
			if (Lweapon != -1)
			{
				leftWeapon = Lweapon;
				animator.SetInteger("LeftWeapon", Lweapon);
			}
			if (weaponSide != -1)
			{
				animator.SetInteger("LeftRight", weaponSide);
			}
			rpgCharacterController.SetWeaponState(weapon);
		}

		public IEnumerator _WeaponVisibility(int weaponNumber, bool visible, bool dual)
		{
			while (isWeaponSwitching)
			{
				yield return null;
			}
			if (weaponNumber == 1)
			{
				twoHandSword.SetActive(visible);
			}
			yield return null;
		}

		public bool IsNoWeapon(int weaponNumber)
		{
			if (weaponNumber < 1)
			{
				return true;
			}
			return false;
		}

		public bool Is2HandedWeapon(int weaponNumber)
		{
			if ((weaponNumber > 0 && weaponNumber < 7) || weaponNumber == 18 || weaponNumber == 20)
			{
				return true;
			}
			return false;
		}
	}
}
