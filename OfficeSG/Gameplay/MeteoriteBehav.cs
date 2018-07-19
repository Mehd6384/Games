using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteBehav : MonoBehaviour {

	public ParticleSystem particleExplosion; 
	public float ExplosionRadius; 
	public float ExplosionForce; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += Vector3.down*Time.deltaTime*4f; 
		if(transform.position.y < 1.5f)
		{
			Explode(); 
		}
		
	}

	void Explode()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
       	for(int i = 0; i<hitColliders.Length;i++)
       	{
       		if(hitColliders[i].gameObject.GetComponent<ZombieLogic>())
       		{
       			hitColliders[i].gameObject.GetComponent<ZombieLogic>().rb.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

       		}
       		else if(hitColliders[i].gameObject.GetComponent<OfficeSGControllerV2>())
       		{
       			hitColliders[i].gameObject.GetComponent<OfficeSGControllerV2>().rb.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
       		}
       	}

       	ParticleSystem p = Instantiate(particleExplosion, transform.position, Quaternion.identity) as ParticleSystem; 
       	p.Play(); 
       	Destroy(gameObject); 
       	// Destroy(p, 1f); 

	}
}
