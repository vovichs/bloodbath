using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
	public GameObject objectToSpawn;

	public float delay = 1f;

	private void Start()
	{
		Invoke("Spawn", delay);
	}

	private void Spawn()
	{
		Object.Instantiate(objectToSpawn, base.transform.position, Quaternion.identity);
		Invoke("Spawn", delay);
	}
}
