using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
	public float speed = 1f;

	private GameObject player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		base.transform.position += (player.transform.position - base.transform.position).normalized * speed * Time.deltaTime;
	}
}
