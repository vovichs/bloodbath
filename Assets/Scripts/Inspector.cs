using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
	public Editor editor;

	public GameObject selectedObject;

	public GameObject selectedSpawnObject;

	public Camera cam;

	public float posX;

	public float posY;

	public float posZ;

	public float rotX;

	public float rotY;

	public float rotZ;

	public float scaleX;

	public float scaleY;

	public float scaleZ;

	public InputField Xpos;

	public InputField Ypos;

	public InputField Zpos;

	public InputField Xrot;

	public InputField Yrot;

	public InputField Zrot;

	public InputField Xscale;

	public InputField Yscale;

	public InputField Zscale;

	private void OnEnable()
	{
		selectedObject = editor.selectedBody;
		posX = selectedObject.transform.localPosition.x;
		posY = selectedObject.transform.localPosition.y;
		posZ = selectedObject.transform.localPosition.z;
		Xpos.text = posX.ToString("0");
		Ypos.text = posY.ToString("0");
		Zpos.text = posZ.ToString("0");
		rotX = selectedObject.transform.localRotation.x;
		rotY = selectedObject.transform.localRotation.y;
		rotZ = selectedObject.transform.localRotation.z;
		Xrot.text = rotX.ToString("0");
		Yrot.text = rotY.ToString("0");
		Zrot.text = rotZ.ToString("0");
		scaleX = selectedObject.transform.localScale.x;
		scaleY = selectedObject.transform.localScale.y;
		scaleZ = selectedObject.transform.localScale.z;
		Xscale.text = scaleX.ToString("0");
		Yscale.text = scaleY.ToString("0f");
		Zscale.text = scaleZ.ToString("0f");
	}

	public void UpdatePos()
	{
		posX = float.Parse(Xpos.text);
		posY = float.Parse(Ypos.text);
		posZ = float.Parse(Zpos.text);
		rotX = float.Parse(Xrot.text);
		rotY = float.Parse(Yrot.text);
		rotZ = float.Parse(Zrot.text);
		scaleX = float.Parse(Xscale.text);
		scaleY = float.Parse(Yscale.text);
		scaleZ = float.Parse(Zscale.text);
		UnityEngine.Debug.Log("Updated Position");
		selectedObject.transform.localPosition = new Vector3(posX, posY, posZ);
		selectedObject.transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
		selectedObject.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
	}

	public void spawn()
	{
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, 10f))
		{
			Object.Instantiate(selectedSpawnObject, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
		}
	}

	public void delete()
	{
		UnityEngine.Object.Destroy(selectedObject);
	}
}
