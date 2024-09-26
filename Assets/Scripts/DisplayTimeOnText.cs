using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTimeOnText : MonoBehaviour
{
	private Text clockText;

	private void Start()
	{
		clockText = GetComponent<Text>();
	}

	private void Update()
	{
		DateTime now = DateTime.Now;
		clockText.text = now.ToString("hh:mm");
	}
}
