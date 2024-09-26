using UnityEngine;
using UnityEngine.Events;

public class entertriggerultra : MonoBehaviour
{
	public UnityEvent OnTriggerEnter1;

	public UnityEvent OnTriggerExit1;

	public string Tags;

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == Tags)
		{
			OnTriggerEnter1.Invoke();
		}
		else if (Tags == null)
		{
			OnTriggerEnter1.Invoke();
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.tag == Tags)
		{
			OnTriggerExit1.Invoke();
		}
		else if (Tags == null)
		{
			OnTriggerExit1.Invoke();
		}
	}
}
