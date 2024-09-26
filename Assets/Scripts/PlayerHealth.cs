using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public delegate void HealthEvent();

	public Slider healthBar;

	public float healthAmount = 10f;

	private float currentHealth;

	public static event HealthEvent OnNoHealth;

	private void Start()
	{
		currentHealth = healthAmount;
	}

	private void OnEnable()
	{
		PlayerOxygen.OnNoOxygen += OnNoOxygen;
	}

	private void OnDisable()
	{
		PlayerOxygen.OnNoOxygen -= OnNoOxygen;
	}

	private void OnNoOxygen()
	{
		ReduceHealth(Time.deltaTime);
	}

	private void OnTriggerEnter()
	{
		ReduceHealth(1f);
	}

	private void ReduceHealth(float amount)
	{
		currentHealth -= amount;
		healthBar.value = currentHealth / healthAmount;
		if (currentHealth <= 0f)
		{
			currentHealth = 0f;
			if (PlayerHealth.OnNoHealth != null)
			{
				PlayerHealth.OnNoHealth();
			}
		}
	}
}
