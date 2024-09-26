using UnityEngine;

public class CubeSave : MonoBehaviour
{
	private int cubeID;

	private static int currentCubeID;

	private void Start()
	{
		cubeID = currentCubeID;
		currentCubeID++;
		if (PlayerPrefs.HasKey("CubePosition" + cubeID.ToString()))
		{
			base.transform.position = PlayerPrefsX.GetVector3("CubePosition" + cubeID.ToString());
			base.transform.rotation = PlayerPrefsX.GetQuaternion("CubeRotation" + cubeID.ToString());
			GetComponent<Rigidbody>().velocity = PlayerPrefsX.GetVector3("CubeRigidbodyVelocity" + cubeID.ToString(), GetComponent<Rigidbody>().velocity);
			GetComponent<Rigidbody>().angularVelocity = PlayerPrefsX.GetVector3("CubeRigidbodyAngularVelocity" + cubeID.ToString(), GetComponent<Rigidbody>().angularVelocity);
		}
	}

	private void OnDestroy()
	{
		PlayerPrefsX.SetVector3("CubePosition" + cubeID.ToString(), base.transform.position);
		PlayerPrefsX.SetQuaternion("CubeRotation" + cubeID.ToString(), base.transform.rotation);
		PlayerPrefsX.SetVector3("CubeRigidbodyVelocity" + cubeID.ToString(), GetComponent<Rigidbody>().velocity);
		PlayerPrefsX.SetVector3("CubeRigidbodyAngularVelocity" + cubeID.ToString(), GetComponent<Rigidbody>().angularVelocity);
	}
}
