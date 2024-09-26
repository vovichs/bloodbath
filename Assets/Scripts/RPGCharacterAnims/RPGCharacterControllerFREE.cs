using System.Collections;
using UnityEngine;

namespace RPGCharacterAnims
{
	[RequireComponent(typeof(RPGCharacterMovementControllerFREE))]
	[RequireComponent(typeof(RPGCharacterWeaponControllerFREE))]
	[RequireComponent(typeof(RPGCharacterInputControllerFREE))]
	public class RPGCharacterControllerFREE : MonoBehaviour
	{
		[HideInInspector]
		public RPGCharacterMovementControllerFREE rpgCharacterMovementController;

		[HideInInspector]
		public RPGCharacterWeaponControllerFREE rpgCharacterWeaponController;

		[HideInInspector]
		public RPGCharacterInputControllerFREE rpgCharacterInputController;

		[HideInInspector]
		public Animator animator;

		[HideInInspector]
		public IKHandsFREE ikHands;

		public Weapon weapon;

		public GameObject target;

		[HideInInspector]
		public bool isDead;

		[HideInInspector]
		public bool isBlocking;

		[HideInInspector]
		public bool isStrafing;

		[HideInInspector]
		public bool canAction = true;

		public float animationSpeed = 1f;

		private void Awake()
		{
			rpgCharacterMovementController = GetComponent<RPGCharacterMovementControllerFREE>();
			rpgCharacterWeaponController = GetComponent<RPGCharacterWeaponControllerFREE>();
			rpgCharacterInputController = GetComponent<RPGCharacterInputControllerFREE>();
			animator = GetComponentInChildren<Animator>();
			if (animator == null)
			{
				UnityEngine.Debug.LogError("ERROR: There is no animator for character.");
				UnityEngine.Object.Destroy(this);
			}
			if (target == null)
			{
				UnityEngine.Debug.LogError("ERROR: There is no target set for character.");
				UnityEngine.Object.Destroy(this);
			}
			ikHands = GetComponent<IKHandsFREE>();
			weapon = Weapon.UNARMED;
			animator.SetInteger("Weapon", 0);
			animator.SetInteger("WeaponSwitch", -1);
		}

		private void Start()
		{
			rpgCharacterMovementController.SwitchCollisionOn();
		}

