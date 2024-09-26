using UnityEngine;

public class AdvancedFootsteps : MonoBehaviour
{
	public GameObject leftFootprint;

	public GameObject rightFootprint;

	public Transform leftFootLocation;

	public Transform rightFootLocation;

	public float footprintOffset = 0.05f;

	public AudioSource leftFootAudioSource;

	public AudioSource rightFootAudioSource;

	private void LeftFootstep()
	{
		if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out RaycastHit hitInfo))
		{
			FootstepMaterialProperties component = hitInfo.transform.GetComponent<FootstepMaterialProperties>();
			if (component != null)
			{
				if (component.showFootprints)
				{
					Object.Instantiate(leftFootprint, hitInfo.point + hitInfo.normal * footprintOffset, Quaternion.LookRotation(hitInfo.normal, leftFootLocation.up));
				}
				leftFootAudioSource.PlayOneShot(component.materialSound);
			}
			else
			{
				leftFootAudioSource.Play();
			}
		}
		else
		{
			leftFootAudioSource.Play();
		}
	}

	private void RightFootstep()
	{
		if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out RaycastHit hitInfo))
		{
			FootstepMaterialProperties component = hitInfo.transform.GetComponent<FootstepMaterialProperties>();
			if (component != null)
			{
				if (component.showFootprints)
				{
					Object.Instantiate(rightFootprint, hitInfo.point + hitInfo.normal * footprintOffset, Quaternion.LookRotation(hitInfo.normal, rightFootLocation.up));
				}
				rightFootAudioSource.PlayOneShot(component.materialSound);
			}
			else
			{
				rightFootAudioSource.Play();
			}
		}
		else
		{
			rightFootAudioSource.Play();
		}
	}
}
