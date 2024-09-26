using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DamageSystem : MonoBehaviour
{
	public float health = 100f;

	public float startHealth;

	public float minMagnitude = 5f;

	public LayerMask Mask;

	private int ranNum;

	private float startBleedInterval;

	public float bleedInterval = 10f;

	public bool bleedAble;

	private bool bleedCoroutine;

	public GameObject[] bloodObjects;

	public Renderer[] damageDecals;

	public GameObject[] explodeGib;

	public GameObject[] gibsAmount;

	public Character character;

	private Rigidbody rigid;

	private CharacterJoint charJoint;

	public bool Head;

	public bool Throat;

	public bool includesOrgans;

	public int organHitChance;

	private int organNum;

	public bool frozen;

	public bool damageOnCollision = true;

	public bool dismemberAble;

	public bool resizeOnDeath;

	public bool destroyOnDeath = true;

	public bool broken;

	public bool disconnected;

	private float damageResistance = 1f;

	public UnityEvent onDeath;

	public UnityEvent onOrganHit;

	public UnityEvent onShot;

	private AudioSource AS;

	public AudioClip BreakSound;

	private bool Playd;

	private void Start()
	{
		if (damageDecals != null)
		{
			for (int i = 0; i < damageDecals.Length; i++)
			{
				Color color = damageDecals[i].material.color;
				color.a = 0f;
				damageDecals[i].material.color = color;
			}
		}
		if (AS == null)
		{
			AS = GetComponent<AudioSource>();
		}
		disconnected = false;
		rigid = GetComponent<Rigidbody>();
		startBleedInterval = bleedInterval;
		startHealth = health;
		damageResistance = 1f;
	}

	public void Armor(float amount)
	{
		damageResistance += amount / 10f;
	}

	public void DisconnectedFromChild()
	{
		bleedInterval = 0.0001f;
	}

	private void Update()
	{
		if (bleedInterval <= 0.0001f)
		{
			bleedInterval = 0.0002f;
		}
		if (bleedInterval < startBleedInterval)
		{
			if (!disconnected && !includesOrgans)
			{
				bleedInterval += 5E-05f;
			}
			else
			{
				bleedInterval += 0.0001f;
			}
			if (!bleedCoroutine && bleedAble)
			{
				bleedCoroutine = true;
				StartCoroutine(Bleed());
			}
		}
		else if (bleedInterval > startBleedInterval)
		{
			bleedInterval = startBleedInterval;
		}
	}

	public IEnumerator Bleed()
	{
		if (character.bledOut)
		{
			yield break;
		}
		yield return new WaitForSeconds(bleedInterval * UnityEngine.Random.Range(0.8f, 1.2f) / (character.bloodInPercent / 100f));
		ranNum = UnityEngine.Random.Range(0, bloodObjects.Length);
		if (!disconnected)
		{
			character.health -= UnityEngine.Random.Range(0.01f, 0.1f);
		}
		float maxDistance = 100f;
		new Ray(base.transform.position + Vector3.up * 100f, Vector3.down);
		character.bloodInLiters -= UnityEngine.Random.Range(0.001f, 0.002f);
		Quaternion from = Quaternion.LookRotation(Vector3.down);
		Quaternion rotation = UnityEngine.Random.rotation;
		from = Quaternion.RotateTowards(from, rotation, UnityEngine.Random.Range(0f, 5f));
		if (Physics.Raycast(base.transform.position, from * Vector3.forward, out RaycastHit hitInfo, maxDistance, Mask))
		{
			if (hitInfo.collider != null && hitInfo.transform.tag != "Decal")
			{
				Object.Instantiate(bloodObjects[ranNum], hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal));
			}
			else if (hitInfo.collider != null && hitInfo.transform.tag == "Decal")
			{
				hitInfo.transform.localScale += new Vector3(0.007f, 0.007f, 0f) / (hitInfo.transform.localScale.x + 1f);
			}
		}
		bleedCoroutine = false;
	}

	public void TakeDamage(float amount)
	{
		character.Anger();
		onShot.Invoke();
		if (!character.Hurt && !disconnected)
		{
			character.StartCoroutine(character.Damaged());
		}
		if (!character.dead && !disconnected)
		{
			character.pain += amount * (float)UnityEngine.Random.Range(3, 8) / damageResistance;
		}
		if (!disconnected)
		{
			bleedInterval -= amount / 2f;
			health -= amount / damageResistance;
		}
		if (Head)
		{
			if (amount >= 10f)
			{
				if (!disconnected)
				{
					character.pain += amount * (float)UnityEngine.Random.Range(80, 100);
					bleedInterval -= UnityEngine.Random.Range(8f, 10f / damageResistance);
					character.health -= UnityEngine.Random.Range(90, 100);
					character.HeadShot();
				}
			}
			else if (!disconnected)
			{
				bleedInterval -= amount / 5f + (float)UnityEngine.Random.Range(8, 10);
				character.health -= (float)UnityEngine.Random.Range(80, 100) / damageResistance;
				character.HeadShot();
			}
		}
		else if (Throat)
		{
			character.pain += 40f;
			if (!disconnected)
			{
				character.pain += 250f;
				bleedInterval = 0.01f;
				character.NeckShot();
				character.health = character.health / 3f / damageResistance;
			}
		}
		else if (!disconnected)
		{
			character.GunShot();
		}
		if (includesOrgans)
		{
			organNum = UnityEngine.Random.Range(0, organHitChance + 1);
			if (organNum == 0)
			{
				bleedInterval -= (float)UnityEngine.Random.Range(4, 9) / damageResistance;
				character.pain += amount * (float)UnityEngine.Random.Range(5, 20);
				character.health -= amount * UnityEngine.Random.Range(0.3f, 10f) / damageResistance;
				if (damageResistance <= 1.1f)
				{
					onOrganHit.Invoke();
					character.BecomeRagDoll();
					character.pain += 250f;
				}
			}
		}
		if (!(amount >= 10f) || UnityEngine.Random.Range(0, 20) != 2)
		{
			return;
		}
		if (health <= startHealth / 2f && dismemberAble)
		{
			character.Dismembered = true;
			character.BecomeRagDoll();
			if (Head)
			{
				character.dead = true;
			}
			bleedInterval = 0.0001f;
			if (!disconnected)
			{
				AS.PlayOneShot(BreakSound);
			}
			disconnected = true;
			charJoint = base.gameObject.GetComponent<CharacterJoint>();
			UnityEngine.Object.Destroy(charJoint);
			base.gameObject.transform.parent = null;
			onDeath.Invoke();
		}
		if (!(health <= 0f))
		{
			return;
		}
		character.Dismembered = true;
		character.BecomeRagDoll();
		if (Head)
		{
			character.dead = true;
		}
		bleedInterval = 0.02f;
		ranNum = UnityEngine.Random.Range(0, bloodObjects.Length);
		if (!Playd && !AS.isPlaying)
		{
			AS.PlayOneShot(BreakSound);
			Playd = true;
		}
		broken = true;
		health = 0f;
		if (character != null)
		{
			character.BecomeRagDoll();
		}
		if (resizeOnDeath)
		{
			UnityEngine.Object.Destroy(GetComponent<Rigidbody>(), 1f);
			onDeath.Invoke();
			base.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
			base.gameObject.GetComponent<Collider>().enabled = false;
			gibsAmount = new GameObject[explodeGib.Length];
			for (int i = 0; i < explodeGib.Length - UnityEngine.Random.Range(1, explodeGib.Length); i++)
			{
				gibsAmount[i] = UnityEngine.Object.Instantiate(explodeGib[i], base.transform.position + new Vector3(UnityEngine.Random.Range(-1, 1), 0f, UnityEngine.Random.Range(-1, 1)), Quaternion.identity);
			}
		}
		if (destroyOnDeath)
		{
			onDeath.Invoke();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "NPC" || !damageOnCollision || broken || !(collision.relativeVelocity.magnitude > minMagnitude))
		{
			return;
		}
		health -= collision.relativeVelocity.magnitude / 2f / damageResistance;
		if (damageDecals != null)
		{
			for (int i = 0; i < damageDecals.Length; i++)
			{
				Color color = damageDecals[i].material.color;
				color.a = 1f - health / startHealth;
				damageDecals[i].material.color = color;
			}
		}
		if (!character.Hurt && Head)
		{
			character.StartCoroutine(character.Damaged());
		}
		if (Head)
		{
			character.health -= collision.relativeVelocity.magnitude / 2f / damageResistance;
		}
		if (includesOrgans)
		{
			character.health -= collision.relativeVelocity.magnitude / 4f / damageResistance;
		}
		if (!disconnected)
		{
			character.pain += collision.relativeVelocity.magnitude;
			character.Collision();
			bleedInterval -= collision.relativeVelocity.magnitude / 6f;
		}
		if (dismemberAble && collision.relativeVelocity.magnitude >= 10f && collision.transform.tag == "Sharp")
		{
			character.Dismembered = true;
			character.BecomeRagDoll();
			if (Head)
			{
				character.dead = true;
			}
			bleedInterval = 0.02f;
			if (!disconnected)
			{
				AS.PlayOneShot(BreakSound);
			}
			disconnected = true;
			charJoint = base.gameObject.GetComponent<CharacterJoint>();
			UnityEngine.Object.Destroy(charJoint);
			base.gameObject.transform.parent = null;
			onDeath.Invoke();
		}
		if (health <= startHealth / 2f && dismemberAble)
		{
			character.Dismembered = true;
			character.BecomeRagDoll();
			if (Head)
			{
				character.dead = true;
			}
			bleedInterval = 0.0001f;
			if (!disconnected)
			{
				AS.PlayOneShot(BreakSound);
			}
			disconnected = true;
			charJoint = base.gameObject.GetComponent<CharacterJoint>();
			UnityEngine.Object.Destroy(charJoint);
			base.gameObject.transform.parent = null;
			onDeath.Invoke();
		}
		if (!(health <= 0f) || !(collision.transform.tag != "Sharp"))
		{
			return;
		}
		character.Dismembered = true;
		character.BecomeRagDoll();
		if (Head)
		{
			character.dead = true;
		}
		bleedInterval = 0.02f;
		ranNum = UnityEngine.Random.Range(0, bloodObjects.Length);
		if (!Playd && !AS.isPlaying)
		{
			AS.PlayOneShot(BreakSound);
			Playd = true;
		}
		broken = true;
		health = 0f;
		if (character != null)
		{
			character.BecomeRagDoll();
		}
		if (resizeOnDeath)
		{
			UnityEngine.Object.Destroy(GetComponent<Rigidbody>(), 1f);
			onDeath.Invoke();
			base.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
			base.gameObject.GetComponent<Collider>().enabled = false;
			gibsAmount = new GameObject[explodeGib.Length];
			for (int j = 0; j < explodeGib.Length - UnityEngine.Random.Range(1, explodeGib.Length); j++)
			{
				gibsAmount[j] = UnityEngine.Object.Instantiate(explodeGib[j], base.transform.position + new Vector3(UnityEngine.Random.Range(-1, 1), 0f, UnityEngine.Random.Range(-1, 1)), Quaternion.identity);
			}
		}
		if (destroyOnDeath)
		{
			onDeath.Invoke();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
