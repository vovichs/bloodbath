using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
	public Camera cam;

	public GameObject selectedBody;

	public Text selectedNameText;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(cam.ScreenPointToRay(UnityEngine.Input.mousePosition), out RaycastHit hitInfo))
		{
			selectedBody = hitInfo.transform.gameObject;
			selectedNameText.text = selectedBody.name;
		}
	}
}
