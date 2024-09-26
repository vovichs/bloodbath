using UnityEngine;

public class LineOfSightRendering : MonoBehaviour
{
	public string viewerTag = "Player";

	public LayerMask layerMask = -1;

	private GameObject player;

	private void Update()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag(viewerTag);
		}
		if (Physics.Raycast(base.transform.position, player.transform.position - base.transform.position, out RaycastHit hitInfo, float.PositiveInfinity, layerMask))
		{
			if (hitInfo.collider.gameObject == player)
			{
				GetComponent<Renderer>().enabled = true;
			}
			else
			{
				GetComponent<Renderer>().enabled = false;
			}
		}
		else
		{
			GetComponent<Renderer>().enabled = false;
		}
	}
}
