using UnityEngine;

public class ignoreCollider : MonoBehaviour
{
	public int Layer1;

	public int Layer2;

	private void Start()
	{
		Physics.IgnoreLayerCollision(Layer1, Layer2);
	}
}
