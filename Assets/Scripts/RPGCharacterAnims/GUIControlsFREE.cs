using UnityEngine;

namespace RPGCharacterAnims
{
	public class GUIControlsFREE : MonoBehaviour
	{
		private RPGCharacterControllerFREE rpgCharacterController;

		private RPGCharacterMovementControllerFREE rpgCharacterMovementController;

		public bool useNavAgent;

		private void Awake()
		{
			rpgCharacterController = GetComponent<RPGCharacterControllerFREE>();
			rpgCharacterMovementController = GetComponent<RPGCharacterMovementControllerFREE>();
		}

		private void OnGUI()
		{
			if (!rpgCharacterController.isDead)
			{
				if (!rpgCharacterController.canAction)
				{
					return;
				}
				if (rpgCharacterMovementController.MaintainingGround())
				{
					useNavAgent = GUI.Toggle(new Rect(500f, 15f, 100f, 30f), useNavAgent, "Use NavAgent");
					if (useNavAgent && rpgCharacterMovementController.navMeshAgent != null)
					{
						rpgCharacterMovementController.useMeshNav = true;
						rpgCharacterMovementController.navMeshAgent.enabled = true;
					}
					else
					{
						rpgCharacterMovementController.useMeshNav = false;
						rpgCharacterMovementController.navMeshAgent.enabled = false;
					}
					if (GUI.Button(new Rect(25f, 15f, 100f, 30f), "Roll Forward"))
					{
						StartCoroutine(rpgCharacterMovementController._Roll(1));
					}
					if (GUI.Button(new Rect(130f, 15f, 100f, 30f), "Roll Backward"))
					{
						StartCoroutine(rpgCharacterMovementController._Roll(3));
					}
					if (GUI.Button(new Rect(25f, 45f, 100f, 30f), "Roll Left"))
					{
						StartCoroutine(rpgCharacterMovementController._Roll(4));
					}
					if (GUI.Button(new Rect(130f, 45f, 100f, 30f), "Roll Right"))
					{
						StartCoroutine(rpgCharacterMovementController._Roll(2));
					}
					if (GUI.Button(new Rect(340f, 15f, 100f, 30f), "Turn Left"))
					{
						StartCoroutine(rpgCharacterController._Turning(1));
					}
					if (GUI.Button(new Rect(340f, 45f, 100f, 30f), "Turn Right"))
					{
						StartCoroutine(rpgCharacterController._Turning(2));
					}
					if (GUI.Button(new Rect(25f, 85f, 100f, 30f), "Attack L"))
					{
						rpgCharacterController.Attack(1);
					}
					if (GUI.Button(new Rect(130f, 85f, 100f, 30f), "Attack R"))
					{
						rpgCharacterController.Attack(2);
					}
					if (GUI.Button(new Rect(25f, 115f, 100f, 30f), "Left Kick"))
					{
						rpgCharacterController.AttackKick(1);
					}
					if (GUI.Button(new Rect(130f, 115f, 100f, 30f), "Right Kick"))
					{
						rpgCharacterController.AttackKick(3);
					}
					if (GUI.Button(new Rect(30f, 240f, 100f, 30f), "Get Hit"))
					{
						rpgCharacterController.GetHit();
					}
				}
				if ((rpgCharacterMovementController.canJump || rpgCharacterMovementController.canDoubleJump) && rpgCharacterController.canAction)
				{
					if (rpgCharacterMovementController.MaintainingGround() && GUI.Button(new Rect(25f, 175f, 100f, 30f), "Jump") && rpgCharacterMovementController.canJump)
					{
						rpgCharacterMovementController.currentState = RPGCharacterStateFREE.Jump;
						rpgCharacterMovementController.rpgCharacterState = RPGCharacterStateFREE.Jump;
					}
					if (rpgCharacterMovementController.canDoubleJump && GUI.Button(new Rect(25f, 175f, 100f, 30f), "Jump Flip"))
					{
						rpgCharacterMovementController.currentState = RPGCharacterStateFREE.DoubleJump;
						rpgCharacterMovementController.rpgCharacterState = RPGCharacterStateFREE.DoubleJump;
					}
				}
				if (rpgCharacterMovementController.MaintainingGround() && rpgCharacterController.canAction && GUI.Button(new Rect(30f, 270f, 100f, 30f), "Death"))
				{
					rpgCharacterController.Death();
				}
			}
			else if (GUI.Button(new Rect(30f, 270f, 100f, 30f), "Revive"))
			{
				rpgCharacterController.Revive();
			}
		}
	}
}
