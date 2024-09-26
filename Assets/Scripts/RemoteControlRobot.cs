using UnityEngine;

public class RemoteControlRobot : MonoBehaviour
{
	public float speed = 5f;

	public float rotationSpeed = 60f;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position += base.transform.forward * UnityEngine.Input.GetAxis("Vertical") * speed * Time.deltaTime;
		base.transform.Rotate(0f, rotationSpeed * UnityEngine.Input.GetAxis("Horizontal") * Time.deltaTime, 0f);
	}
}
