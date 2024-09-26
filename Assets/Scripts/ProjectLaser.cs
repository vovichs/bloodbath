using UnityEngine;

public class ProjectLaser : MonoBehaviour
{
	public GameObject laser;

	public float offset = 0.1f;

	private GameObject laserInstance;

	private void Update()
	{
		if (Physics.Raycast(base.transform.position, base.transform.forward, out RaycastHit hitInfo))
		{
			if (laserInstance == null)
			{
				laserInstance = UnityEngine.Object.Instantiate(laser, hitInfo.point + hitInfo.normal * offset, Quaternion.identity);
				return;
			}
			laserInstance.SetActive(value: true);
			laserInstance.transform.position = hitInfo.point + hitInfo.normal * offset;
		}
		else if (laserInstance != null)
		{
			laserInstance.SetActive(value: false);
		}
	}
}
