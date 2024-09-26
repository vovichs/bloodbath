using UnityEngine;
using UnityEngine.UI;

public class Agreement : MonoBehaviour
{
	private int Agree;

	public GameObject Intro;

	public string NameTag;

	public InputField NameField;

	public InputField PasswordField;

	public GameObject ConfirmButton;

	public GameObject LoginFelixFilip;

	public Text Welcome;

	public GameObject VIP;

	public void Start()
	{
		NameTag = PlayerPrefs.GetString("Name");
		Agree = PlayerPrefs.GetInt("License");
		Welcome.text = NameTag;
		if (NameTag == "")
		{
			Agree = 0;
		}
		if (NameTag == "felixfilip")
		{
			VIP.SetActive(value: true);
		}
		if (Agree == 1)
		{
			Intro.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		}
	}

	public void Agreed()
	{
		Agree = 1;
		PlayerPrefs.SetInt("License", Agree);
		Start();
	}

	public void UpdateName()
	{
		if (NameField.text != "")
		{
			NameTag = NameField.text;
			PlayerPrefs.SetString("Name", NameTag);
			Agreed();
		}
	}

	public void LoginFelix()
	{
		if (PasswordField.text == "18k")
		{
			NameTag = NameField.text;
			PlayerPrefs.SetString("Name", NameTag);
			Agreed();
		}
	}

	public void Update()
	{
		if (NameField.text != NameField.text.ToLower())
		{
			NameField.text = NameField.text.ToLower();
		}
		if (NameField.text == "felixfilip")
		{
			ConfirmButton.SetActive(value: false);
			LoginFelixFilip.SetActive(value: true);
		}
		else if (NameField.text == "f2games")
		{
			ConfirmButton.SetActive(value: false);
			LoginFelixFilip.SetActive(value: true);
		}
		else if (NameField.text != "")
		{
			ConfirmButton.SetActive(value: true);
			LoginFelixFilip.SetActive(value: false);
		}
		else
		{
			ConfirmButton.SetActive(value: false);
			LoginFelixFilip.SetActive(value: false);
		}
	}
}
