using System.Collections.Generic;
using UnityEngine;

public class RecursionTree : MonoBehaviour
{
	public RecursionTree twigReference;

	public float twigLength = 1f;

	public float spreadAngle = 30f;

	public float iterations = 3f;

	public float splits = 3f;

	public float windAngle = 10f;

	public float minWindChangeTime = 1f;

	public float maxWindChangeTime = 5f;

	private List<Transform> childList = new List<Transform>();

	private bool doneParenting;

	private Quaternion originalRotation;

	[HideInInspector]
	public bool root = true;

	private static Quaternion goalRotation;

	private void Start()
	{
		if (iterations >= 1f)
		{
			for (int i = 0; (float)i < splits; i++)
			{
				Quaternion from = Quaternion.LookRotation(base.transform.forward);
				Quaternion rotation = UnityEngine.Random.rotation;
				Quaternion rotation2 = Quaternion.RotateTowards(from, rotation, UnityEngine.Random.Range(0f, spreadAngle));
				RecursionTree recursionTree = UnityEngine.Object.Instantiate(twigReference, base.transform.position + base.transform.forward * twigLength, rotation2);
				recursionTree.iterations = iterations - 1f;
				recursionTree.root = false;
				childList.Add(recursionTree.transform);
			}
		}
		originalRotation = base.transform.rotation;
		if (root)
		{
			WindChange();
			Invoke("WindChange", UnityEngine.Random.Range(minWindChangeTime, maxWindChangeTime));
		}
	}

	private void WindChange()
	{
		goalRotation = UnityEngine.Random.rotation;
		Invoke("WindChange", UnityEngine.Random.Range(minWindChangeTime, maxWindChangeTime));
	}

	private void Update()
	{
		if (!doneParenting)
		{
			foreach (Transform child in childList)
			{
				child.parent = base.transform;
			}
			doneParenting = true;
		}
		else
		{
			Quaternion to = Quaternion.RotateTowards(originalRotation, goalRotation, windAngle);
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, to, UnityEngine.Random.Range(0f, windAngle * Time.deltaTime));
		}
		if (root)
		{
			UnityEngine.Debug.DrawRay(base.transform.position, goalRotation * base.transform.forward);
		}
	}
}
