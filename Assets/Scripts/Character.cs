using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
	public bool FightBack;

	public eventGun[] Guns;

	public eventGun chosenGun;

	public AudioSource AS;

	public AudioClip[] NeckShotSound;

	public AudioClip[] HeadShotSound;

	public AudioClip[] GunShotSound;

	public AudioClip[] CollisionSound;

	public float pain;

	public float health = 100f;

	public DamageSystem head;

	public bool dead;

	public Transform Pelvis;

	public rigidChild rigidchild;

	public UnityEvent on0Health;

	public UnityEvent onRagdoll;

	private bool died;

	public GameObject NormalFace;

	public GameObject PainFace;

	public GameObject ShiftFace;

	public GameObject DeadFace;

	public GameObject ClosedMouth;

	public GameObject OpenedMouth;

	private bool openMouth;

	private bool InPain;

	public bool Dismembered;

	public bool Hurt;

	private bool MidShift;

	private bool Blinking;

	private bool BlinkCoroutine;

	public float bloodInLiters;

	public float startBlood;

	public float bloodInPercent;

	public bool bledOut;

	public bool ragdoll;

	public Animator anim;

	public NavMeshAgent AI;

	private float startSpeed;

	public float speed;

	public Transform target;

	private bool Angered;

	private bool ReactedToPain;

	public bool shooting;

	public bool runningAway;

	public float Distance;

	public Mood Group;

	public void Start()
	{
		startSpeed = speed;
		bloodInLiters = 5f;
		rigidchild.SetKinematic(newValue: true);
		startBlood = bloodInLiters;
	}

	public IEnumerator Damaged()
	{
		if ((float)UnityEngine.Random.Range(1, 4) == 3f)
		{
			chosenGun.Shoot();
		}
		Hurt = true;
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 2f));
		Hurt = false;
	}

	public IEnumerator Blink()
	{
		Blinking = false;
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f, 7f));
		if (!MidShift)
		{
			Blinking = true;
			yield return new WaitForSeconds(0.1f);
			Blinking = false;
			BlinkCoroutine = false;
		}
	}

	public void ForceMouth()
	{
		StartCoroutine("OpenMouth");
	}

	public void NeckShot()
	{
		if (!dead && !AS.isPlaying && !MidShift)
		{
			AS.clip = NeckShotSound[Random.Range(0, NeckShotSound.Length)];
			AS.Play();
		}
	}

	public void HeadShot()
	{
		if (!dead)
		{
			AS.Stop();
			AS.clip = HeadShotSound[Random.Range(0, HeadShotSound.Length)];
			AS.Play();
		}
	}

	public void GunShot()
	{
		if (!dead && !MidShift && !AS.isPlaying)
		{
			AS.clip = GunShotSound[Random.Range(0, GunShotSound.Length)];
			AS.Play();
		}
	}

	public void Collision()
	{
		if (!dead && !MidShift && !AS.isPlaying)
		{
			AS.clip = CollisionSound[Random.Range(0, CollisionSound.Length)];
			AS.Play();
		}
	}

	private void InstantlyTurn(Vector3 destination)
	{
		if (!((destination - base.transform.position).magnitude < 0.1f))
		{
			Quaternion b = Quaternion.LookRotation((destination - base.transform.position).normalized);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * 5f);
		}
	}

	private void Update()
	{
		if (Time.timeScale <= 0.1f)
		{
			return;
		}
		if (FightBack && Group.Angered && !Angered)
		{
			Anger();
		}
		AI.destination = target.position;
		Distance = Vector3.Distance(target.position, base.transform.position);
		if (Distance >= 20f && Angered && !ragdoll)
		{
			chosenGun.forceFire = false;
			anim.SetBool("Running", value: true);
			AI.speed = startSpeed * 2f;
		}
		else
		{
			anim.SetBool("Running", value: false);
			if (!ReactedToPain && !ragdoll && Angered)
			{
				if (Distance <= 11f)
				{
					chosenGun.forceFire = true;
					shooting = true;
					anim.SetBool("Shooting", value: true);
					InstantlyTurn(AI.destination);
					AI.speed = 0f;
					anim.speed = 1f;
				}
				else if (Distance >= 10f)
				{
					chosenGun.forceFire = false;
					shooting = false;
					anim.SetBool("Shooting", value: false);
				}
			}
			else
			{
				chosenGun.forceFire = false;
				shooting = false;
				anim.SetBool("Shooting", value: false);
			}
			if (Angered && !ReactedToPain && !shooting)
			{
				if (AS.isPlaying)
				{
					chosenGun.forceFire = false;
					shooting = false;
					anim.speed = 0.1f;
					AI.speed = 0f;
				}
				else
				{
					chosenGun.forceFire = false;
					shooting = false;
					anim.speed = 1f;
					AI.speed = speed;
				}
			}
		}
		bloodInPercent = bloodInLiters / startBlood * 100f;
		if (bloodInLiters < 0f)
		{
			bloodInLiters = 0f;
		}
		if (!dead)
		{
			if (bloodInLiters <= 2.5f)
			{
				dead = true;
			}
			if (pain > 0f)
			{
				pain -= 0.01f;
			}
			if (head.broken)
			{
				dead = true;
			}
			if (health <= 0f)
			{
				dead = true;
			}
		}
		if (bloodInLiters <= 0f)
		{
			bledOut = true;
		}
		else if (health <= 25f)
		{
			MidShift = true;
			BecomeRagDoll();
		}
		if (dead)
		{
			if (!died)
			{
				chosenGun.UseAble = false;
				anim.enabled = false;
				died = true;
				rigidchild.SetKinematic(newValue: false);
				on0Health.Invoke();
				BecomeRagDoll();
			}
			AS.volume = 0f;
			pain = 0f;
			if (!PainFace.active)
			{
				PainFace.SetActive(value: false);
				NormalFace.SetActive(value: false);
				ShiftFace.SetActive(value: false);
				DeadFace.SetActive(value: true);
			}
			return;
		}
		if (AS.isPlaying)
		{
			OpenedMouth.SetActive(value: true);
			ClosedMouth.SetActive(value: false);
		}
		else if (Dismembered)
		{
			OpenedMouth.SetActive(value: true);
			ClosedMouth.SetActive(value: false);
		}
		else if (openMouth)
		{
			OpenedMouth.SetActive(value: true);
			ClosedMouth.SetActive(value: false);
		}
		else
		{
			OpenedMouth.SetActive(value: false);
			ClosedMouth.SetActive(value: true);
		}
		if (pain >= 250f && !ReactedToPain)
		{
			InPain = true;
			ReactedToPain = true;
			BecomeRagDoll();
		}
		else
		{
			InPain = false;
		}
		if (!BlinkCoroutine && !MidShift)
		{
			BlinkCoroutine = true;
			StartCoroutine(Blink());
		}
		else if (Blinking)
		{
			PainFace.SetActive(value: true);
			NormalFace.SetActive(value: false);
			ShiftFace.SetActive(value: false);
			DeadFace.SetActive(value: false);
		}
		else if (Hurt)
		{
			PainFace.SetActive(value: true);
			NormalFace.SetActive(value: false);
			ShiftFace.SetActive(value: false);
			DeadFace.SetActive(value: false);
		}
		else if (InPain)
		{
			PainFace.SetActive(value: true);
			NormalFace.SetActive(value: false);
			ShiftFace.SetActive(value: false);
			DeadFace.SetActive(value: false);
		}
		else if (MidShift)
		{
			PainFace.SetActive(value: false);
			NormalFace.SetActive(value: false);
			ShiftFace.SetActive(value: true);
			DeadFace.SetActive(value: false);
		}
		else
		{
			PainFace.SetActive(value: false);
			NormalFace.SetActive(value: true);
			ShiftFace.SetActive(value: false);
			DeadFace.SetActive(value: false);
		}
	}

	public void BecomeRagDoll()
	{
		if (FightBack)
		{
			chosenGun.forceFire = false;
		}
		onRagdoll.Invoke();
		ragdoll = true;
		rigidchild.SetKinematic(newValue: false);
		anim.enabled = false;
		AI.enabled = false;
	}

	public void Scare()
	{
	}

	public void Anger()
	{
		if (Group != null)
		{
			Group.Angered = true;
		}
		if (!FightBack)
		{
			Scare();
		}
		else if (!Angered)
		{
			Angered = true;
			anim.SetBool("Angry", value: true);
			int num = UnityEngine.Random.Range(0, Guns.Length);
			Guns[num].gameObject.SetActive(value: true);
			chosenGun = Guns[num];
		}
	}

	public void Suicide()
	{
		anim.SetBool("Suicidal", value: true);
		anim.SetBool("Shooting", value: false);
		anim.SetBool("Angry", value: false);
		Angered = false;
		shooting = false;
		AI.speed = 0f;
		anim.speed = 1f;
	}

	private IEnumerator OpenMouth()
	{
		openMouth = true;
		Hurt = true;
		yield return new WaitForSeconds(2f);
		Hurt = false;
		openMouth = false;
	}
}
