using UnityEngine;

public class RenderBallisticPath : MonoBehaviour
{
	public GameObject explosionDisplay;

	public float initialVelocity = 10f;

	public float timeResolution = 0.02f;

	public float maxTime = 10f;

	public LayerMask layerMask = -1;

	private GameObject explosionDisplayInstance;

	private LineRenderer lineRenderer;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		Vector3 vector = base.transform.forward * initialVelocity;
		lineRenderer.SetVertexCount((int)(maxTime / timeResolution));
		int num = 0;
		Vector3 vector2 = base.transform.position;
		float num2 = 0f;
		RaycastHit hitInfo;
		while (true)
		{
			if (num2 < maxTime)
			{
				lineRenderer.SetPosition(num, vector2);
				if (Physics.Raycast(vector2, vector, out hitInfo, vector.magnitude * timeResolution, layerMask))
				{
					break;
				}
				if (explosionDisplayInstance != null)
				{
					explosionDisplayInstance.SetActive(value: false);
				}
				vector2 += vector * timeResolution;
				vector += Physics.gravity * timeResolution;
				num++;
				num2 += timeResolution;
				continue;
			}
			return;
		}
		lineRenderer.SetVertexCount(num + 2);
		lineRenderer.SetPosition(num + 1, hitInfo.point);
		if (explosionDisplay != null)
		{
			if (explosionDisplayInstance != null)
			{
				explosionDisplayInstance.SetActive(value: true);
				explosionDisplayInstance.transform.position = hitInfo.point;
			}
			else
			{
				explosionDisplayInstance = UnityEngine.Object.Instantiate(explosionDisplay, hitInfo.point, Quaternion.identity);
				explosionDisplayInstance.SetActive(value: true);
			}
		}
	}
}
