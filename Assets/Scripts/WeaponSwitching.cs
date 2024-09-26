using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
	public int selectedWeapon;

	public bool Aiming;

	private void Start()
	{
		SelectWeapon();
	}

	private void Update()
	{
		int num = selectedWeapon;
		if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (selectedWeapon >= base.transform.childCount - 1)
			{
				selectedWeapon = 0;
			}
			else
			{
				selectedWeapon++;
			}
		}
		if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (selectedWeapon <= 0)
			{
				selectedWeapon = base.transform.childCount - 1;
			}
			else
			{
				selectedWeapon--;
			}
		}
		if (num != selectedWeapon)
		{
			SelectWeapon();
		}
	}

	private void SelectWeapon()
	{
		int num = 0;
		foreach (Transform item in base.transform)
		{
			if (num == selectedWeapon)
			{
				item.gameObject.SetActive(value: true);
			}
			else
			{
				item.gameObject.SetActive(value: false);
			}
			num++;
		}
	}
}
