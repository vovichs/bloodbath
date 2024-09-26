using UnityEngine;

public class Interact : MonoBehaviour
{
	public static Interact Instance;

	public float distance = 1f;

	public LayerMask layerMask = -1;

	public string buttonName = "Fire1";

	public Behaviour[] controlsToDisableDuringInteract;

	public float reinteractDelay = 0.1f;

	private bool displayInteract;

	private bool interactDisabled;

	private void Start()
	{
		Instance = this;
	}

	private void Update()
	{
		if (Physics.Raycast(base.transform.position, base.transform.forward, out RaycastHit hitInfo, distance, layerMask))
		{
			if (!interactDisabled)
			{
				displayInteract = true;
				if (Input.GetButtonDown(buttonName))
				{
					hitInfo.collider.SendMessageUpwards("InteractEvent");
				}
			}
			else
			{
				displayInteract = false;
			}
		}
		else
		{
			displayInteract = false;
		}
	}

	public static void DisableControl()
	{
		Behaviour[] array = Instance.controlsToDisableDuringInteract;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		Instance.interactDisabled = true;
	}

	public static void EnableControl()
	{
		Behaviour[] array = Instance.controlsToDisableDuringInteract;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
		Instance.Invoke("InteractDisabledFalse", Instance.reinteractDelay);
	}

	private void InteractDisabledFalse()
	{
		Instance.interactDisabled = false;
	}

	private void OnGUI()
	{
		if (displayInteract)
		{
			GUILayout.Label("Press Fire1 to Interact");
		}
	}
}
