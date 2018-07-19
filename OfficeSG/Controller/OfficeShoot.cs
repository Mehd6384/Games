using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeShoot : MonoBehaviour {

	public float ShootDistance; 
	public LayerMask Touchable; 
	public float ExplosionRadius; 
	public float ExplosionForce; 

	public ParticleSystem particleImpact; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	// public void Shoot()
	// {
	// 	Ray ray = new Ray(transform.position, transform.forward); 
	// 	RaycastHit hit; 

	// 	if(Physics.Raycast(ray, out hit, 10, Touchable))
	// 	{
	// 		GameObject g = hit.collider.gameObject; 
	// 		if(g.GetComponent<WallTouched>())
	// 		{
	// 			AddRigidbodies(g, hit.point);
	// 		}
	// 		else if(g.GetComponent<EnnemiTouched>())
	// 		{
	// 			Debug.Log("Touched ennemi"); 
	// 			g.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce,hit.point,ExplosionRadius,1f); 
	// 			g.GetComponent<Animator>().SetTrigger("Impact"); 
	// 		}
	// 	}
	// }

	public void Shoot()
	{
		RaycastHit hit; 
		if(Physics.BoxCast(transform.position, Vector3.one*0.5f,  transform.forward, out hit,  Quaternion.identity, ShootDistance, Touchable ))
		{	
			GameObject g = hit.collider.gameObject; 
			Debug.Log(g); 
			if(g.GetComponent<WallTouched>())
			{
				g.GetComponent<WallTouched>().Touched(hit.point, ExplosionForce, ExplosionRadius); 
			}
			else if(g.GetComponent<ZombieLogic>())
			{
				g.GetComponent<ZombieLogic>().Die(hit.point, ExplosionForce, ExplosionRadius); 
			}	

			PlayParticles(hit.point); 

		}
	}

	void PlayParticles(Vector3 v)
	{
		ParticleSystem p = Instantiate(particleImpact, v, Quaternion.identity) as ParticleSystem;
		p.Play();
	}

	
	void AddRigidbodies(GameObject g, Vector3 v)
	{
		int childcount = g.transform.childCount; 
		for(int i = 0; i<childcount; i++)
		{
			GameObject o = g.transform.GetChild(i).gameObject; 
			Rigidbody r = o.AddComponent<Rigidbody>();
			o.GetComponent<Collider>().enabled = false;  
			r.AddExplosionForce(ExplosionForce, v,ExplosionRadius, 0f); 
			Destroy(o, 3f); 
		}
		g.transform.DetachChildren();
		Destroy(g); 
	}
}
