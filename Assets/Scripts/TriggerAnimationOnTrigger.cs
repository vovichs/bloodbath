using UnityEngine;

public class TriggerAnimationOnTrigger : MonoBehaviour
{
	public Animator animator;

	public string onTriggerEnterParameterName;

	public string onTriggerExitParameterName;

	private void Start()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
			if (animator == null)
			{
				UnityEngine.Debug.LogError("No animator component on this script!", base.gameObject);
			}
		}
	}

	private void OnTriggerEnter()
	{
		if (onTriggerEnterParameterName != null)
		{
			animator.SetTrigger(onTriggerEnterParameterName);
		}
	}

	private void OnTriggerExit()
	{
		if (onTriggerExitParameterName != null)
		{
			animator.SetTrigger(onTriggerExitParameterName);
		}
	}
}
