using UnityEngine;

public class RandomlyMoveFunction : MonoBehaviour
{
	public float radius = 1f;

	public void RandomMove()
	{
		base.transform.position += UnityEngine.Random.insideUnitSphere * radius;
	}
}
