using UnityEngine;

public class RandomSkin : MonoBehaviour
{
	public Material[] iris;

	public Material[] outFit;

	public Renderer iris1;

	public Renderer iris2;

	public Renderer outFit1;

	private int irisNum;

	private int outFitNum;

	private void Start()
	{
		irisNum = UnityEngine.Random.Range(0, iris.Length);
		outFitNum = UnityEngine.Random.Range(0, outFit.Length);
		iris1.material = iris[irisNum];
		iris2.material = iris[irisNum];
		outFit1.material = outFit[outFitNum];
	}
}
