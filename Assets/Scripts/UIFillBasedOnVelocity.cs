using UnityEngine;
using UnityEngine.UI;

public class UIFillBasedOnVelocity : MonoBehaviour
{
	public Rigidbody objectToMeasure;

	public float maxVelocity = 10f;

	public bool disallowBackwards;

	private Image image;

	private void Start()
	{
		image = GetComponent<Image>();
	}

	private void Update()
	{
		if (!disallowBackwards || Vector3.Dot(objectToMeasure.velocity, objectToMeasure.transform.forward) > 0f)
		{
			image.fillAmount = objectToMeasure.velocity.magnitude / maxVelocity;
		}
		else
		{
			image.fillAmount = 0f;
		}
	}
}
