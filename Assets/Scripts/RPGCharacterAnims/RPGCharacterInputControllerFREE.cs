using UnityEngine;

namespace RPGCharacterAnims
{
	public class RPGCharacterInputControllerFREE : MonoBehaviour
	{
		[HideInInspector]
		public bool inputJump;

		[HideInInspector]
		public bool inputLightHit;

		[HideInInspector]
		public bool inputDeath;

		[HideInInspector]
		public bool inputAttackL;

		[HideInInspector]
		public bool inputAttackR;

		[HideInInspector]
		public bool inputSwitchUpDown;

		[HideInInspector]
		public bool inputStrafe;

		[HideInInspector]
		public float inputAimVertical;

		[HideInInspector]
		public float inputAimHorizontal;

		[HideInInspector]
		public float inputHorizontal;

		[HideInInspector]
		public float inputVertical;

		[HideInInspector]
		public bool inputRoll;

		[HideInInspector]
		public bool allowedInput = true;

		[HideInInspector]
		public Vector3 moveInput;

		[HideInInspector]
		public Vector2 aimInput;

		private void Inputs()
		{
			inputJump = Input.GetButtonDown("Jump");
			inputLightHit = Input.GetButtonDown("LightHit");
			inputDeath = Input.GetButtonDown("Death");
			inputAttackL = Input.GetButtonDown("AttackL");
			inputAttackR = Input.GetButtonDown("AttackR");
			inputSwitchUpDown = Input.GetButtonDown("SwitchUpDown");
			inputStrafe = UnityEngine.Input.GetKey(KeyCode.LeftShift);
			inputAimVertical = UnityEngine.Input.GetAxisRaw("AimVertical");
			inputAimHorizontal = UnityEngine.Input.GetAxisRaw("AimHorizontal");
			inputHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
			inputVertical = UnityEngine.Input.GetAxisRaw("Vertical");
			inputRoll = Input.GetButtonDown("L3");
		}

		private void Awake()
		{
			allowedInput = true;
		}

		private void Update()
		{
			Inputs();
			moveInput = CameraRelativeInput(inputHorizontal, inputVertical);
			aimInput = new Vector2(inputAimHorizontal, inputAimVertical);
		}

		private Vector3 CameraRelativeInput(float inputX, float inputZ)
		{
			Vector3 vector = Camera.main.transform.TransformDirection(Vector3.forward);
			vector.y = 0f;
			vector = vector.normalized;
			Vector3 a = new Vector3(vector.z, 0f, 0f - vector.x);
			Vector3 result = inputHorizontal * a + inputVertical * vector;
			if (result.magnitude > 1f)
			{
				result.Normalize();
			}
			return result;
		}

		public bool HasAnyInput()
		{
			if (allowedInput && moveInput != Vector3.zero && aimInput != Vector2.zero && inputJump)
			{
				return true;
			}
			return false;
		}

		public bool HasMoveInput()
		{
			if (allowedInput && moveInput != Vector3.zero)
			{
				return true;
			}
			return false;
		}

		public bool HasAimInput()
		{
			if ((allowedInput && (aimInput.x < -0.8f || aimInput.x > 0.8f)) || aimInput.y < -0.8f || aimInput.y > 0.8f)
			{
				return true;
			}
			return false;
		}
	}
}
