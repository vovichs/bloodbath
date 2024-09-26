using UnityEngine;
using UnityEngine.UI;

public class NpcAddonTemplate : MonoBehaviour
{
	public Character Host;

	public Image HealthBar;

	public Image PainBar;

	public Image BloodBar;

	private float PainMax = 250f;

	public void Update()
	{
		HealthBar.fillAmount = Host.health / 100f;
		PainBar.fillAmount = Host.pain / PainMax;
		BloodBar.fillAmount = (Host.bloodInLiters - 2.5f) / 2.5f;
	}
}
