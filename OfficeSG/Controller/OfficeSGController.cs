using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeSGController : MonoBehaviour {


	public float Speed; 
	public float RotationSpeed; 
	public ParticleSystem particles;
	public Transform TargetPosition; 

	Rigidbody rb; 
	Camera cam; 
	Quaternion target_rotation; 
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>(); 
		cam = Camera.main;
		target_rotation = transform.rotation; 		
	}
	
	// Update is called once per frame
	void Update () {

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		if(Input.GetButtonDown("XButton"))
		{
			Shoot(x,y); 
		}


		// Debug.DrawRay(transform.position, transform.forward, Color.red, 1f); 
		// Rotate(x,y); 
		AdjustRotation(); 
		
	}

	void Shoot(float x, float y)
	{
		Vector3 direction = CamToPlayerDirected(x,y); 
		AddVelocity(direction*Speed); 
		PlayParticles(); 
		Rotate(x,y); 
	}

	void AdjustRotation()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, target_rotation, RotationSpeed*Time.deltaTime); 
	}

	void PlayParticles()
	{
		ParticleSystem s = Instantiate(particles, TargetPosition.position, transform.rotation) as ParticleSystem; 
		s.Play(); 
	}

	void AddVelocity(Vector3 v)
	{
		rb.velocity += v; 
	}

	void Rotate(float x, float y)
	{
		if (rb.velocity.magnitude > 0.1f)
		{
			Vector3 direction = CamToPlayerDirected(x,y); 
			// transform.rotation = Quaternion.LookRotation(direction); 
			target_rotation = Quaternion.LookRotation(direction);
		}
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

		if(x*x + y*y < 0.1f)
		{
			return v; 
		}

		return (y*v + x*vd).normalized; 
	}
}
