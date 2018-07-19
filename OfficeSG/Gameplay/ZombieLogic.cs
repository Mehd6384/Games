using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLogic : MonoBehaviour {

	public Transform Target; 
	public float Speed; 
	public float RotationSpeed; 
	public float HitDistance; 

	public bool InRange; 
	public float MaxVelocityMagnitude; 
	public float LerpSpeed; 

	public List<ZombieHitbox> hitboxes = new List<ZombieHitbox>(); 
	public float HitActivationTime; 
	public float DelayActivation; 
	float timerHit = -0.1f; 


	public bool HitActive = false; 

	[HideInInspector]
	public GameObject mothership;  


	float MaxTimer = 0.1f; 
	float timer = 0f; 

	Animator anim; 
	[HideInInspector]
	public Rigidbody rb; 
	Vector3 Direction; 
	Quaternion TargetRotation; 

	bool Dead = false; 
	float sink_timer = 2f;
	bool started_sink = false;  

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>(); 
		rb = GetComponent<Rigidbody>(); 

		TargetRotation = transform.rotation; 
		timer = MaxTimer; 
		
		HitboxesActivation(false); 
	}
	
	// Update is called once per frame
	void Update () {

		if (!Dead)
		{
			if(InRange)
			{
				Hit();
			}
			else
			{
				GetCloser(); 
			}
				
			CheckDistance(); 
			AdjustRotation(); 

			HitboxCount(); 

		}
		else
		{
			Disapear(); 
		}
	
	}

	void CheckDistance()
	{
		if(timer <= 0f)
		{	
			timer = MaxTimer; 
			InRange = ((Target.position - transform.position).magnitude < HitDistance) ? true: false; 
		}
		else
		{
			timer -= Time.deltaTime; 
		}
	}

	void Hit()
	{
		if(!HitActive)
		{
			anim.SetTrigger("Hit"); 
			HitActive = true; 
			HitboxesActivation(true); 
			timerHit = HitActivationTime; 
		}
	}

	void HitboxCount()
	{
		if(HitActive)
		{
			timerHit -= Time.deltaTime; 
			if(timerHit <= 0f)
			{
				HitActive = false; 
				HitboxesActivation(false);
				timerHit = -1f; 
			}
		}
	}

	void HitboxesActivation(bool state)
	{
		foreach(ZombieHitbox zhb in hitboxes)
		{
			zhb.enabled = state; 
			if(state) 
				zhb.SetDelay(DelayActivation); 
		}
	}

	void GetDirection()
	{
		Vector3 v = Target.position - transform.position; 
		v.y = 0; 
		Direction = v.normalized; 
	}

	void GetCloser()
	{
		GetDirection();

		Move(transform.forward*Speed); 
		Rotate(); 
	}
	void Move(Vector3 v)
	{
		rb.AddForce(v);
		if(rb.velocity.magnitude > MaxVelocityMagnitude)
		{
			Vector3 ideal_speed = rb.velocity.normalized*MaxVelocityMagnitude; 
			Vector3 tmp = Vector3.Lerp(rb.velocity, ideal_speed, LerpSpeed*Time.deltaTime); 
			rb.velocity = tmp; 
			Debug.Log("Smoothing"); 
		}
	}

	void Rotate()
	{
		// float angle = Vector3.SignedAngle(transform.forward, Direction, transform.up);
		// if(Mathf.Abs(angle) > 2f)
		// TargetRotation = Quaternion.AngleAxis(angle, transform.up); 
		// else
		// 	TargetRotation = transform.rotation; 
		transform.LookAt(transform.position + Direction); 

	}

	void AdjustRotation()
	{
		float a= 0; 
		// transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, RotationSpeed*Time.deltaTime); 
	}

	public void Die(Vector3 pos, float ex_strength, float ex_rad)
	{
		rb.constraints = RigidbodyConstraints.None;
		rb.AddExplosionForce(ex_strength, pos, ex_rad, 3f); 
		anim.SetTrigger("Impact"); 

		rb.drag = 2f; 
		Destroy(gameObject, 5f); 
		if(!Dead)
			mothership.GetComponent<MonsterCreation>().AddDeathToCounter(); 
		
		Dead = true ;
	}

	void Disapear()
	{
	
		if (sink_timer > 0f)
		{
			sink_timer -= Time.deltaTime; 
			if(sink_timer <= 0f)
				GetComponent<Collider>().enabled = false; 
		}
	}
}
