using UnityEngine;

public class SetAspectRatio : MonoBehaviour
{
	public float aspectRatio = 1f;

	public bool realtimeUpdate;

	private Camera camera;

	private void Start()
	{
		camera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (realtimeUpdate)
		{
			camera.aspect = aspectRatio;
		}
	}
}
