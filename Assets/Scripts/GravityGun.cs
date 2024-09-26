using UnityEngine;

public class GravityGun : MonoBehaviour
{
	public string fireButton = "Fire1";

	public float grabDistance = 10f;

	public Transform holdPosition;

	public float throwForce = 100f;

	public ForceMode throwForceMode;

	public LayerMask layerMask = -1;

	private GameObject heldObject;

	private void Start()
	{
	}

	private void Update()
	{
		if (heldObject == null)
		{
			if (Input.GetButtonDown(fireButton) && Physics.Raycast(base.transform.position, base.transform.forward, out RaycastHit hitInfo, grabDistance, layerMask))
			{
				heldObject = hitInfo.collider.gameObject;
				heldObject.GetComponent<Rigidbody>().isKinematic = true;
				heldObject.GetComponent<Collider>().enabled = false;
			}
			return;
		}
		heldObject.transform.position = holdPosition.position;
		heldObject.transform.rotation = holdPosition.rotation;
		if (Input.GetButtonDown(fireButton))
		{
			Rigidbody component = heldObject.GetComponent<Rigidbody>();
			component.isKinematic = false;
			heldObject.GetComponent<Collider>().enabled = true;
			component.AddForce(throwForce * base.transform.forward, throwForceMode);
			heldObject = null;
		}
	}
}
