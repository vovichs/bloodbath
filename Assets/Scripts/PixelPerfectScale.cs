using UnityEngine;

[ExecuteInEditMode]
public class PixelPerfectScale : MonoBehaviour
{
	public int screenVerticalPixels = 256;

	public bool preferUncropped = true;

	private float screenPixelsY;

	private bool currentCropped;

	private void Update()
	{
		if (screenPixelsY != (float)Screen.height || currentCropped != preferUncropped)
		{
			screenPixelsY = Screen.height;
			currentCropped = preferUncropped;
			float num = screenPixelsY / (float)screenVerticalPixels;
			float d = (!preferUncropped) ? (Mathf.Ceil(num) / num) : (Mathf.Floor(num) / num);
			base.transform.localScale = Vector3.one * d;
		}
	}
}
