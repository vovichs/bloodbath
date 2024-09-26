using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour
{
	public delegate void OxygenEvent();

	public Slider oxygenBar;

	public float oxygenAmount = 10f;

	private float currentOxygen;

	private bool isUnderwater;

	public static event OxygenEvent OnNoOxygen;

	private void Start()
	{
		currentOxygen = oxygenAmount;
	}

	private void OnTriggerStay()
	{
		isUnderwater = false;
		currentOxygen -= Time.deltaTime;
		oxygenBar.value = currentOxygen / oxygenAmount;
		if (currentOxygen <= 0f)
		{
			currentOxygen = 0f;
			if (PlayerOxygen.OnNoOxygen != null)
			{
				PlayerOxygen.OnNoOxygen();
			}
		}
	}

	private void Update()
	{
		if (!isUnderwater)
		{
			UnityEngine.Debug.Log("Not in water!");
			currentOxygen += Time.deltaTime;
			if (currentOxygen >= oxygenAmount)
			{
				currentOxygen = oxygenAmount;
			}
			oxygenBar.value = currentOxygen / oxygenAmount;
		}
		isUnderwater = false;
	}
}
