using UnityEngine;

public class GibOnCollide : MonoBehaviour
{
	public GameObject gib;

	private void OnCollisionEnter()
	{
		Object.Instantiate(gib, base.transform.position, base.transform.rotation);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
