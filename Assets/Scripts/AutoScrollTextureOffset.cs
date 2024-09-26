using System;
using UnityEngine;

public class AutoScrollTextureOffset : MonoBehaviour
{
	public Vector2 scrollSpeed = Vector2.one;

	private void Start()
	{
	}

	private void Update()
	{
		Vector2 vector = GetComponent<Renderer>().material.mainTextureOffset;
		vector += scrollSpeed * Time.deltaTime;
		if (vector.x >= 1f)
		{
			vector.x -= (float)Math.Truncate(vector.x);
		}
		while (vector.x < 0f)
		{
			vector.x += 1f;
		}
		if (vector.y >= 1f)
		{
			vector.y -= (float)Math.Truncate(vector.y);
		}
		while (vector.y < 0f)
		{
			vector.y += 1f;
		}
		GetComponent<Renderer>().material.mainTextureOffset = vector;
	}
}
