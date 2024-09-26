using System;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
	public Transform hourHand;

	public Transform minuteHand;

	private void Update()
	{
		DateTime now = DateTime.Now;
		Vector3 localEulerAngles = hourHand.localEulerAngles;
		localEulerAngles.z = 30f * (float)now.Hour + 0.5f * (float)now.Minute;
		hourHand.localEulerAngles = localEulerAngles;
		localEulerAngles = minuteHand.localEulerAngles;
		localEulerAngles.z = 6f * (float)now.Minute;
		minuteHand.localEulerAngles = localEulerAngles;
	}
}
