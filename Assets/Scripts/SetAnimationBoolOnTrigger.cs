using UnityEngine;

public class SetAnimationBoolOnTrigger : MonoBehaviour
{
	public string animationBoolName;

	public Animator target;

	private void OnTriggerEnter()
	{
		target.SetBool(animationBoolName, value: true);
	}

	private void OnTriggerExit()
	{
		target.SetBool(animationBoolName, value: false);
	}
}
