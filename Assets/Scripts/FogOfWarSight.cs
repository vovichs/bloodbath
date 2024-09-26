using UnityEngine;

public class FogOfWarSight : MonoBehaviour
{
	public float radius = 10f;

	public LayerMask layerMask = -1;

	private void Start()
	{
	}

	private void Update()
	{
		Collider[] array = Physics.OverlapSphere(base.transform.position, radius, layerMask);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SendMessage("Observed", SendMessageOptions.DontRequireReceiver);
		}
	}
}
