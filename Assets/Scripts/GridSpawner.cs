using UnityEngine;

[AddComponentMenu("Spawn/Grid Spawner")]
public class GridSpawner : MonoBehaviour
{
	public GameObject objectToSpawn;

	public int numObjectsX = 1;

	public int numObjectsY = 1;

	public int numObjectsZ = 1;

	public Vector3 objectSpacing = Vector3.one;

	[ContextMenu("SpawnCubesNow")]
	private void Start()
	{
		for (int i = 0; i < numObjectsX; i++)
		{
			for (int j = 0; j < numObjectsY; j++)
			{
				for (int k = 0; k < numObjectsZ; k++)
				{
					Object.Instantiate(objectToSpawn, base.transform.position + base.transform.right * i * objectSpacing.x + base.transform.up * j * objectSpacing.y + base.transform.forward * k * objectSpacing.z, Quaternion.identity);
				}
			}
		}
	}

	private void Update()
	{
	}
}
