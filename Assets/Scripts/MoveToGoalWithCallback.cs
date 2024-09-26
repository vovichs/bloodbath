using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoveToGoalWithCallback : MonoBehaviour
{
	public Transform target;

	public float moveTime = 1f;

	public UnityEvent atDestinationCallback;

	private void Start()
	{
		StartCoroutine(MoveToGoal());
	}

	public void MoveAgain()
	{
		StartCoroutine(MoveToGoal());
	}

	private IEnumerator MoveToGoal()
	{
		float t = 0f;
		Vector3 startPosition = base.transform.position;
		Vector3 goalPosition = target.position;
		while (t < moveTime)
		{
			t += Time.deltaTime;
			base.transform.position = Vector3.Lerp(startPosition, goalPosition, t / moveTime);
			yield return null;
		}
		base.transform.position = target.position;
		atDestinationCallback.Invoke();
	}
}
