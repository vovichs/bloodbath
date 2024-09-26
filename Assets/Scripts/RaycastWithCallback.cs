using System;
using UnityEngine;
using UnityEngine.Events;

public class RaycastWithCallback : MonoBehaviour
{
	[Serializable]
	public class RaycastHitCallback : UnityEvent<RaycastHit>
	{
	}

	public RaycastHitCallback callback;

	private void Start()
	{
	}

	private void Update()
	{
		if (Physics.Raycast(base.transform.position, base.transform.forward, out RaycastHit hitInfo))
		{
			callback.Invoke(hitInfo);
		}
	}
}
