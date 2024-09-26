using UnityEngine;

public class BallisticTargeting : MonoBehaviour
{
	public class BallisticInfo
	{
		public float lowAngle;

		public float highAngle;
	}

	public Transform target;

	public Transform targetIndicator1;

	public Transform targetIndicator2;

	public GameObject targetHUD1;

	public GameObject targetHUD2;

	public float velocity = 10f;

	public bool debugMode;

	private float v;

	private float vSquared;

	private float vHyperCubed;

	private void Start()
	{
		v = velocity;
		vSquared = v * v;
		vHyperCubed = vSquared * vSquared;
	}

	private BallisticInfo CalculateTrajectoryAngles()
	{
		Vector3 vector = target.position - base.transform.position;
		float y = vector.y;
		vector.y = 0f;
		float magnitude = vector.magnitude;
		float num = y;
		float num2 = Mathf.Abs(Physics.gravity.y);
		float num3 = vHyperCubed - num2 * (num2 * magnitude * magnitude + 2f * num * vSquared);
		if (num3 < 0f)
		{
			return null;
		}
		float num4 = Mathf.Sqrt(num3);
		float x = vSquared + num4;
		float x2 = vSquared - num4;
		float y2 = num2 * magnitude;
		float num5 = Mathf.Atan2(y2, x);
		float num6 = Mathf.Atan2(y2, x2);
		return new BallisticInfo
		{
			lowAngle = num5 * 57.29578f,
			highAngle = num6 * 57.29578f
		};
	}

	private void Update()
	{
		BallisticInfo ballisticInfo = CalculateTrajectoryAngles();
		if (ballisticInfo != null)
		{
			targetHUD1.SetActive(value: true);
			targetHUD2.SetActive(value: true);
			targetIndicator1.localEulerAngles = new Vector3(0f - ballisticInfo.lowAngle, 0f, 0f);
			targetIndicator2.localEulerAngles = new Vector3(0f - ballisticInfo.highAngle, 0f, 0f);
			if (debugMode)
			{
				base.transform.localRotation = Quaternion.LookRotation(target.position - base.transform.position);
				Camera.main.transform.localEulerAngles = new Vector3(0f - ballisticInfo.highAngle, 0f, 0f);
			}
		}
		else
		{
			targetHUD1.SetActive(value: false);
			targetHUD2.SetActive(value: false);
		}
	}
}
