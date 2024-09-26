using UnityEngine;

public class Footsteps : MonoBehaviour
{
	[Header("Tweak Values")]
	public float footprintOffset = 0.05f;

	[Header("Required References")]
	public GameObject leftFootprint;

	public GameObject rightFootprint;

	public Transform leftFootLocation;

	public Transform rightFootLocation;

	public AudioSource leftFootAudioSource;

	public AudioSource rightFootAudioSource;

	private void LeftFootstep()
	{
		leftFootAudioSource.Play();
		if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out RaycastHit hitInfo))
		{
			Object.Instantiate(leftFootprint, hitInfo.point + hitInfo.normal * footprintOffset, Quaternion.LookRotation(hitInfo.normal, leftFootLocation.up));
		}
	}

	private void RightFootstep()
	{
		rightFootAudioSource.Play();
		if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out RaycastHit hitInfo))
		{
			Object.Instantiate(rightFootprint, hitInfo.point + hitInfo.normal * footprintOffset, Quaternion.LookRotation(hitInfo.normal, rightFootLocation.up));
		}
	}
}
