using UnityEngine;

public class SpawnBasedOnVisualizationManager : MonoBehaviour
{
	public GameObject objectPrefab;

	public float spawnThreshold = 0.5f;

	public int frequency;

	private void Update()
	{
		if (VisualizationManager.samples[frequency] > spawnThreshold)
		{
			Object.Instantiate(objectPrefab, base.transform.position, base.transform.rotation);
		}
	}
}
