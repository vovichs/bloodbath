using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RPGCharacterAnims
{
	public class RPGCharacterMovementControllerFREE : SuperStateMachine
	{
		private SuperCharacterController superCharacterController;

		private RPGCharacterControllerFREE rpgCharacterController;

		[HideInInspector]
		public NavMeshAgent navMeshAgent;

		private RPGCharacterInputControllerFREE rpgCharacterInputController;

		private Rigidbody rb;

		private Animator animator;

		private CapsuleCollider capCollider;

		public RPGCharacterStateFREE rpgCharacterState;

		[HideInInspector]
		public bool useMeshNav;

		[HideInInspector]
		public bool isKnockback;

		public float knockbackMultiplier = 1f;

		[HideInInspector]
		public bool canJump;

		[HideInInspector]
		public bool canDoubleJump;

		private bool doublejumped;

		public float gravity = 25f;

		public float jumpAcceleration = 5f;

		public float jumpHeight = 3f;

		public float doubleJumpHeight = 4f;

		[HideInInspector]
		public Vector3 currentVelocity;

		[HideInInspector]
		public bool isMoving;

		[HideInInspector]
		public bool canMove = true;

		public float movementAcceleration = 90f;

		public float walkSpeed = 4f;

		public float runSpeed = 6f;

		private float rotationSpeed = 40f;

		public float groundFriction = 50f;

		[HideInInspector]
		public bool isRolling;

		public float rollSpeed = 8f;

		public float rollduration = 0.35f;

		private int rollNumber;

		public float inAirSpeed = 6f;

		[HideInInspector]
		public Vector3 lookDirection
		{
			get;
			private set;
		}

		private void Awake()
		{
			superCharacterController = GetComponent<SuperCharacterController>();
			rpgCharacterController = GetComponent<RPGCharacterControllerFREE>();
			rpgCharacterInputController = GetComponent<RPGCharacterInputControllerFREE>();
			navMeshAgent = GetComponent<NavMeshAgent>();
			animator = GetComponentInChildren<Animator>();
			capCollider = GetComponent<CapsuleCollider>();
			rb = GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.constraints = (RigidbodyConstraints)80;
			}
			base.currentState = RPGCharacterStateFREE.Idle;
			rpgCharacterState = RPGCharacterStateFREE.Idle;
		}

		protected override void EarlyGlobalSuperUpdate()
		{
		}

		protected override void LateGlobalSuperUpdate()
		{
			base.transform.position += currentVelocity * superCharacterController.deltaTime;
			if (navMeshAgent != null && useMeshNav)
			{
				if (navMeshAgent.velocity.sqrMagnitude > 0f)
				{
					animator.SetBool("Moving", value: true);
					animator.SetFloat("Velocity Z", navMeshAgent.velocity.magnitude);
				}
				else
				{
					animator.SetFloat("Velocity Z", 0f);
				}
			}
			if (!useMeshNav && !rpgCharacterController.isDead && canMove)
			{
				if (currentVelocity.magnitude > 0f && rpgCharacterInputController.HasMoveInput())
				{
					isMoving = true;
					animator.SetBool("Moving", value: true);
					animator.SetFloat("Velocity Z", currentVelocity.magnitude);
				}
				else
				{
					isMoving = false;
					animator.SetBool("Moving", value: false);
				}
			}
			if (!rpgCharacterController.isStrafing)
			{
				if (rpgCharacterInputController.HasMoveInput() && canMove)
				{
					RotateTowardsMovementDir();
				}
			}
			else
			{
				Strafing(rpgCharacterController.target.transform.position);
			}
		}

		private bool AcquiringGround()
		{
			return superCharacterController.currentGround.IsGrounded(currentlyGrounded: false, 0.01f);
		}

		public bool MaintainingGround()
		{
			return superCharacterController.currentGround.IsGrounded(currentlyGrounded: true, 0.5f);
		}

		public void RotateGravity(Vector3 up)
		{
			lookDirection = Quaternion.FromToRotation(base.transform.up, up) * lookDirection;
		}

		private Vector3 LocalMovement()
		{
			return rpgCharacterInputController.moveInput;
		}

		private float CalculateJumpSpeed(float jumpHeight, float gravity)
		{
			return Mathf.Sqrt(2f * jumpHeight * gravity);
		}

		private void Idle_EnterState()
		{
			superCharacterController.EnableSlopeLimit();
			superCharacterController.EnableClamping();
			canJump = true;
			doublejumped = false;
			canDoubleJump = false;
			animator.SetInteger("Jumping", 0);
		}

		private void Idle_SuperUpdate()
		{
			if (rpgCharacterInputController.allowedInput && rpgCharacterInputController.inputJump)
			{
				base.currentState = RPGCharacterStateFREE.Jump;
				rpgCharacterState = RPGCharacterStateFREE.Jump;
			}
			else if (!MaintainingGround())
			{
				base.currentState = RPGCharacterStateFREE.Fall;
				rpgCharacterState = RPGCharacterStateFREE.Fall;
			}
			else if (rpgCharacterInputController.HasMoveInput() && canMove)
			{
				base.currentState = RPGCharacterStateFREE.Move;
				rpgCharacterState = RPGCharacterStateFREE.Move;
			}
			else
			{
				currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, groundFriction * superCharacterController.deltaTime);
			}
		}

		private void Idle_ExitState()
		{
		}

		private void Move_SuperUpdate()
		{
			if (rpgCharacterInputController.allowedInput && rpgCharacterInputController.inputJump)
			{
				base.currentState = RPGCharacterStateFREE.Jump;
				rpgCharacterState = RPGCharacterStateFREE.Jump;
			}
			else if (!MaintainingGround())
			{
				base.currentState = RPGCharacterStateFREE.Fall;
				rpgCharacterState = RPGCharacterStateFREE.Fall;
			}
			else if (rpgCharacterInputController.HasMoveInput() && canMove)
			{
				animator.SetFloat("Velocity X", 0f);
				if (rpgCharacterController.isStrafing)
				{
					currentVelocity = Vector3.MoveTowards(currentVelocity, LocalMovement() * walkSpeed, movementAcceleration * superCharacterController.deltaTime);
					if (rpgCharacterController.weapon != Weapon.RELAX)
					{
						Strafing(rpgCharacterController.target.transform.position);
					}
				}
				else
				{
					currentVelocity = Vector3.MoveTowards(currentVelocity, LocalMovement() * runSpeed, movementAcceleration * superCharacterController.deltaTime);
				}
			}
			else
			{
				base.currentState = RPGCharacterStateFREE.Idle;
				rpgCharacterState = RPGCharacterStateFREE.Idle;
			}
		}

		private void Jump_EnterState()
		{
			superCharacterController.DisableClamping();
			superCharacterController.DisableSlopeLimit();
			currentVelocity += superCharacterController.up * CalculateJumpSpeed(jumpHeight, gravity);
			if (rpgCharacterController.weapon == Weapon.RELAX)
			{
				rpgCharacterController.weapon = Weapon.UNARMED;
				animator.SetInteger("Weapon", 0);
			}
			canJump = false;
			animator.SetInteger("Jumping", 1);
			animator.SetTrigger("JumpTrigger");
		}

		private void Jump_SuperUpdate()
		{
			Vector3 vector = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
			Vector3 vector2 = currentVelocity - vector;
			if (Vector3.Angle(vector2, superCharacterController.up) > 90f && AcquiringGround())
			{
				currentVelocity = vector;
				base.currentState = RPGCharacterStateFREE.Idle;
				rpgCharacterState = RPGCharacterStateFREE.Idle;
				return;
			}
			vector = Vector3.MoveTowards(vector, LocalMovement() * inAirSpeed, jumpAcceleration * superCharacterController.deltaTime);
			vector2 -= superCharacterController.up * gravity * superCharacterController.deltaTime;
			currentVelocity = vector + vector2;
			if (currentVelocity.y < 0f)
			{
				DoubleJump();
			}
		}

		private void DoubleJump_EnterState()
		{
			currentVelocity += superCharacterController.up * CalculateJumpSpeed(doubleJumpHeight, gravity);
			canDoubleJump = false;
			doublejumped = true;
			animator.SetInteger("Jumping", 3);
			animator.SetTrigger("JumpTrigger");
		}

		private void DoubleJump_SuperUpdate()
		{
			Jump_SuperUpdate();
		}

		private void DoubleJump()
		{
			if (!doublejumped)
			{
				canDoubleJump = true;
			}
			if (rpgCharacterInputController.inputJump && canDoubleJump && !doublejumped)
			{
				base.currentState = RPGCharacterStateFREE.DoubleJump;
				rpgCharacterState = RPGCharacterStateFREE.DoubleJump;
			}
		}

		private void Fall_EnterState()
		{
			if (!doublejumped)
			{
				canDoubleJump = true;
			}
			superCharacterController.DisableClamping();
			superCharacterController.DisableSlopeLimit();
			canJump = false;
			animator.SetInteger("Jumping", 2);
			animator.SetTrigger("JumpTrigger");
		}

		private void Fall_SuperUpdate()
		{
			if (AcquiringGround())
			{
				currentVelocity = Math3d.ProjectVectorOnPlane(superCharacterController.up, currentVelocity);
				base.currentState = RPGCharacterStateFREE.Idle;
				rpgCharacterState = RPGCharacterStateFREE.Idle;
			}
			else
			{
				DoubleJump();
				currentVelocity -= superCharacterController.up * gravity * superCharacterController.deltaTime;
			}
		}

		private void Roll_SuperUpdate()
		{
			if (rollNumber == 1)
			{
				currentVelocity = Vector3.MoveTowards(currentVelocity, base.transform.forward * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
			if (rollNumber == 2)
			{
				currentVelocity = Vector3.MoveTowards(currentVelocity, base.transform.right * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
			if (rollNumber == 3)
			{
				currentVelocity = Vector3.MoveTowards(currentVelocity, -base.transform.forward * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
			if (rollNumber == 4)
			{
				currentVelocity = Vector3.MoveTowards(currentVelocity, -base.transform.right * rollSpeed, groundFriction * superCharacterController.deltaTime);
			}
		}

		public void DirectionalRoll()
		{
			float num = Vector3.Angle(rpgCharacterInputController.moveInput, -base.transform.forward);
			float num2 = Mathf.Sign(Vector3.Dot(base.transform.up, Vector3.Cross(rpgCharacterInputController.aimInput, base.transform.forward)));
			float num3 = (num * num2 + 180f) % 360f;
			if (num3 > 315f || num3 < 45f)
			{
				StartCoroutine(_Roll(1));
			}
			if (num3 > 45f && num3 < 135f)
			{
				StartCoroutine(_Roll(2));
			}
			if (num3 > 135f && num3 < 225f)
			{
				StartCoroutine(_Roll(3));
			}
			if (num3 > 225f && num3 < 315f)
			{
				StartCoroutine(_Roll(4));
			}
		}

		public IEnumerator _Roll(int roll)
		{
			rollNumber = roll;
			base.currentState = RPGCharacterStateFREE.Roll;
			rpgCharacterState = RPGCharacterStateFREE.Roll;
			if (rpgCharacterController.weapon == Weapon.RELAX)
			{
				rpgCharacterController.weapon = Weapon.UNARMED;
				animator.SetInteger("Weapon", 0);
			}
			animator.SetInteger("Action", rollNumber);
			animator.SetTrigger("RollTrigger");
			isRolling = true;
			rpgCharacterController.canAction = false;
			yield return new WaitForSeconds(rollduration);
			isRolling = false;
			rpgCharacterController.canAction = true;
			base.currentState = RPGCharacterStateFREE.Idle;
			rpgCharacterState = RPGCharacterStateFREE.Idle;
		}

		public void SwitchCollisionOff()
		{
			canMove = false;
			superCharacterController.enabled = false;
			animator.applyRootMotion = true;
			if (rb != null)
			{
				rb.isKinematic = false;
			}
		}

		public void SwitchCollisionOn()
		{
			canMove = true;
			superCharacterController.enabled = true;
			animator.applyRootMotion = false;
			if (rb != null)
			{
				rb.isKinematic = true;
			}
		}

		private void RotateTowardsMovementDir()
		{
			if (rpgCharacterInputController.moveInput != Vector3.zero)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(rpgCharacterInputController.moveInput), Time.deltaTime * rotationSpeed);
			}
		}

		private void RotateTowardsTarget(Vector3 targetPosition)
		{
			Quaternion quaternion = Quaternion.LookRotation(targetPosition - new Vector3(base.transform.position.x, 0f, base.transform.position.z));
			base.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(base.transform.eulerAngles.y, quaternion.eulerAngles.y, rotationSpeed * Time.deltaTime * rotationSpeed);
		}

		private void Aiming()
		{
			for (int i = 0; i < Input.GetJoystickNames().Length; i++)
			{
				if ((double)Mathf.Abs(rpgCharacterInputController.inputAimHorizontal) > 0.8 || (double)Mathf.Abs(rpgCharacterInputController.inputAimVertical) < -0.8)
				{
					Vector3 normalized = new Vector3(rpgCharacterInputController.inputAimHorizontal, 0f, 0f - rpgCharacterInputController.inputAimVertical).normalized;
					Quaternion rotation = Quaternion.LookRotation(normalized);
					base.transform.rotation = rotation;
				}
			}
			if (Input.GetJoystickNames().Length == 0)
			{
				Plane plane = new Plane(Vector3.up, base.transform.position);
				Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
				Vector3 vector = new Vector3(0f, 0f, 0f);
				float enter = 0f;
				if (plane.Raycast(ray, out enter))
				{
					vector = ray.GetPoint(enter);
				}
				vector = new Vector3(vector.x, base.transform.position.y, vector.z);
				RotateTowardsTarget(vector);
			}
			animator.SetFloat("Velocity X", base.transform.InverseTransformDirection(currentVelocity).x);
			animator.SetFloat("Velocity Z", base.transform.InverseTransformDirection(currentVelocity).z);
		}

		private void Strafing(Vector3 targetPosition)
		{
			animator.SetFloat("Velocity X", base.transform.InverseTransformDirection(currentVelocity).x);
			animator.SetFloat("Velocity Z", base.transform.InverseTransformDirection(currentVelocity).z);
			RotateTowardsTarget(targetPosition);
		}

		public IEnumerator _Knockback(Vector3 knockDirection, int knockBackAmount, int variableAmount)
		{
			isKnockback = true;
			StartCoroutine(_KnockbackForce(knockDirection, knockBackAmount, variableAmount));
			yield return new WaitForSeconds(0.1f);
			isKnockback = false;
		}

		private IEnumerator _KnockbackForce(Vector3 knockDirection, int knockBackAmount, int variableAmount)
		{
			while (isKnockback)
			{
				rb.AddForce(knockDirection * ((float)(knockBackAmount + Random.Range(-variableAmount, variableAmount)) * (knockbackMultiplier * 10f)), ForceMode.Impulse);
				yield return null;
			}
		}

		public void LockMovement()
		{
			canMove = false;
			animator.SetBool("Moving", value: false);
			animator.applyRootMotion = true;
			currentVelocity = new Vector3(0f, 0f, 0f);
		}

		public void UnlockMovement()
		{
			canMove = true;
			animator.applyRootMotion = false;
		}
	}
}
