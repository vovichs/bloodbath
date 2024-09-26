using UnityEngine;

public class AIRobot : MonoBehaviour
{
	public float speed = 5f;

	public float rotationSpeed = 60f;

	private float perlinOffset;

	private float t;

	private void Start()
	{
		perlinOffset = UnityEngine.Random.Range(-1000f, 1000f);
	}

	private void Update()
	{
		t += Time.deltaTime;
		base.transform.position += base.transform.forward * speed * Time.deltaTime;
		base.transform.Rotate(0f, (Mathf.PerlinNoise(t, perlinOffset) - 0.5f) * 2f * rotationSpeed * Time.deltaTime, 0f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		base.transform.forward = Vector3.Reflect(base.transform.forward, Vector3.ProjectOnPlane(collision.contacts[0].normal, Vector3.up));
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}

	private void OnCollisionExit()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
}
