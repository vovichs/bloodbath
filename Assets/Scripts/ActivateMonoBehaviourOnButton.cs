using UnityEngine;

public class ActivateMonoBehaviourOnButton : MonoBehaviour
{
	public Behaviour[] componentsToActivate;

	public string buttonName = "Fire1";

	private bool componentsActive;

	private void Update()
	{
		if (Input.GetButton(buttonName))
		{
			Behaviour[] array = componentsToActivate;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
			componentsActive = true;
		}
		else if (componentsActive)
		{
			Behaviour[] array = componentsToActivate;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			componentsActive = false;
		}
	}
}
