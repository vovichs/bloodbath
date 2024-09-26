using UnityEngine;

public class hyperlink : MonoBehaviour
{
	public string Link;

	public void OPEN()
	{
		Application.OpenURL(Link);
	}
}
