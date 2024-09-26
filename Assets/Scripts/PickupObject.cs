using UnityEngine;

public class PickupObject : MonoBehaviour
{
	public GameObject mainCamera;

	private bool carrying;

	private GameObject carriedObject;

	public float distance;

	public float smooth;

	private void Update()
	{
		if (carrying)
		{
			carry(carriedObject);
			checkDrop();
		}
		else
		{
			pickup();
		}
	}

	private void rotateObject()
	{
		carriedObject.transform.Rotate(5f, 10f, 15f);
	}

	private void carry(GameObject o)
	{
		o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
		o.transform.rotation = Quaternion.identity;
	}

	private void pickup()
	{
		if (!Input.GetKeyDown(KeyCode.E))
		{
			return;
		}
		int num = Screen.width / 2;
		int num2 = Screen.height / 2;
		if (Physics.Raycast(mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(num, num2)), out RaycastHit hitInfo))
		{
			Pickupable component = hitInfo.collider.GetComponent<Pickupable>();
			if (component != null)
			{
				carrying = true;
				carriedObject = component.gameObject;
				component.gameObject.GetComponent<Rigidbody>().useGravity = false;
			}
		}
	}

	private void checkDrop()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.E))
		{
			dropObject();
		}
	}

	private void dropObject()
	{
		carrying = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
		carriedObject = null;
	}
}