		private void Update()
		{
			UpdateAnimationSpeed();
			if (rpgCharacterMovementController.MaintainingGround())
			{
				if (rpgCharacterInputController.inputDeath && isDead)
				{
					Revive();
				}
				if (canAction && !isBlocking)
				{
					Strafing();
					Rolling();
					if (rpgCharacterInputController.inputLightHit)
					{
						GetHit();
					}
					if (rpgCharacterInputController.inputDeath)
					{
						if (!isDead)
						{
							Death();
						}
						else
						{
							Revive();
						}
					}
					if (rpgCharacterInputController.inputAttackL)
					{
						Attack(1);
					}
					if (rpgCharacterInputController.inputAttackR)
					{
						Attack(2);
					}
					if (rpgCharacterInputController.inputLightHit)
					{
						GetHit();
					}
					if (rpgCharacterInputController.inputSwitchUpDown && rpgCharacterWeaponController.isSwitchingFinished)
					{
						if (weapon == Weapon.UNARMED)
						{
							rpgCharacterWeaponController.SwitchWeaponTwoHand(1);
						}
						else
						{
							rpgCharacterWeaponController.SwitchWeaponTwoHand(0);
						}
					}
					if (Input.GetMouseButtonDown(0) && rpgCharacterMovementController.useMeshNav && Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out RaycastHit hitInfo, 100f))
					{
						rpgCharacterMovementController.navMeshAgent.destination = hitInfo.point;
					}
				}
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.T))
			{
				if (Time.timeScale != 1f)
				{
					Time.timeScale = 1f;
				}
				else
				{
					Time.timeScale = 0.005f;
				}
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.P))
			{
				if (Time.timeScale != 1f)
				{
					Time.timeScale = 1f;
				}
				else
				{
					Time.timeScale = 0f;
				}
			}
			if (rpgCharacterInputController.inputStrafe)
			{
				animator.SetBool("Strafing", value: true);
			}
			else
			{
				animator.SetBool("Strafing", value: false);
			}
		}

		private void UpdateAnimationSpeed()
		{
			animator.SetFloat("AnimationSpeed", animationSpeed);
		}

		public IEnumerator _Turning(int direction)
		{
			if (direction == 1)
			{
				Lock(lockMovement: true, lockAction: true, timed: true, 0f, 0.55f);
				animator.SetTrigger("TurnLeftTrigger");
			}
			if (direction == 2)
			{
				Lock(lockMovement: true, lockAction: true, timed: true, 0f, 0.55f);
				animator.SetTrigger("TurnRightTrigger");
			}
			yield return null;
		}

		public void Attack(int attackSide)
		{
			int value = 0;
			if (!canAction)
			{
				return;
			}
			if (rpgCharacterMovementController.MaintainingGround() && !rpgCharacterMovementController.isMoving)
			{
				if (weapon == Weapon.RELAX)
				{
					weapon = Weapon.UNARMED;
					animator.SetInteger("Weapon", 0);
				}
				if (weapon == Weapon.UNARMED || weapon == Weapon.ARMED || weapon == Weapon.ARMEDSHIELD)
				{
					int num = 3;
					switch (attackSide)
					{
					case 1:
						animator.SetInteger("AttackSide", 1);
						value = UnityEngine.Random.Range(1, num + 1);
						break;
					case 2:
						animator.SetInteger("AttackSide", 2);
						value = UnityEngine.Random.Range(4, num + 4);
						break;
					}
					if (attackSide != 3)
					{
						Lock(lockMovement: true, lockAction: true, timed: true, 0f, 1.25f);
					}
				}
				else
				{
					int max = 6;
					value = UnityEngine.Random.Range(1, max);
					if (weapon == Weapon.TWOHANDSWORD)
					{
						Lock(lockMovement: true, lockAction: true, timed: true, 0f, 0.85f);
					}
					else
					{
						Lock(lockMovement: true, lockAction: true, timed: true, 0f, 0.75f);
					}
				}
			}
			animator.SetInteger("Action", value);
			if (attackSide == 3)
			{
				animator.SetTrigger("AttackDualTrigger");
			}
			else
			{
				animator.SetTrigger("AttackTrigger");
			}
		}

		public void AttackKick(int kickSide)
		{
			if (rpgCharacterMovementController.MaintainingGround())
			{
				if (weapon == Weapon.RELAX)
				{
					weapon = Weapon.UNARMED;
					animator.SetInteger("Weapon", 0);
				}
				animator.SetInteger("Action", kickSide);
				animator.SetTrigger("AttackKickTrigger");
				Lock(lockMovement: true, lockAction: true, timed: true, 0f, 0.9f);
			}
		}

		private void Strafing()
		{
			if (rpgCharacterInputController.inputStrafe && weapon != Weapon.RIFLE)
			{
				if (weapon != Weapon.RELAX)
				{
					animator.SetBool("Strafing", value: true);
					isStrafing = true;
				}
			}
			else
			{
				isStrafing = false;
				animator.SetBool("Strafing", value: false);
			}
		}

		private void Rolling()
		{
			if (!rpgCharacterMovementController.isRolling && rpgCharacterInputController.inputRoll)
			{
				rpgCharacterMovementController.DirectionalRoll();
			}
		}

		public void GetHit()
		{
			if (weapon == Weapon.RELAX)
			{
				weapon = Weapon.UNARMED;
				animator.SetInteger("Weapon", 0);
			}
			if (weapon == Weapon.RIFLE && weapon == Weapon.TWOHANDCROSSBOW)
			{
				return;
			}
			int num = 5;
			if (isBlocking)
			{
				num = 2;
			}
			int num2 = UnityEngine.Random.Range(1, num + 1);
			animator.SetInteger("Action", num2);
			animator.SetTrigger("GetHitTrigger");
			Lock(lockMovement: true, lockAction: true, timed: true, 0.1f, 0.4f);
			if (isBlocking)
			{
				StartCoroutine(rpgCharacterMovementController._Knockback(-base.transform.forward, 3, 3));
				return;
			}
			if (num2 <= 1)
			{
				StartCoroutine(rpgCharacterMovementController._Knockback(-base.transform.forward, 8, 4));
				return;
			}
			switch (num2)
			{
			case 2:
				StartCoroutine(rpgCharacterMovementController._Knockback(base.transform.forward, 8, 4));
				break;
			case 3:
				StartCoroutine(rpgCharacterMovementController._Knockback(base.transform.right, 8, 4));
				break;
			case 4:
				StartCoroutine(rpgCharacterMovementController._Knockback(-base.transform.right, 8, 4));
				break;
			}
		}

		public void Death()
		{
			animator.SetTrigger("Death1Trigger");
			Lock(lockMovement: true, lockAction: true, timed: false, 0.1f, 0f);
			isDead = true;
		}

		public void Revive()
		{
			animator.SetTrigger("Revive1Trigger");
			Lock(lockMovement: true, lockAction: true, timed: true, 0f, 1f);
			isDead = false;
		}

		private void LockAction()
		{
			canAction = false;
		}

		private void UnLock(bool movement, bool actions)
		{
			if (movement)
			{
				rpgCharacterMovementController.UnlockMovement();
			}
			if (actions)
			{
				canAction = true;
			}
		}

		public void Hit()
		{
		}

		public void Shoot()
		{
		}

		public void FootR()
		{
		}

		public void FootL()
		{
		}

		public void Land()
		{
		}

		private IEnumerator _GetCurrentAnimationLength()
		{
			yield return new WaitForEndOfFrame();
			UnityEngine.Debug.Log((float)animator.GetCurrentAnimatorClipInfo(0).Length);
		}

		public void Lock(bool lockMovement, bool lockAction, bool timed, float delayTime, float lockTime)
		{
			StopCoroutine("_Lock");
			StartCoroutine(_Lock(lockMovement, lockAction, timed, delayTime, lockTime));
		}

		public IEnumerator _Lock(bool lockMovement, bool lockAction, bool timed, float delayTime, float lockTime)
		{
			if (delayTime > 0f)
			{
				yield return new WaitForSeconds(delayTime);
			}
			if (lockMovement)
			{
				rpgCharacterMovementController.LockMovement();
			}
			if (lockAction)
			{
				LockAction();
			}
			if (timed)
			{
				if (lockTime > 0f)
				{
					yield return new WaitForSeconds(lockTime);
				}
				UnLock(lockMovement, lockAction);
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
				rpgCharacterWeaponController.leftWeapon = Lweapon;
				animator.SetInteger("LeftWeapon", Lweapon);
				if (Lweapon == 7)
				{
					animator.SetBool("Shield", value: true);
				}
				else
				{
					animator.SetBool("Shield", value: false);
				}
			}
			if (weaponSide != -1)
			{
				animator.SetInteger("LeftRight", weaponSide);
			}
			SetWeaponState(weapon);
		}

		public void SetWeaponState(int weaponNumber)
		{
			switch (weaponNumber)
			{
			case -1:
				weapon = Weapon.RELAX;
				break;
			case 0:
				weapon = Weapon.UNARMED;
				break;
			case 1:
				weapon = Weapon.TWOHANDSWORD;
				break;
			}
		}

		public void AnimatorDebug()
		{
			UnityEngine.Debug.Log("ANIMATOR SETTINGS---------------------------");
			UnityEngine.Debug.Log("Moving: " + animator.GetBool("Moving").ToString());
			UnityEngine.Debug.Log("Strafing: " + animator.GetBool("Strafing").ToString());
			UnityEngine.Debug.Log("Stunned: " + animator.GetBool("Stunned").ToString());
			UnityEngine.Debug.Log("Weapon: " + animator.GetInteger("Weapon"));
			UnityEngine.Debug.Log("WeaponSwitch: " + animator.GetInteger("WeaponSwitch"));
			UnityEngine.Debug.Log("Jumping: " + animator.GetInteger("Jumping"));
			UnityEngine.Debug.Log("Action: " + animator.GetInteger("Action"));
			UnityEngine.Debug.Log("Velocity X: " + animator.GetFloat("Velocity X"));
			UnityEngine.Debug.Log("Velocity Z: " + animator.GetFloat("Velocity Z"));
		}
	}
}
