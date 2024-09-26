using UnityEngine;

public class ScriptableObjectReferenceExample : MonoBehaviour
{
	public TestScriptableObject scriptableObjectReference;

	private void Start()
	{
		UnityEngine.Debug.Log(scriptableObjectReference.exampleFloat);
		Object.Instantiate(scriptableObjectReference.gameObjectReference, scriptableObjectReference.exampleVector, Quaternion.identity);
	}
}
