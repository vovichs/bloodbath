using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public Image bloodIMG;

	public Color color;

	private void Start()
	{
	}

	private void Update()
	{
		bloodIMG.color = color;
	}

	public void TakeDamage(float amount)
	{
		Color color = bloodIMG.color;
		bloodIMG.color = color;
		color.a += amount;
		bloodIMG.color = color;
	}
}
