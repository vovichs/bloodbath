using UnityEngine;

public class ToggleBehavioursOnInvisible : MonoBehaviour
{
	public Behaviour[] behavioursEnabledOnInvisible;

	public Behaviour[] behavioursDisabledOnInvisible;

	private bool visible;

	private bool raycastable;

	private void Update()
	{
		if (Physics.Raycast(base.transform.position, Camera.main.transform.position - base.transform.position, out RaycastHit hitInfo))
		{
			if (hitInfo.collider.tag == "MainCamera")
			{
				raycastable = true;
			}
			else
			{
				raycastable = false;
			}
		}
		else
		{
			raycastable = false;
		}
		if (raycastable && visible)
		{
			Behaviour[] array = behavioursEnabledOnInvisible;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			array = behavioursDisabledOnInvisible;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
		else
		{
			Behaviour[] array = behavioursEnabledOnInvisible;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
			array = behavioursDisabledOnInvisible;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
	}

	private void OnBecameInvisible()
	{
		visible = false;
	}

	private void OnBecameVisible()
	{
		visible = true;
	}
}
