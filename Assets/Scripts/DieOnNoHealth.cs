using UnityEngine;

public class DieOnNoHealth : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		PlayerHealth.OnNoHealth += OnNoHealth;
	}

	private void OnDisable()
	{
		PlayerHealth.OnNoHealth -= OnNoHealth;
	}

	private void OnNoHealth()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
