using UnityEngine;

public class SetObliqueFrustrum : MonoBehaviour
{
	public float horizontalOblique;

	public float verticalOblique;

	public bool setEveryFrame;

	private void Start()
	{
		SetObliqueness(horizontalOblique, verticalOblique);
	}

	private void Update()
	{
		if (setEveryFrame)
		{
			SetObliqueness(horizontalOblique, verticalOblique);
		}
	}

	private void SetObliqueness(float horizObl, float vertObl)
	{
		Matrix4x4 projectionMatrix = GetComponent<Camera>().projectionMatrix;
		projectionMatrix[0, 2] = horizObl;
		projectionMatrix[1, 2] = vertObl;
		GetComponent<Camera>().projectionMatrix = projectionMatrix;
	}
}
