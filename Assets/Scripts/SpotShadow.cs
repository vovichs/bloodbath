using UnityEngine;

public class SpotShadow : MonoBehaviour
{
	public GameObject spotShadow;

	public float maxSize = 1f;

	public float maxDistance = 10f;

	public float offset = 0.05f;

	private GameObject spotShadowInstance;

	private void Update()
	{
		if (spotShadowInstance == null)
		{
			spotShadowInstance = UnityEngine.Object.Instantiate(spotShadow);
		}
		if (Physics.Raycast(base.transform.position, Vector3.down, out RaycastHit hitInfo, maxDistance))
		{
			spotShadowInstance.SetActive(value: true);
			spotShadowInstance.transform.position = hitInfo.point + hitInfo.normal * offset;
			spotShadowInstance.transform.rotation = Quaternion.LookRotation(-hitInfo.normal);
			float d = Mathf.Lerp(maxSize, 0f, hitInfo.distance / maxDistance);
			spotShadowInstance.transform.localScale = Vector3.one * d;
		}
		else
		{
			spotShadowInstance.SetActive(value: false);
		}
	}
}
