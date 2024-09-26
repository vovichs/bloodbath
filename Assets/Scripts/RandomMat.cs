using UnityEngine;

public class RandomMat : MonoBehaviour
{
	public Material[] Mat;

	public Renderer OBJ;

	private int irisNum;

	private int outFitNum;

	private void Start()
	{
		outFitNum = UnityEngine.Random.Range(0, Mat.Length);
		OBJ.material = Mat[outFitNum];
	}
}
