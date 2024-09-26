using UnityEngine;

public class DisplayCurrentDieValue : MonoBehaviour
{
	public LayerMask dieValueColliderLayer = -1;

	private int currentValue = 1;

	private bool rollComplete;

	private void Update()
	{
		if (Physics.Raycast(base.transform.position, Vector3.up, out RaycastHit hitInfo, float.PositiveInfinity, dieValueColliderLayer))
		{
			currentValue = hitInfo.collider.GetComponent<DieValue>().value;
		}
		if (GetComponent<Rigidbody>().IsSleeping() && !rollComplete)
		{
			rollComplete = true;
			UnityEngine.Debug.Log("Die roll complete, die is at rest");
		}
		else if (!GetComponent<Rigidbody>().IsSleeping())
		{
			rollComplete = false;
		}
	}

	private void OnGUI()
	{
		GUILayout.Label(currentValue.ToString());
	}
}
