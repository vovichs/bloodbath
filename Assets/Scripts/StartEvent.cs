using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
	public UnityEvent Event;

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		Event.Invoke();
	}
}
