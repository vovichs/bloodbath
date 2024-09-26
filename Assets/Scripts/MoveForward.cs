using UnityEngine;

[AddComponentMenu("Movement/Move Forward")]
public class MoveForward : MonoBehaviour
{
	public float speed = 1f;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position += base.transform.forward * speed * Time.deltaTime;
	}
}
