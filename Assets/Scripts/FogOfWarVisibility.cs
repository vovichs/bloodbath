using UnityEngine;

public class FogOfWarVisibility : MonoBehaviour
{
	private bool observed;

	private void Start()
	{
	}

	private void Update()
	{
		if (observed)
		{
			GetComponent<Renderer>().enabled = true;
		}
		else
		{
			GetComponent<Renderer>().enabled = false;
		}
		observed = false;
	}

	private void Observed()
	{
		UnityEngine.Debug.Log("Observed", base.gameObject);
		observed = true;
	}
}
