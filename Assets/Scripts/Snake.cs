using UnityEngine;

public class Snake : MonoBehaviour
{
	public GameObject snakeBit;

	public float spawnTime = 0.5f;

	private void Start()
	{
		Invoke("MoveSnake", spawnTime);
	}

	private void MoveSnake()
	{
		Object.Instantiate(snakeBit, base.transform.position, base.transform.rotation);
		switch (UnityEngine.Random.Range(0, 3))
		{
		case 1:
			base.transform.Rotate(new Vector3(0f, 90f, 0f));
			break;
		case 2:
			base.transform.Rotate(new Vector3(0f, -90f, 0f));
			break;
		}
		base.transform.position += base.transform.forward;
		Invoke("MoveSnake", spawnTime);
	}

	private void Update()
	{
	}
}
