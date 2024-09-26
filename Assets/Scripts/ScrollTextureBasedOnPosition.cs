using UnityEngine;

public class ScrollTextureBasedOnPosition : MonoBehaviour
{
	public float scrollSpeedX = 1f;

	public float scrollSpeedY = 1f;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		position.x *= scrollSpeedX;
		position.y *= scrollSpeedY;
		GetComponent<Renderer>().material.mainTextureOffset = position;
	}
}
