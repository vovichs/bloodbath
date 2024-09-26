using UnityEngine;

public class rigidChild : MonoBehaviour
{
	public void SetKinematic(bool newValue)
	{
		Rigidbody[] componentsInChildren = GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = newValue;
		}
	}
}
