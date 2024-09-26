using UnityEngine;
using UnityEngine.UI;

public class accountloader : MonoBehaviour
{
	public Text nameTag;

	public bool VIP;

	public GameObject VIPsign;

	public void Start()
	{
		nameTag.text = PlayerPrefs.GetString("Name");
		if (nameTag.text == "felixfilip")
		{
			VIP = true;
			VIPsign.SetActive(value: true);
		}
	}

	public void clearData()
	{
		PlayerPrefs.DeleteAll();
	}
}
