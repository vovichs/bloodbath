using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
	public float money;

	public Character character;

	public Text moneyText;

	public Text addText;

	public bool showcase;

	private bool died;

	private void Start()
	{
		if (!showcase)
		{
			InvokeRepeating("MoneyUpdate", 1f, 1f);
		}
		else
		{
			addText.text = "";
		}
		money = PlayerPrefs.GetFloat("Money");
	}

	public void MoneyUpdate()
	{
		money += character.pain / 20f;
		addText.text = "+" + (character.pain / 10f).ToString("000") + "$";
		if (!died && character.dead)
		{
			died = true;
			money += 10f;
			addText.text = 10 + "$";
		}
		PlayerPrefs.SetFloat("Money", money);
	}

	private void Update()
	{
		moneyText.text = money.ToString("000000") + "$";
	}
}
