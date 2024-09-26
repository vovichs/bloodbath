using UnityEngine;

public class RandomActivator : MonoBehaviour
{
	public GameObject[] OBJs;

	private void Start()
	{
		int num = UnityEngine.Random.Range(0, OBJs.Length - 1);
		OBJs[num].SetActive(value: true);
	}
}
