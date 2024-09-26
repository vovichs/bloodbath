using UnityEngine;

public class ObjectMover : MonoBehaviour
{
	public Vector3 mainDir;

	public Vector3 secondaryDir;

	public float range = 1f;

	public float speed = 1f;

	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void Update()
	{
		Vector3 a = mainDir * Mathf.Sin(Time.time * speed) * range;
		Vector3 b = secondaryDir * Mathf.Cos(Time.time * speed) * range;
		base.transform.position = a + b + startPos;
	}
}
