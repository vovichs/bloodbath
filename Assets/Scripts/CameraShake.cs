using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 originalPos = base.transform.localPosition;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			float x = UnityEngine.Random.Range(-1f, 1f) * (magnitude / 2f);
			float z = UnityEngine.Random.Range(-1f, 1f) * magnitude;
			base.transform.localPosition = new Vector3(x, originalPos.y, z);
			elapsed += Time.fixedDeltaTime;
			yield return null;
		}
		base.transform.localPosition = originalPos;
	}
}
