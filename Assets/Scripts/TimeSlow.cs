using System.Collections;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
	public static TimeSlow Instance;

	public float defaultTimeScale = 0.5f;

	public float defaultDuration = 5f;

	public float defaultTransitionTime = 0.5f;

	private void Start()
	{
		Instance = this;
	}

	public void SlowTime()
	{
		StopAllCoroutines();
		StartCoroutine(ActuallyTimeSlow(defaultTimeScale, defaultDuration, defaultTransitionTime));
	}

	public void SlowTime(float timeScale, float duration, float transitionTime)
	{
		StopAllCoroutines();
		StartCoroutine(ActuallyTimeSlow(timeScale, duration, transitionTime));
	}

	private IEnumerator ActuallyTimeSlow(float timeScale, float duration, float transitionTime)
	{
		float initialTime3 = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - initialTime3 < transitionTime)
		{
			Time.timeScale = Mathf.Lerp(1f, timeScale, (Time.realtimeSinceStartup - initialTime3) / transitionTime);
			yield return null;
		}
		Time.timeScale = timeScale;
		initialTime3 = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - initialTime3 < duration)
		{
			yield return null;
		}
		initialTime3 = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - initialTime3 < transitionTime)
		{
			Time.timeScale = Mathf.Lerp(timeScale, 1f, (Time.realtimeSinceStartup - initialTime3) / transitionTime);
			yield return null;
		}
		Time.timeScale = 1f;
	}
}
