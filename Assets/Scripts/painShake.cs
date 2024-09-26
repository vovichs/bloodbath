using UnityEngine;

public class painShake : MonoBehaviour
{
	public Character character;

	private Quaternion originalRot;

	public bool plus;

	private void Update()
	{
		if (Time.timeScale == 0f || !character.ragdoll || character.dead)
		{
			return;
		}
		float num;
		if (!plus)
		{
			num = UnityEngine.Random.Range(0f, character.pain);
			if (num >= 50f)
			{
				num = 45f;
			}
		}
		else
		{
			num = UnityEngine.Random.Range(0f, 0f - character.pain);
			if (num <= -50f)
			{
				num = -45f;
			}
		}
		base.transform.localRotation *= Quaternion.Euler(0f, 0f, num / 200f);
	}
}
