using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTouched : MonoBehaviour {

	public GameObject DestructedVersion; 

	public void Touched(Vector3 v, float ExplosionForce, float ExplosionRadius)
	{
		GameObject g = Instantiate(DestructedVersion, transform.position, transform.rotation) as GameObject; 
		AddRigidbodies(g, v, ExplosionForce, ExplosionRadius); 
		Destroy(gameObject); 
	}

	void AddRigidbodies(GameObject g, Vector3 v, float ExplosionForce, float ExplosionRadius)
	{
		int childcount = g.transform.childCount; 
		for(int i = 0; i<childcount; i++)
		{
			GameObject o = g.transform.GetChild(i).gameObject; 
			Rigidbody r = o.AddComponent<Rigidbody>();
			// o.GetComponent<Collider>().enabled = false;  
			r.AddExplosionForce(ExplosionForce, v,ExplosionRadius, 5f); 
			r.angularVelocity = new Vector3(Random.Range(-0.8f,0.8f),Random.Range(-0.8f,0.8f),Random.Range(-0.8f,0.8f)); 
			Destroy(o, 3f); 
		}
		g.transform.DetachChildren();
		Destroy(g); 
	}


}
