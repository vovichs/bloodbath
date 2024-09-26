using UnityEngine;

public class ActivateOnStart : MonoBehaviour
{
	public GameObject Object1;

	public GameObject Object2;

	public Collider col;

	public void GameStart()
	{
		col.enabled = false;
		Object1.SetActive(value: true);
		Object2.SetActive(value: true);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
