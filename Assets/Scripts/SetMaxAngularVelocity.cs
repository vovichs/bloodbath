using UnityEngine;

public class SetMaxAngularVelocity : MonoBehaviour
{
	public float maxAngularVelocity = 7f;

	private void Start()
	{
		GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVelocity;
	}
}
