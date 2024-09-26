using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineVisualizerUsingVisualizationManager : MonoBehaviour
{
	public float size = 10f;

	public float amplitude = 1f;

	public int cutoffSample = 128;

	private LineRenderer lineRenderer;

	private float stepSize;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(cutoffSample);
		stepSize = size / (float)cutoffSample;
	}

	private void Update()
	{
		int num = 0;
		for (num = 0; num < cutoffSample; num++)
		{
			Vector3 position = new Vector3((float)num * stepSize - size / 2f, VisualizationManager.samples[num] * amplitude, 0f);
			lineRenderer.SetPosition(num, position);
		}
	}
}
