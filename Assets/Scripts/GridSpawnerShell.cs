using UnityEngine;

[AddComponentMenu("Spawn/Grid Spawner Shell")]
public class GridSpawnerShell : MonoBehaviour
{
	[Header("Required GameObject variables")]
	[Tooltip("Object to spawn outside of grid")]
	public GameObject shellObject;

	[Tooltip("Object to spawn inside of grid")]
	public GameObject interiorObject;

	[Header("Tweak values")]
	[Tooltip("Number of objects in the X direction")]
	public int numObjectsX = 1;

	[Tooltip("Number of objects in the Y direction")]
	public int numObjectsY = 1;

	[Tooltip("Number of objects in the Z direction")]
	public int numObjectsZ = 1;

	[Space(10f)]
	public Vector3 objectSpacing = Vector3.one;

	private void Start()
	{
		for (int i = 0; i < numObjectsX; i++)
		{
			for (int j = 0; j < numObjectsY; j++)
			{
				for (int k = 0; k < numObjectsZ; k++)
				{
					if (i == 0 || j == 0 || k == 0 || i == numObjectsX - 1 || j == numObjectsY - 1 || k == numObjectsZ - 1)
					{
						Object.Instantiate(shellObject, base.transform.position + base.transform.right * i * objectSpacing.x + base.transform.up * j * objectSpacing.y + base.transform.forward * k * objectSpacing.z, Quaternion.identity);
					}
					else
					{
						Object.Instantiate(interiorObject, base.transform.position + base.transform.right * i * objectSpacing.x + base.transform.up * j * objectSpacing.y + base.transform.forward * k * objectSpacing.z, Quaternion.identity);
					}
				}
			}
		}
	}

	private void Update()
	{
	}
}
