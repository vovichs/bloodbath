using UnityEngine;

public class SlowMotion : MonoBehaviour
{
	private float currentAmount;

	private float maxAmount = 5f;

	public GameObject SlowMoSprite;

	private void Start()
	{
		SlowMoSprite.SetActive(value: false);
		Time.timeScale = 1f;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown("z"))
		{
			if (Time.timeScale == 1f)
			{
				SlowMoSprite.SetActive(value: true);
				Time.timeScale = 0.3f;
			}
			else
			{
				SlowMoSprite.SetActive(value: false);
				Time.timeScale = 1f;
			}
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
		if (Time.timeScale == 0.03f)
		{
			currentAmount += Time.deltaTime;
		}
		if (currentAmount > maxAmount)
		{
			currentAmount = 0f;
			Time.timeScale = 1f;
		}
	}
}
