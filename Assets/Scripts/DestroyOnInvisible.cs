using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
	public GameObject objectToDestroy;

	private void Start()
	{
		if (!objectToDestroy)
		{
			objectToDestroy = base.gameObject;
		}
	}

	private void OnBecameInvisible()
	{
		UnityEngine.Object.Destroy(objectToDestroy);
	}
}
