using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessageBoxOnInteract : MonoBehaviour
{
	public GameObject messageBox;

	public Text messageBoxText;

	public float characterDelay = 0.1f;

	public bool messageBoxEnabled;

	public string message;

	private void InteractEvent()
	{
		messageBox.SetActive(value: true);
		StopAllCoroutines();
		StartCoroutine(TypeMessage());
		Interact.DisableControl();
		messageBoxEnabled = true;
	}

	private IEnumerator TypeMessage()
	{
		messageBoxText.text = "";
		string text = message;
		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			yield return new WaitForSeconds(characterDelay);
			messageBoxText.text += c.ToString();
			if (messageBox.GetComponent<AudioSource>() != null)
			{
				messageBox.GetComponent<AudioSource>().Play();
			}
		}
	}

	private void Update()
	{
		if (messageBoxEnabled && Input.GetButtonDown("Fire1"))
		{
			StopAllCoroutines();
			messageBox.SetActive(value: false);
			Interact.EnableControl();
			messageBoxEnabled = false;
		}
	}
}
