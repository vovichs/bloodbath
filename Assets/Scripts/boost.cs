using UnityEngine;

public class boost : MonoBehaviour
{
	public Rigidbody rigid;

	public float thrust;

	public bool greenArrow;

	public bool redArrow;

	public bool blueArrow;

	public bool Triggered;

	private void Update()
	{
		if (Triggered)
		{
			if (greenArrow)
			{
				rigid.AddForce(base.transform.up * (thrust * 100f));
			}
			if (blueArrow)
			{
				rigid.AddForce(base.transform.forward * (thrust * 100f));
			}
			if (redArrow)
			{
				rigid.AddForce(base.transform.right * (thrust * 100f));
			}
		}
	}

	public void trigger()
	{
		Triggered = true;
	}
}
