using System;
using UnityEngine;
using UnityEngine.Events;

public class RaycastPositionWithCallback : MonoBehaviour
{
	[Serializable]
	public class PositionCallback : UnityEvent<Vector3>
	{
	}

	public PositionCallback callback;

	private void Start()
	{
	}

	private void Update()
	{
		if (Physics.Raycast(base.transform.position, base.transform.forward, out RaycastHit hitInfo))
		{
			callback.Invoke(hitInfo.point);
		}
	}
}
