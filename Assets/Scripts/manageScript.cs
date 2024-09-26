using UnityEngine;

public class manageScript : MonoBehaviour
{
	public Behaviour Behavior;

	public bool Enable;

	private void OnEnable()
	{
		Behavior.enabled = Enable;
	}
}
