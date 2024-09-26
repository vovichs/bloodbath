using UnityEngine;

public class RandomlyPlaceObjectsOnSurface : MonoBehaviour
{
	public GameObject[] objectsToSpawn;

	public float spawnRadius = 10f;

	public int numberOfObjects = 10;

	public bool randomOrientation;

	public bool orientToSurface;

	private void Start()
	{
		for (int i = 0; i < numberOfObjects; i++)
		{
			GameObject gameObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
			Vector2 vector = UnityEngine.Random.insideUnitCircle * spawnRadius;
			Vector3 b = new Vector3(vector.x, 0f, vector.y);
			if (Physics.Raycast(base.transform.position + b, Vector3.down, out RaycastHit hitInfo))
			{
				Vector3 point = hitInfo.point;
				Quaternion rotation = randomOrientation ? UnityEngine.Random.rotation : ((!orientToSurface) ? gameObject.transform.rotation : Quaternion.LookRotation(hitInfo.normal));
				Object.Instantiate(gameObject, point, rotation);
			}
		}
	}
}
