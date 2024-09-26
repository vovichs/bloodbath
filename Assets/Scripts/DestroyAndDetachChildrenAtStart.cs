using UnityEngine;

public class DestroyAndDetachChildrenAtStart : MonoBehaviour
{
	private void Start()
	{
		base.transform.DetachChildren();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
