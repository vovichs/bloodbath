using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
	public MoneySystem moneySys;

	public bool containsWeapons;

	public string[] containedWeaponNames;

	public int minLoot;

	public int maxLoot;

	public int maxBullets;

	private int chosenLoot;

	private int RanNum2;

	private int AmmoNum;

	public Sprite[] SyncedWithWeaponNames;

	public Image Showcase;

	public Text ShowcaseText;

	public GameObject ShowcaseHost;

	public void Buy(float price)
	{
		moneySys.money -= price;
		PlayerPrefs.SetFloat("Money", moneySys.money);
		chosenLoot = UnityEngine.Random.Range(minLoot, maxLoot + 1);
		UpdateLoot();
	}

	public void UpdateLoot()
	{
		if (chosenLoot != 0)
		{
			chosenLoot--;
			StartCoroutine(ChooseLoot());
		}
	}

	private IEnumerator ChooseLoot()
	{
		int num = UnityEngine.Random.Range(0, 5);
		if (containsWeapons)
		{
			if (num == 3)
			{
				RanNum2 = UnityEngine.Random.Range(0, containedWeaponNames.Length);
				UnityEngine.Debug.Log("Got Weapon " + containedWeaponNames[RanNum2]);
				ShowcaseText.text = "Got Weapon " + containedWeaponNames[RanNum2];
			}
			else
			{
				RanNum2 = UnityEngine.Random.Range(1, containedWeaponNames.Length);
				AmmoNum = UnityEngine.Random.Range(1, maxBullets + 1);
				UnityEngine.Debug.Log("Got " + AmmoNum + "Bullets for " + containedWeaponNames[RanNum2]);
				ShowcaseText.text = "Got " + AmmoNum + " Bullets for " + containedWeaponNames[RanNum2];
				int @int = PlayerPrefs.GetInt(containedWeaponNames[RanNum2] + "Ammo");
				@int += AmmoNum;
				PlayerPrefs.SetInt(containedWeaponNames[RanNum2] + "Ammo", @int);
			}
		}
		else
		{
			RanNum2 = UnityEngine.Random.Range(0, containedWeaponNames.Length);
			AmmoNum = UnityEngine.Random.Range(1, maxBullets + 1);
			UnityEngine.Debug.Log("Got " + AmmoNum + "Bullets for " + containedWeaponNames[RanNum2]);
			ShowcaseText.text = "Got " + AmmoNum + " Bullets for " + containedWeaponNames[RanNum2];
			int @int = PlayerPrefs.GetInt(containedWeaponNames[RanNum2] + "Ammo");
			@int += AmmoNum;
			PlayerPrefs.SetInt(containedWeaponNames[RanNum2] + "Ammo", @int);
		}
		Showcase.sprite = SyncedWithWeaponNames[RanNum2];
		ShowcaseHost.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		ShowcaseHost.SetActive(value: false);
		UpdateLoot();
	}
}
