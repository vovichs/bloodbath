using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
	public Vector3 v3Offset;

	public GameObject goFollow;

	public float speed = 3f;

	public bool Aiming;

	private void Start()
	{
		v3Offset = base.transform.position - goFollow.transform.position;
	}

	private void Update()
	{
		base.transform.position = goFollow.transform.position + v3Offset;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
	}

	public void Shoot()
	{
		if (!Aiming)
		{
			base.transform.Rotate(-4f, 0f, 0f);
		}
		else
		{
			base.transform.Rotate(-1f, 0f, 0f);
		}
	}
}
