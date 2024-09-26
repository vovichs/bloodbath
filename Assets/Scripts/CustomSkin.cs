using System.Collections;
using UnityEngine;

public class CustomSkin : MonoBehaviour
{
	public string url;

	private IEnumerator Start()
	{
		using (WWW www = new WWW(url))
		{
			yield return www;
			GetComponent<Renderer>().material.mainTexture = www.texture;
		}
	}
}
