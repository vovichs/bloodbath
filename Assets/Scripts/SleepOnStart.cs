using UnityEngine;

public class SleepOnStart : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Rigidbody>().Sleep();
	}
}
