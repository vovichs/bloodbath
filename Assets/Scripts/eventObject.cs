using UnityEngine;

public class eventObject : MonoBehaviour
{
	public void DisconnectFromParent()
	{
		base.gameObject.transform.parent = null;
	}

	public void ActivateRigidbody()
	{
		base.gameObject.GetComponent<Rigidbody>().isKinematic = false;
	}

	public void DeactivateRigidbody()
	{
		base.gameObject.GetComponent<Rigidbody>().isKinematic = true;
	}
}
