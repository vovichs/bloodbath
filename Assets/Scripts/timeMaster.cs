using UnityEngine;
using UnityEngine.UI;

public class timeMaster : MonoBehaviour
{
	public float timeScale;

	public int Hour;

	public int Minute;

	public int timeID;

	public float sunRot;

	public Text Clock;

	public bool highQualityUpdate;

	private void Start()
	{
		InvokeRepeating("UpdateTime", timeScale, timeScale);
	}

	private void Update()
	{
	}

	public void UpdateTime()
	{
		if (timeID == 1441)
		{
			timeID = 0;
		}
		else
		{
			timeID++;
		}
		if (Hour == 24 && Minute == 60)
		{
			Hour = 0;
			Minute = 0;
		}
		else if (Minute == 60)
		{
			Hour++;
			Minute = 0;
		}
		else
		{
			Minute++;
		}
		Clock.text = Hour.ToString("00") + ":" + Minute.ToString("00");
		sunRot = timeID / 4;
	}
}
