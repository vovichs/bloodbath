using UnityEngine;

[AddComponentMenu("Spawn/Spawn On Button")]
public class SpawnOnButton : MonoBehaviour
{
	public GameObject objectToSpawn;

	public string buttonName = "Fire1";

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetButtonDown(buttonName))
		{
			Object.Instantiate(objectToSpawn, base.transform.position, base.transform.rotation);
		}
	}
}
