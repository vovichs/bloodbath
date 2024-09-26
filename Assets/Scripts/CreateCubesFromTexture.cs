using System.Collections.Generic;
using UnityEngine;

public class CreateCubesFromTexture : MonoBehaviour
{
	public Renderer cube;

	public Texture2D texture;

	public float alphaThreshold = 50f;

	public Dictionary<Color32, Material> palette = new Dictionary<Color32, Material>();

	private void Start()
	{
		int num = 0;
		int num2 = 0;
		Color32[] pixels = texture.GetPixels32();
		foreach (Color32 color in pixels)
		{
			if (num >= texture.width)
			{
				num2++;
				num = 0;
			}
			if ((float)(int)color.a > alphaThreshold)
			{
				Renderer renderer = UnityEngine.Object.Instantiate(cube, base.transform.position + base.transform.right * num + base.transform.up * num2, Quaternion.identity);
				if (!palette.ContainsKey(color))
				{
					renderer.material.color = color;
					palette.Add(color, renderer.sharedMaterial);
				}
				else
				{
					renderer.sharedMaterial = palette[color];
				}
			}
			num++;
		}
	}
}
