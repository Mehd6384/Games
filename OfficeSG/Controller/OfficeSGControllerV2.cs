using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeSGControllerV2 : MonoBehaviour {

	public bool UseMouseKeyboard = false; 
	public float Speed; 
	public float RotationSpeed; 
	public ParticleSystem particles;
	public Transform TargetPosition; 

	[HideInInspector]
	public Rigidbody rb; 
	Camera cam; 
	Quaternion target_rotation; 
	Animator anim; 
	OfficeShoot ShootingLogic; 

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody>(); 
		cam = Camera.main;
		target_rotation = transform.rotation; 	
		Speed = - Speed; 
		anim = GetComponent<Animator>(); 
		ShootingLogic = GetComponent<OfficeShoot>(); 
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 inputs = Vector2.zero; 
		if(UseMouseKeyboard)
		{
			inputs = ProcessKB(); 
		}
		else 
		{
			inputs = ProcessAxisJoystick(); 
		}
		
		float x = inputs.x; 
		float y = inputs.y; 

		if (x*x + y*y > 0.2f)
		{
			Rotate(x,y); 
		}

		AdjustRotation(); 

		bool shoot_trigger = ProcessShoot(); 

		if(shoot_trigger)
		{
			Shoot();
		}
		
	}



	void Shoot()
	{
		AddVelocity(); 
		PlayParticles(); 
		PlayShootAnim(); 
		ShootingLogic.Shoot(); 
	}

	void PlayShootAnim()
	{
		anim.SetTrigger("Shoot"); 
	}

	void PlayParticles()
	{
		ParticleSystem p = Instantiate(particles, TargetPosition.position, TargetPosition.rotation) as ParticleSystem; 
		p.Play(); 
	}

	void AddVelocity()
	{
		rb.velocity += (transform.forward)*Speed; 
	}

	void AdjustRotation()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, target_rotation, RotationSpeed*Time.deltaTime); 
	}

	void Rotate(float x,float y)
	{
		target_rotation = Quaternion.AngleAxis(180, transform.up)*Quaternion.LookRotation(CamToPlayerDirected(x,y)); 
	}

	Vector3 CamToPlayer()
	{
		Vector3 v = transform.position - cam.transform.position; 
		v.y = 0; 
		return v.normalized;
	}

	Vector3 CamToPlayerDirected(float x, float y)
	{
		Vector3 v = -CamToPlayer(); 
		Vector3 vd = Quaternion.AngleAxis(90, transform.up)*v; 

		return (y*v + x*vd).normalized; 
	}

	Vector2 ProcessKB()
	{
		float x =0f;
		float y = 0f;  
		if(Input.GetKey(KeyCode.H))
		{
			x = 1f; 
		}
		else if(Input.GetKey(KeyCode.F))
		{
			x = -1f; 
		}

		if(Input.GetKey(KeyCode.T))
		{
			y = 1f; 
		}
		else if(Input.GetKey(KeyCode.G))
		{
			y = -1f; 
		}

		return new Vector2(x,y);
	}

	Vector2 ProcessAxisJoystick()
	{
		return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	bool ProcessShoot()
	{
		if(UseMouseKeyboard)
		{
			return Input.GetMouseButtonDown(0); 
		}
		else
		{
			return Input.GetButtonDown("XButton"); 
		}
	}
}
