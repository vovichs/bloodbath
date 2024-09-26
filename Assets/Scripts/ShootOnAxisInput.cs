using UnityEngine;

public class ShootOnAxisInput : MonoBehaviour
{
	public GameObject bullet;

	public string horizontalAxis = "Horizontal";

	public string verticalAxis = "Vertical";

	public float shootDelay = 0.1f;

	private bool canShoot = true;

	private void ResetShot()
	{
		canShoot = true;
	}

	private void Update()
	{
		Vector3 forward = Vector3.right * UnityEngine.Input.GetAxis(horizontalAxis) + Vector3.forward * UnityEngine.Input.GetAxis(verticalAxis);
		if (forward.sqrMagnitude > 0f)
		{
			base.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
			if (canShoot)
			{
				Object.Instantiate(bullet, base.transform.position, base.transform.rotation);
				canShoot = false;
				Invoke("ResetShot", shootDelay);
			}
		}
	}
}
