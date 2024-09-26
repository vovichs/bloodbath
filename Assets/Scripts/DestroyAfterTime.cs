using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	public float delay = 1f;

	private void Start()
	{
		Invoke("DestroyNow", delay);
	}

	private void DestroyNow()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
