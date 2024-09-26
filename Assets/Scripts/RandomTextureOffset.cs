using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("TextureEffects/Random Texture Offset")]
public class RandomTextureOffset : MonoBehaviour
{
	[Serializable]
	public class MaterialInfo
	{
		public Material material;

		public string textureName = "_MainTex";
	}

	public List<MaterialInfo> materialInfoList = new List<MaterialInfo>();

	private void Update()
	{
		foreach (MaterialInfo materialInfo in materialInfoList)
		{
			materialInfo.material.SetTextureOffset(materialInfo.textureName, new Vector2(UnityEngine.Random.value, UnityEngine.Random.value));
		}
	}

	private void OnDestroy()
	{
		foreach (MaterialInfo materialInfo in materialInfoList)
		{
			materialInfo.material.SetTextureOffset(materialInfo.textureName, Vector2.zero);
		}
	}
}
