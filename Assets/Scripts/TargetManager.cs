using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
	public float scaleMultiplier = 1f;

	public LayerMask targetMask = -1;

	public Text targetInfo;

	private RectTransform rectTransform;

	private CanvasRenderer canvasRenderer;

	private Transform target;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasRenderer = GetComponent<CanvasRenderer>();
		base.transform.position = Vector3.one * 10000f;
	}

	private void LateUpdate()
	{
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, float.PositiveInfinity, targetMask))
		{
			target = hitInfo.transform;
		}
		if (target != null && Vector3.Dot(target.position - Camera.main.transform.position, Camera.main.transform.forward) > 0f)
		{
			base.transform.position = Camera.main.WorldToScreenPoint(target.position);
			Rect rect = GUIRectWithObject(target);
			rectTransform.sizeDelta = new Vector2(rect.width, rect.height) * scaleMultiplier;
			Text text = targetInfo;
			string[] obj = new string[9]
			{
				target.name,
				"\nX:",
				null,
				null,
				null,
				null,
				null,
				null,
				null
			};
			Vector3 position = target.position;
			obj[2] = position.x.ToString("F1");
			obj[3] = " Y:";
			position = target.position;
			obj[4] = position.y.ToString("F1");
			obj[5] = " Z:";
			position = target.position;
			obj[6] = position.z.ToString("F1");
			obj[7] = "\nDistance: ";
			obj[8] = (Camera.main.transform.position - target.position).magnitude.ToString("F2");
			text.text = string.Concat(obj);
		}
		else
		{
			base.transform.position = Vector3.one * 10000f;
		}
	}

	public static Rect GUIRectWithObject(Transform trans)
	{
		Vector3 center = trans.GetComponent<Renderer>().bounds.center;
		Vector3 extents = trans.GetComponent<Renderer>().bounds.extents;
		Vector2[] obj = new Vector2[8]
		{
			Camera.main.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z)),
			Camera.main.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z)),
			Camera.main.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z)),
			Camera.main.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z)),
			Camera.main.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z)),
			Camera.main.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z)),
			Camera.main.WorldToScreenPoint(new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z)),
			Camera.main.WorldToScreenPoint(new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z))
		};
		Vector2 vector = obj[0];
		Vector2 vector2 = obj[0];
		Vector2[] array = obj;
		foreach (Vector2 rhs in array)
		{
			vector = Vector2.Min(vector, rhs);
			vector2 = Vector2.Max(vector2, rhs);
		}
		return new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
	}
}
