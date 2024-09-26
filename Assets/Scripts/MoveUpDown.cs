using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position += base.transform.up * UnityEngine.Input.GetAxis("Vertical") * Time.deltaTime;
	}
}
