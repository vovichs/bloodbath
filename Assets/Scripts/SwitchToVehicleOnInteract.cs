using UnityEngine;

public class SwitchToVehicleOnInteract : MonoBehaviour
{
	public Behaviour[] vehicleBehaviours;

	public Transform exitPoint;

	public string escapeKey = "Fire1";

	private bool activeNow;

	private GameObject player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		Behaviour[] array = vehicleBehaviours;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
	}

	private void Update()
	{
		if (activeNow && Input.GetButtonDown(escapeKey))
		{
			activeNow = false;
			Behaviour[] array = vehicleBehaviours;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			player.transform.position = exitPoint.position;
			player.transform.rotation = exitPoint.rotation;
			player.SetActive(value: true);
		}
	}

	private void InteractEvent()
	{
		if (!activeNow)
		{
			activeNow = true;
			Behaviour[] array = vehicleBehaviours;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
			player.SetActive(value: false);
		}
	}
}
